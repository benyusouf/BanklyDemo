using BanklyDemo.Core.Complaints.Models;
using BanklyDemo.Core.Products.Models;
using Microsoft.EntityFrameworkCore;

namespace BanklyDemo.Data
{
    public class BanklyDemoDbContext: DbContext
    {
        public BanklyDemoDbContext(DbContextOptions<BanklyDemoDbContext> options) 
            : base(options) 
        {
        }

        public DbSet<ProductEntity> Products { get; set; }

        public DbSet<ComplaintEntity> Complaints { get; set; }
    }
}
