using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Criteria;
using Cmas.BusinessLayers.CallOffOrders.Entities;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByContractIdQuery : IQuery<FindByContractId, Task<IEnumerable<CallOffOrder>>>
    {
        private IMapper _autoMapper;

        public FindByContractIdQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<IEnumerable<CallOffOrder>> Ask(FindByContractId criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {

                var result = new List<CallOffOrder>();

                var query = new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByContractDocsViewName).Configure(q => q.Key(criterion.ContractId));

                var viewResult = await client.Views.QueryAsync<CallOffOrderDto>(query);

                foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
                {

                    var order = _autoMapper.Map<CallOffOrder>(row.Value);
                    order.Id = row.Value._id;
                    result.Add(order);
                }

                return result;

            }

        }
    }
}
