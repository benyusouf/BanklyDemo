using System;

namespace BanklyDemo.Core.Complaints.Models
{
    public class ComplaintCreationModel
    {
        public Guid UserId { get; set; }

        public Guid ProductId { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }

    }
}
