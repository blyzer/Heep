using System.Collections.Generic;

namespace HeeP.Models.BusinessModel
{
    public class UserProfile : Person
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public ICollection<Access> AccessList { get; set; }
    }
}
