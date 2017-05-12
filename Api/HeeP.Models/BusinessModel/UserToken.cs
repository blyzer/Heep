using System;

namespace HeeP.Models.BusinessModel
{
    public class UserToken : BaseEntity
    {
        public int UserTokenId { get; set; }
        public string Token { get; set; }
        public int UserId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime DueDate { get; set; }

        public User User { get; set; }
    }
}
