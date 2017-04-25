using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;
using Microsoft.Extensions.Logging;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class UpdateCallOffOrderCommand : ICommand<UpdateCallOffOrderCommandContext>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchWrapper _couchWrapper;

        public UpdateCallOffOrderCommand(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<UpdateCallOffOrderCommand>();
            _couchWrapper = new CouchWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<UpdateCallOffOrderCommandContext> Execute(UpdateCallOffOrderCommandContext commandContext)
        {
            // FIXME: нельзя так делать, надо от frontend получать Rev
            var header = await _couchWrapper.GetHeaderAsync(commandContext.Form.Id);
             
            var entity = _autoMapper.Map<CallOffOrderDto>(commandContext.Form);

            entity._rev = header.Rev;
             
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.PutAsync(entity._id, entity);
            });
             
            return commandContext;   // TODO: возвращать _revid
        }
    }
}