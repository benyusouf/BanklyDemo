using Microsoft.EntityFrameworkCore;

namespace BanklyDemo.Core.Users.Models
{
        [Owned]
        public class LoginProfile
        {
            public string Password { get; set; }

            public string Salt { get; set; }

            public string ApiKey { get; set; }
        }
}
