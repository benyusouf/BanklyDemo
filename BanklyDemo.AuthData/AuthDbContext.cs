using BanklyDemo.Core.Users.Models;
using Microsoft.EntityFrameworkCore;

namespace BanklyDemo.AuthData
{
    public class AuthDbContext: DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options)
            : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
    }
}
