using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeeP.Data.EntityFrameworkImplementation.EntityMaps
{
    public class AccessMap
    {
        public AccessMap(EntityTypeBuilder<Access> builder)
        {
            builder.Property(p => p.RowVersion).IsConcurrencyToken();
        }
    }
}
