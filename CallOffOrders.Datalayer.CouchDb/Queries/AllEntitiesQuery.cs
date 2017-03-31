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
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
                var result = new List<CallOffOrder>();

                var query = new QueryViewRequest("call-off-orders", "all");

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
