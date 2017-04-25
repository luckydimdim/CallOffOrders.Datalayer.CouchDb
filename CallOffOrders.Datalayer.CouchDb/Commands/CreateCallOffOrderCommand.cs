using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public CreateCallOffOrderCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<CreateCallOffOrderCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            var doc = _autoMapper.Map<CallOffOrderDto>(commandContext.Form);

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PostAsync(doc);
            });

            commandContext.Id = result.Id;

            return commandContext;
        }
    }
}