using System;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using Cmas.BusinessLayers.CallOffOrders.CommandsContexts;
using Cmas.Infrastructure.Domain.Commands;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Commands
{
 
    public class UpdateCallOffOrderCommand : ICommand<UpdateCallOffOrderCommandContext>
    {

        private IMapper _autoMapper;

        public UpdateCallOffOrderCommand(IMapper autoMapper)
        {
            _autoMapper = autoMapper;
        }

        public async Task<UpdateCallOffOrderCommandContext> Execute(UpdateCallOffOrderCommandContext commandContext)
        {
            using (var client = new MyCouchClient("http://cmas-backend:backend967@cm-ylng-msk-03:5984", "call-off-orders"))
            {
                // FIXME: нельзя так делать, надо от frontend получать
                var existingDoc = (await client.Entities.GetAsync<CallOffOrderDto>(commandContext.Form.Id)).Content;
 
                var newDto = _autoMapper.Map<CallOffOrderDto>(commandContext.Form);
                newDto._id = existingDoc._id;
                newDto.Status = existingDoc.Status;
                newDto._rev = existingDoc._rev;
                newDto.UpdatedAt = DateTime.Now;

                var result = await client.Entities.PutAsync(newDto._id, newDto);

                if (!result.IsSuccess)
                {
                    throw new Exception(result.Error);
                }

                // TODO: возвращать _revid

                return commandContext;
            }

        }
    }
}
