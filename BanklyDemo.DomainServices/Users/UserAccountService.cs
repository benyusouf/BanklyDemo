using AutoMapper;
using BanklyDemo.Core.Common;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Helpers;
using BanklyDemo.Core.Users;
using BanklyDemo.Core.Users.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BanklyDemo.DomainServices.Users
{
    public class UserAccountService : IUserAccountService
    {
        private IUserRepository _userRepository;
        private IUserService _userService;
        private ICryptoService _cryptoService;
        private IJwtHandler _jwtHandler;
        public UserAccountService(
            IUserRepository userRepository,
            IUserService userService,
            ICryptoService cryptoService, IJwtHandler jwtHandler)
        {
            _userRepository = userRepository;
            _userService = userService;
            _cryptoService = cryptoService;
            _jwtHandler = jwtHandler;
        }
        public Task<Guid> ChangePasswordAsync(string password)
        {
            throw new NotImplementedException();
        }

        public Task ConfirmEmailAsync()
        {
            throw new NotImplementedException();
        }

        public Task<string> GetApiKeyAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailAddressChangeRequestAccessKeyAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task<string> GetEmailConfirmationAccessKeyAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public AuthenticationResponse LoginAsync(UserLoginModel model)
        {
            ArgumentGuard.NotNullOrEmpty(model.EmailAddress, nameof(model.EmailAddress));
            // ArgumentGuard.NotNullOrEmpty(model.ReturnUrl, nameof(model.ReturnUrl));

            var email = model.EmailAddress.Trim().ToLower();

            var userEntities = _userRepository.GetMany(u => u.EmailAddress == email);
            var userEntity = userEntities.SingleOrDefault();

            if (userEntity.IsNull())
            {
                throw new Exception($"No user with email {email} exists");
            }

            var hashedPassword = _cryptoService.Hash(model.Password, userEntity.LoginProfile.Salt, 5323);

            if (userEntity.LoginProfile.Password != hashedPassword)
            {
                throw new Exception("Incorrect Password");
            }

            var user =  Mapper.Map<User>(userEntity);

            var token = _jwtHandler.CreateAccessToken(user);

            return new AuthenticationResponse
            {
                Token = token,
                User = user
            };
        }

        public async Task<User> RegisterAsync(UserRegistrationModel model, bool password = false)
        {
            ArgumentGuard.NotNull(model, nameof(model));
            ArgumentGuard.NotNullOrEmpty(model.EmailAddress, nameof(model.EmailAddress));
            ArgumentGuard.NotNullOrEmpty(model.FirstName, nameof(model.FirstName));
            ArgumentGuard.NotNullOrEmpty(model.LastName, nameof(model.LastName));

            var dbUser = _userService.FindUserByEmailAsync(model.EmailAddress);

            if (!dbUser.IsNull())
            {
                throw new Exception($"User With ${model.EmailAddress} Already Exists");
            }

            var newUser = Mapper.Map<UserEntity>(model);

            newUser.Id = Guid.NewGuid();
            newUser.CreatedTimeUtc = DateTime.Now;

            if (password && !model.Password.IsNullOrEmpty())
            {
                newUser.LoginProfile = new LoginProfile();
                var salt = _cryptoService.GenerateSalt(32);
                newUser.LoginProfile.Salt = salt;
                newUser.LoginProfile.Password = _cryptoService.Hash(model.Password, salt, 5323);

                await _userRepository.AddAsync(newUser);
            }
            else
            {
                await _userRepository.AddAsync(newUser);
            }

            return Mapper.Map<User>(newUser);
        }

        public Task<string> ResetApiKeyAsync(Guid userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateProfileAsync(Guid userId, User model)
        {
            throw new NotImplementedException();
        }
    }
}
