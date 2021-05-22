using keyapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace keyapi.Repositories
{
    public interface IKeyRepository
    {
        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <param name="vendor">The vendor for the API Key</param>
        /// <returns></returns>
        public Task<List<ApiKey>> GetAsync(string vendor = null);

        /// <summary>
        /// Gets a key by Id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        public Task<ApiKey> GetByIdAsync(int id);

        /// <summary>Creates a new API key.</summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>created api key</returns>
        public Task<ApiKey> StoreApiKeyAsync(string vendor, Guid? apiKey = null);

        /// <summary>
        /// Deletes a key by id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        public Task<bool> DeleteByIdAsync(int id);
    }
}
