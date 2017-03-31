using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Entities;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrder>>
    {
        private IMapper _autoMapper;

        public FindByIdQuery(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<CallOffOrder> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
 
                var dto = await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);

                CallOffOrder result = _autoMapper.Map<CallOffOrder>(dto.Content);
                result.Id = dto.Content._id;

                return result;
 
            }

        }
    }
}
