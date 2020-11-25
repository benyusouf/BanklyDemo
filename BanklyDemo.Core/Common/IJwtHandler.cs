using BanklyDemo.Core.Users.Models;

namespace BanklyDemo.Core.Common
{
    public interface IJwtHandler
    {
        string CreateAccessToken(User user);
    }
}
