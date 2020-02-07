using BanklyDemo.Core.Products.Models;
using System;

namespace BanklyDemo.Core.Complaints.Models
{
    public class Complaint
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }

        public ProductEntity Product { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

        public DateTime CreatedTimeUtc { get; set; }

        public ComplaintStatus Status { get; set; }
    }
}
