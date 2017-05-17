using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeeP.Data.EntityFrameworkImplementation.EntityMaps
{
    public class AccessMap
    {
        public AccessMap(EntityTypeBuilder<Access> builder)
        {
            builder
                .HasKey(a => a.AccessId);

            builder
                .Property(p => p.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();            
        }
    }
}
