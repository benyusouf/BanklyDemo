using BanklyDemo.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Users
{
    public interface IUserService
    {
        Task<User> GetUserAsync(Guid userId);

        User FindUserByEmailAsync(string email);

        Task<IList<User>> GetAllUsersAsync();
    }
}
