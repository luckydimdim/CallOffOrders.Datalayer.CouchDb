using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using System;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public CreateCallOffOrderCommand(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            var doc = _autoMapper.Map<CallOffOrderDto>(commandContext.CallOffOrder);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PostAsync(doc);
            });

            commandContext.Id = result.Id;

            return commandContext;
        }
    }
}