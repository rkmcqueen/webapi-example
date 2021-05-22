
using keyapi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace keyapi.Maps{
    /// <summary>Api Key Mapper</summary>
    public class ApiKeyMap
    {
        /// <summary>Initializes a new instance of the <see cref="ApiKeyMap" /> class.</summary>
        /// <param name="entityBuilder">The entity builder.</param>
        public ApiKeyMap(EntityTypeBuilder<ApiKey> entityBuilder)
        {
            entityBuilder.HasKey(x => x.Id);
            entityBuilder.ToTable("apikey");

            entityBuilder.Property(x => x.Id).HasColumnName("id");
            entityBuilder.Property(x => x.Key).HasColumnName("key");
            entityBuilder.Property(x => x.Vendor).HasColumnName("vendor");
            entityBuilder.Property(x => x.RequestCount).HasColumnName("request_count");

            entityBuilder.Property(x => x.CreatedAt).HasColumnName("created_at");
        }
    }
}