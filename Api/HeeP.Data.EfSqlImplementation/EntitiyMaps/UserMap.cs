using HeeP.Models.BusinessModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HeeP.Data.EntityFrameworkImplementation.EntityMaps
{
    public class UserMap
    {
        public UserMap(EntityTypeBuilder<User> builder)
        {
            builder.Property(p => p.RowVersion).IsConcurrencyToken()
                .HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .Map(ur => {
                    ur.MapLeftKey(nameof(User.UserId));
                    ur.MapRightKey(nameof(Role.RoleId));
                    ur.ToTable("UserRoles");
                })
                .Ignore(u => u.FullName);
            //Ignore(u => u.IdentificationTypeDescription);
        }
    }
}
