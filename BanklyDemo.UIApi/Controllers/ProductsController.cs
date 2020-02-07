using BanklyDemo.Core.Products;
using BanklyDemo.Core.Products.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("/api/products")]
    [ApiController]
    [Authorize]
    public class ProductsController: ControllerBase
    {
        private readonly IProductService _productService;
        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("{productId:Guid}/summary")]
        public async Task<Product> GetProductAsync(Guid productId)
        {
            return await _productService.GetProductAsync(productId);
        }

        [HttpGet]
        public async Task<IList<Product>> GetAllProductsAsync()
        {
            return await _productService.GetAllProductsAsync();
        }

        [HttpGet("{userId:Guid}/products")]
        public IList<Product> GetUserProducts(Guid userId) 
        {
            return _productService.GetUserProducts(userId);
        }

        [HttpPost("add")]
        public async Task<Guid> AddProductAsync([FromForm]SaveProductModel product)
        {
            return await _productService.AddProductAsync(product);
        }

        [HttpPut("{productId:Guid}/update")]
        public async Task UpdateProductAsync(Guid productId, [FromForm]SaveProductModel product)
        {
            await _productService.UpdateProductAsync(productId, product);
        }

        [HttpDelete("{productId:Guid}")]
        public async Task DeleteProductAsync(Guid productId)
        {
            await _productService.DeleteProductAsync(productId);
        }
    }
}
