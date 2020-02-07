using BanklyDemo.Core.Data;
using BanklyDemo.Core.Users.Models;

namespace BanklyDemo.AuthData.Repositories
{
    public class UserRepository : DataRepository<UserEntity>, IUserRepository
    {
        private readonly AuthDbContext _dbContext;

        public UserRepository(AuthDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
