using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using System;
using Cmas.DataLayers.Infrastructure;


namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrder>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public FindByIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<CallOffOrder> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);
            });

            if (result == null)
                return null;

            return _autoMapper.Map<CallOffOrder>(result.Content);
        }
    }
}