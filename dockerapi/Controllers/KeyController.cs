using keyapi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Encodings.Web;
using System.Linq;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Annotations;
using keyapi.Repositories;

namespace keyapi.Controllers
{
    /// <summary>
    /// Api Key Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class KeyController : ControllerBase
    {
        private readonly IKeyRepository Repository;

        /// <summary>Initializes a new instance of the <see cref="KeyController" /> class.</summary>
        /// <param name="repository">The key repository.</param>
        public KeyController(IKeyRepository repository)
        {
            this.Repository = repository;
        }

        /// <summary>
        /// This method shows all keys
        /// </summary>
        /// <param name="vendor">The vendor for the API Key</param>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Key/Vendor
        /// </remarks>
        [HttpGet]
        public async Task<object> Get(string vendor = null)
        {
            List<ApiKey> vendorKeys = await this.Repository.GetAsync(vendor);
            if (!vendorKeys.Any())
                return NotFound(new { error = $"There were no api keys found{(string.IsNullOrEmpty(vendor) ? "" : $" for vendor {vendor}")}." });
            return vendorKeys;
        }

        /// <summary>
        /// This method shows all keys by Id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Key/Id
        /// </remarks>
        [HttpGet("{id}")]
        public async Task<object> GetById(int id)
        {
            ApiKey apiKey = await this.Repository.GetByIdAsync(id);
            if (apiKey == null)
                return NotFound(new { Id = id, error = $"There was no api key with an id of {id}." });
            return apiKey;
        }

        /// <summary>Creates a new API key.</summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="apiKey">The API key.</param>
        /// <returns>created api key</returns>
        [HttpPost]
        public async Task<object> CreateApiKey(string vendor, Guid? apiKey = null)
        {
            if (string.IsNullOrWhiteSpace(vendor))
                return BadRequest(new { Parameter = "vendor", error = "vendor is required" });
            try
            {
                return await this.Repository.StoreApiKeyAsync(vendor, apiKey);
            }
            catch (Exception e)
            {
                return BadRequest(new { error = e.Message });
            }
        }

        /// <summary>
        /// This method deletes a key by id
        /// </summary>
        /// <param name="id">The id for the API Key</param>
        /// <returns></returns>
        ///<remarks>
        /// Sample request
        /// GET/Key/Id
        /// </remarks>
        [HttpDelete("{id}")]
        public async Task<object> DeleteById(int id)
        {
            bool success = await this.Repository.DeleteByIdAsync(id);
            if (!success)
                return NotFound(new { Id = id, error = $"There was no api key with an id of {id}." });
            return null;
        }
    }
}