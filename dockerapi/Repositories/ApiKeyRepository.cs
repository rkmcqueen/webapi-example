using keyapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;

namespace keyapi.Repositories
{
   
    public class KeyRepository : IKeyRepository
    {
        private readonly ApiKeyDbContext _context;

        /// <summary>Initializes a new instance of the <see cref="KeyRepository" /> class.</summary>
        /// <param name="context">The context.</param>
        public KeyRepository(ApiKeyDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get All Keys
        /// </summary>
        /// <param name="vendor">The vendor for the API Key</param>
        /// <returns></returns>
        public async Task<List<ApiKey>> GetAsync(string vendor = null)
        {
            List<ApiKey> vendorKeys = await _context.ApiKeys.Where(b => string.IsNullOrEmpty(vendor) || b.Vendor.ToLower() == vendor.ToLower()).ToListAsync();
            vendorKeys.ForEach(key => key.RequestCount++);
            await _context.SaveChangesAsync();
            return vendorKeys;
        }

        /// <summary>
        /// Gets a key by Id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        public async Task<ApiKey> GetByIdAsync(int id)
        {
            ApiKey apiKey = await _context.ApiKeys.FirstOrDefaultAsync(b => b.Id == id);
            if (apiKey != null)
            {
                apiKey.RequestCount++;
                await _context.SaveChangesAsync();
            }
            return apiKey;
        }

        /// <summary>Creates a new API key.</summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>created api key</returns>
        public async Task<ApiKey> StoreApiKeyAsync(string vendor, Guid? apiKey = null)
        {
            if (string.IsNullOrWhiteSpace(vendor))
                throw new Exception("vendor is required");
            ApiKey newKey = new ApiKey(vendor, apiKey);
            if (_context.ApiKeys.Where(key => key.Key == newKey.Key).Any())
                throw new Exception($"Key {apiKey} already exists");
            await _context.ApiKeys.AddAsync(newKey);
            await _context.SaveChangesAsync();
            return await this.GetByIdAsync(newKey.Id);
        }

        /// <summary>
        /// Deletes a key by id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        public async Task<bool> DeleteByIdAsync(int id)
        {
            ApiKey apiKey = await _context.ApiKeys.FirstOrDefaultAsync(b => b.Id == id);
            if (apiKey == null)
                return false;
            _context.ApiKeys.Remove(apiKey);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}