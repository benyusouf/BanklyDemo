namespace BanklyDemo.Core.Users.Models
{
    public class AuthenticationResponse
    {
        public string Token { get; set; }

        public User User { get; set; }
    }
}
