using Microsoft.AspNetCore.Server.IIS.Core;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace keyapi.Models
{
    /// <summary>Api Key</summary>
    public class ApiKey
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public int Id { get; set; }
        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        public Guid Key { get; set; }
        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        public string Vendor { get; set; }

        /// <summary>Gets or sets the request count.</summary>
        /// <value>The request count.</value>
        public int RequestCount { get; set; }

        /// <summary>Gets or sets the created at timestamp.</summary>
        /// <value>The created at timestamp.</value>
        public DateTime CreatedAt { get; set; }
        /// <summary>Initializes a new instance of the <see cref="ApiKey" /> class.</summary>
        public ApiKey()
        {

        }
        /// <summary>Initializes a new instance of the <see cref="ApiKey" /> class.</summary>
        /// <param name="vendor">The vendor.</param>
        /// <param name="key">The key.</param>
        public ApiKey(string vendor, Guid? key = null)
        {
            this.Vendor = vendor;
            this.Key = key ?? Guid.NewGuid();
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}