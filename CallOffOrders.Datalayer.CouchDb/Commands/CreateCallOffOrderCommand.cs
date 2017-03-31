using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class CreateCallOffOrderCommand : ICommand<CreateCallOffOrderCommandContext>
    {
        private IMapper _autoMapper;

        public CreateCallOffOrderCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<CreateCallOffOrderCommandContext> Execute(CreateCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
            {
                var doc = _autoMapper.Map<CallOffOrderDto>(commandContext.Form);

                doc._id = null;
                doc._rev = null;
                doc.UpdatedAt = DateTime.Now;
                doc.CreatedAt = DateTime.Now;

                var result = await store.Client.Entities.PostAsync(doc);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                commandContext.Id = result.Id;


                return commandContext;
            }

        }
    }
}
