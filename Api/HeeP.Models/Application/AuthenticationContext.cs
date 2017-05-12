namespace HeeP.Models.Application
{
    public class AuthenticationContext
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Token { get; set; }
        public string Client { get; set; }
    }
}
