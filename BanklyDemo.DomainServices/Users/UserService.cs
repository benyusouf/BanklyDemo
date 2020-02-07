using AutoMapper;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Helpers;
using BanklyDemo.Core.Users;
using BanklyDemo.Core.Users.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BanklyDemo.DomainServices.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public User FindUserByEmailAsync(string email)
        {
            ArgumentGuard.NotNullOrWhiteSpace(email, nameof(email));

            var userEntities = _userRepository.GetMany(u => u.EmailAddress == email);

            return Mapper.Map<User>(userEntities.SingleOrDefault());
        }

        public async Task<IList<User>> GetAllUsersAsync()
        {
            var userEntities = await _userRepository.GetAllAsync();
            return Mapper.Map<IList<User>>(userEntities);
        }

        public async Task<User> GetUserAsync(Guid userId)
        {
            ArgumentGuard.NotEmpty(userId, nameof(userId));

            var userEntity = await _userRepository.GetAsync(userId);

            return Mapper.Map<User>(userEntity);
        }
    }
}
