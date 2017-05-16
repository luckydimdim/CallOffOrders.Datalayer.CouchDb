using System.Threading.Tasks;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using System;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class DeleteCallOffOrderCommand : ICommand<DeleteCallOffOrderCommandContext>
    { 
        private readonly CouchWrapper _couchWrapper;

        public DeleteCallOffOrderCommand(IServiceProvider serviceProvider)
        {  
            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<DeleteCallOffOrderCommandContext> Execute(DeleteCallOffOrderCommandContext commandContext)
        {
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Id);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Documents.DeleteAsync(commandContext.Id, header.Rev);
            });

            return commandContext;
        }
    }
}