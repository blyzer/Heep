using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeeP.Data.EntityFrameworkImplementation.EntityMaps
{
    public class RoleAccessMap
    {
        public RoleAccessMap(EntityTypeBuilder<RoleAccess> builder)
        {
            builder
                .Property(p => p.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            builder
                .HasKey(t => new { t.AccessId, t.RoleId });

            builder
                .HasOne(ar => ar.Access)
                .WithMany(a => a.RoleAccesses)
                .HasForeignKey(ar => ar.AccessId);

            builder
                .HasOne(ar => ar.Role)
                .WithMany(r => r.RoleAccesses)
                .HasForeignKey(ar => ar.RoleId);
        }
    }
}
