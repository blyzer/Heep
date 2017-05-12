using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class Role : BaseEntity
    {
        public int RoleId { get; set; }
        public string Description { get; set; }
        public bool Active { get; set; }

        public virtual ICollection<Access> Access { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}