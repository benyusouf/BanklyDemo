using BanklyDemo.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Products
{
    public interface IProductService
    {
        Task<Product> GetProductAsync(Guid productId);

        Task<IList<Product>> GetAllProductsAsync();

        IList<Product> GetUserProducts(Guid userId);

        Task<Guid> AddProductAsync(SaveProductModel product);

        Task UpdateProductAsync(Guid productId, SaveProductModel product);

        Task DeleteProductAsync(Guid productId);
    }
}
