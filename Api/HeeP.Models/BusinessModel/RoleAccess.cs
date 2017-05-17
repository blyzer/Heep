using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class RoleAccess : BaseEntity
    {
        public int AccessId { get; set; }
        public int RoleId { get; set; }

        public virtual Access Access { get; set; }
        public virtual Role Role { get; set; }        
    }
}