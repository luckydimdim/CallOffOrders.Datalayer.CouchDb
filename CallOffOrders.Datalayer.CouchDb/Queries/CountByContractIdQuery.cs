using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Cmas.DataLayers.CouchDb.CallOffOrders.Dtos;
using MyCouch.Requests;
using Cmas.Infrastructure.Domain.Queries;
using Cmas.BusinessLayers.CallOffOrders.Criteria;
using Cmas.BusinessLayers.CallOffOrders.Entities;
using System;
using Cmas.DataLayers.Infrastructure;

namespace Cmas.DataLayers.CouchDb.CallOffOrders.Queries
{
    /// <summary>
    /// Получить количество НЗ по договору
    /// </summary>
    public class CountByContractIdQuery : IQuery<FindByContractId, Task<int>>
    {
        private readonly IMapper _autoMapper;
        private readonly CouchWrapper _couchWrapper;

        public CountByContractIdQuery(IServiceProvider serviceProvider)
        {
            _autoMapper = (IMapper)serviceProvider.GetService(typeof(IMapper));

            _couchWrapper = new CouchWrapper(serviceProvider, DbConsts.ServiceName);
        }

        public async Task<int> Ask(FindByContractId criterion)
        {
            var query =
                new QueryViewRequest(DbConsts.DesignDocumentName, DbConsts.ByContractDocsViewName).Configure(
                    q => q.Key(criterion.ContractId).Reduce(true).IncludeDocs(false));

            var viewResult = await _couchWrapper.GetResponseAsync(async (client) =>
            {
                return await client.Views.QueryAsync(query);
            });
            
            return viewResult.Rows.Length;
        }
    }
}