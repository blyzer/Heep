using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeeP.Data.EntityFrameworkImplementation.EntityMaps
{
    public class RolMap
    {
        public RolMap(EntityTypeBuilder<Role> builder)
        {
            builder
                .Property(p => p.RowVersion)
                .IsConcurrencyToken()
                .ValueGeneratedOnAddOrUpdate();

            builder
                .HasMany(r => r.Access)
                .WithMany(a => a.Roles)
                .Map(ra => {
                    ra.MapLeftKey(nameof(Role.RoleId));
                    ra.MapRightKey(nameof(Access.AccessId));
                    ra.ToTable("RoleAccess");
                });
        }
    }
}
