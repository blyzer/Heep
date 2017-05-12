using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class Access : BaseEntity
    {
        public int AccessId { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Role> Roles { get; set; }
    }
}