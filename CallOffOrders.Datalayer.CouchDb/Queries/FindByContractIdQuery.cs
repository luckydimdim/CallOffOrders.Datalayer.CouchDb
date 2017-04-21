using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Criteria;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using Microsoft.Extensions.Logging;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    public class FindByContractIdQuery : IQuery<FindByContractId, Task<IEnumerable<CallOffOrder>>>
    {
        private IMapper _autoMapper;
        private readonly ILogger _logger;
        private readonly CouchDbWrapper _couchWrapper;

        public FindByContractIdQuery(IMapper autoMapper, ILoggerFactory loggerFactory)
        {
            _autoMapper = autoMapper;
            _logger = loggerFactory.CreateLogger<FindByContractIdQuery>();
            _couchWrapper = new CouchDbWrapper(DbConsts.DbConnectionString, DbConsts.DbName, _logger);
        }

        public async Task<IEnumerable<CallOffOrder>> Ask(FindByContractId criterion)
        {
            var result = new List<CallOffOrder>();

            var query =
                new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByContractDocsViewName).Configure(
                    q => q.Key(criterion.ContractId));

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync<CallOffOrderDto>(query);
            });

            foreach (var row in viewResult.Rows.OrderByDescending(s => s.Value.CreatedAt))
            {
                result.Add(_autoMapper.Map<CallOffOrder>(row.Value));
            }

            return result;
        }
    }
}