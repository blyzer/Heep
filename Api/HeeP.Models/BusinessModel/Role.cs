using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<RoleAccess> RoleAccesses { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}