using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using System.Net;
using System;
using Cmas.Infrastructure.ErrorHandler;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrder>>
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        
        public FindByIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByIdQuery>();
        }

        public async Task<CallOffOrder> Ask(FindById criterion)
        {
            using (var client = new MyCouchClient(DbConsts.DbConnectionString, DbConsts.DbName))
            {
 
                var result = await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);

                _logger.LogInformation(result.ToStringDebugVersion());

                if (!result.IsSuccess)
                {
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    { 
                        throw new NotFoundErrorException(String.Format("Call off order with id {0} not found", criterion.Id));
                    }
                     
                    throw new Exception("Unknown exception");
                }
                  
                return _autoMapper.Map<CallOffOrder>(result.Content);
            }

        }
    }
}
