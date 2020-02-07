using BanklyDemo.Core.Common.Models;
using System;

namespace BanklyDemo.Core.Products.Models
{
    public class ProductEntity: BaseEntity
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public double Price { get; set; }

        public DateTime CreatedTimeUtc { get; set; }
    }
}
