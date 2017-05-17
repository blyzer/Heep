using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class User : Person
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
