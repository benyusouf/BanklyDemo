using BanklyDemo.Core.Data;
using BanklyDemo.Core.Products.Models;

namespace BanklyDemo.Data.Repositories
{
    internal class ProductRepository : DataRepository<ProductEntity>, IProductRepository
    {
        private readonly BanklyDemoDbContext _dbContext;

        public ProductRepository(BanklyDemoDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
