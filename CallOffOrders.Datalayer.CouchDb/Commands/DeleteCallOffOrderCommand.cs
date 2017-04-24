using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class DeleteCallOffOrderCommand : ICommand<DeleteCallOffOrderCommandContext>
    {
        private readonly ILogger _logger;
        private readonly CouchDbWrapper _couchWrapper;

        public DeleteCallOffOrderCommand(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<UpdateCallOffOrderCommand>();
            _couchWrapper = new CouchDbWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<DeleteCallOffOrderCommandContext> Execute(DeleteCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
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
}