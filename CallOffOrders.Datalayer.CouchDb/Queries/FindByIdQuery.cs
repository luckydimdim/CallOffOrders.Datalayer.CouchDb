using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using Cmas.Infrastructure.Domain.Criteria;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByIdQuery : IQuery<FindById, Task<CallOffOrder>>
    {
        private readonly IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchDbWrapper _couchWrapper;

        public FindByIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByIdQuery>();
            _couchWrapper = new CouchDbWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<CallOffOrder> Ask(FindById criterion)
        {
            var result = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Entities.GetAsync<CallOffOrderDto>(criterion.Id);
            });

            return _autoMapper.Map<CallOffOrder>(result.Content);
        }
    }
}