using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Cmas.Infrastructure.Domain.Criteria;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class AllEntitiesQuery : IQuery<AllEntities, Task<IEnumerable<CallOffOrder>>>
    {
        private IMapper _autoMapper;

        public AllEntitiesQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<CallOffOrder>> Ask(AllEntities criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var result = new List<CallOffOrder>();

                var query = new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.AllDocsViewName);

                var viewResult = await client.Views.QueryAsync<CallOffOrderDto>(query);

                foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
                { 
                    result.Add(_autoMapper.Map<CallOffOrder>(row.Value));
                }

                return result;
            }
        }
    }
}
