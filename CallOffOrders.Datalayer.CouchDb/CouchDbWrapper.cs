using System;
using System.Net;
using System.Threading.Tasks;
using Cmas.Infrastructure.ErrorHandler;
using Microsoft.Extensions.Logging;
using MyCouch;
using MyCouch.Responses;

namespace Cmas.DataLayers.CouchDb.CallOffOrders
{
    public class CouchDbWrapper
    {
        private string dbConnectionString;
        private string dbName;
        private readonly ILogger logger;

        public CouchDbWrapper(string dbConnectionString, string dbName, ILogger logger)
        {
            this.dbConnectionString = dbConnectionString;
            this.dbName = dbName;
            this.logger = logger;
        }

        public async Task<T> GetResponseAsync<T>(Func<MyCouchClient, Task<T>> method) where T : Response
        {
            using (var client = new MyCouchClient(dbConnectionString, dbName))
            {
                var result = await method(client);

                logger.LogInformation(result.ToStringDebugVersion());

                if (!result.IsSuccess)
                {
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundErrorException();
                    }

                    throw new Exception("Unknown exception");
                }

                return result;
            }
        }

        /*public async Task<T> GetResponseAsync<T>(Func<MyCouchStore, Task<T>> method) where T : Response
        {
            using (var store = new MyCouchStore(dbConnectionString, dbName))
            {
                var result = await method(store);

                logger.LogInformation(result.ToStringDebugVersion());

                if (!result.IsSuccess)
                {
                    if (result.StatusCode == HttpStatusCode.NotFound)
                    {
                        throw new NotFoundErrorException();
                    }

                    throw new Exception("Unknown exception");
                }

                return result;
            }
        }*/
    }

}
