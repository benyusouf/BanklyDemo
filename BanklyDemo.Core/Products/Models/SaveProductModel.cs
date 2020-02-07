using System;
using System.Collections.Generic;
using System.Text;

namespace BanklyDemo.Core.Products.Models
{
    public class SaveProductModel
    {
        public Guid UserId { get; set; }
        public string Name { get; set; }

        public string Description { get; set; }

        public string Price { get; set; }
    }
}
