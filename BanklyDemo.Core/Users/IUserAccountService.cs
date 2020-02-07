using BanklyDemo.Core.Users.Models;
using System;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Users
{
    public interface IUserAccountService
    {
        Task<User> RegisterAsync(UserRegistrationModel model, bool password = false);

        User LoginAsync(UserLoginModel model);

        Task UpdateProfileAsync(Guid userId, User model);

        Task<string> ResetApiKeyAsync(Guid userId);

        Task<string> GetApiKeyAsync(Guid userId);

        Task<Guid> ChangePasswordAsync(string password);

        Task<string> GetEmailConfirmationAccessKeyAsync(Guid userId);

        Task<string> GetEmailAddressChangeRequestAccessKeyAsync(Guid userId);

        Task ConfirmEmailAsync();
    }
}
