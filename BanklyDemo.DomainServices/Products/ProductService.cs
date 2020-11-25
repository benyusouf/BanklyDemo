using AutoMapper;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Helpers;
using BanklyDemo.Core.Products;
using BanklyDemo.Core.Products.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.DomainServices.Products
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<Guid> AddProductAsync(SaveProductModel product)
        {
            ArgumentGuard.NotNull(product, nameof(product));

            var productEntity = Mapper.Map<ProductEntity>(product);
            productEntity.Id = Guid.NewGuid();
            productEntity.CreatedTimeUtc = DateTime.Now;
            productEntity.Price = double.Parse(product.Price);

            return await _productRepository.AddAsync(productEntity);
        }

        public async Task DeleteProductAsync(Guid productId)
        {
            ArgumentGuard.NotNull(productId, nameof(productId));

            var productEntity = await _productRepository.GetAsync(productId);

            if (productEntity.IsNull())
            {
                throw new Exception("Product not found");
            }

            await _productRepository.DeleteAsync(productEntity);
        }

        public async Task<IList<Product>> GetAllProductsAsync()
        {
            var productEntities = await _productRepository.GetAllAsync();

            return Mapper.Map<IList<Product>>(productEntities);
        }

        public async Task<Product> GetProductAsync(Guid productId)
        {
            ArgumentGuard.NotNull(productId, nameof(productId));

            var productEntity = await _productRepository.GetAsync(productId);

            return Mapper.Map<Product>(productEntity);
        }

        public IList<Product> GetUserProducts(Guid userId)
        {
            ArgumentGuard.NotNull(userId, nameof(userId));

            var productEntities = _productRepository.GetMany(c => c.UserId == userId);

            return Mapper.Map<IList<Product>>(productEntities);
        }

        public async Task UpdateProductAsync(Guid productId, SaveProductModel product)
        {
            ArgumentGuard.NotNull(productId, nameof(productId));
            ArgumentGuard.NotNull(product, nameof(product));

            var dbProduct = await _productRepository.GetAsync(productId);

            if (dbProduct.IsNull())
            {
                throw new Exception("Product not found");
            }

            var productEntity = Mapper.Map<ProductEntity>(product);

            productEntity.Id = productId;
            productEntity.UserId = dbProduct.UserId;
            productEntity.CreatedTimeUtc = dbProduct.CreatedTimeUtc;

            await _productRepository.UpdateAsync(productEntity);
        }
    }
}
