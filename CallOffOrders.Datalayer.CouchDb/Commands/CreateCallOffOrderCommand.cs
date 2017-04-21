using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.DataLayers.CouchDb.CallOffOrders.Queries;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchDbWrapper _couchWrapper;

        public CreateCallOffOrderCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByContractIdQuery>();
            _couchWrapper = new CouchDbWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            var doc = _autoMapper.Map<CallOffOrderDto>(commandContext.Form);

            doc._id = null;
            doc._rev = null;

            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PostAsync(doc);
            });

            commandContext.Id = result.Id;
             
            return commandContext;
        }
    }
}