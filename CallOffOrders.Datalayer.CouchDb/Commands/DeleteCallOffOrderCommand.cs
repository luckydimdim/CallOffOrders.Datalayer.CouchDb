using System;
using System.Threading.Tasks;
using MyCouch;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
    public class DeleteCallOffOrderCommand : ICommand<DeleteCallOffOrderCommandContext>
    {
        public async Task<DeleteCallOffOrderCommandContext> Execute(DeleteCallOffOrderCommandContext commandContext)
        {
            using (var store = new MyCouchStore(DbConsts.DbConnectionString, DbConsts.DbName))
            {

                bool success = await store.DeleteAsync(commandContext.Id);

                if (!success)
                {
                    throw new Exception("error while deleting");
                }

                return commandContext;
            }

        }
    }
}
