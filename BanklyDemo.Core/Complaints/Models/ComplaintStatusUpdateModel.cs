using System;
using System.Collections.Generic;
using System.Text;

namespace BanklyDemo.Core.Complaints.Models
{
    public class ComplaintStatusUpdateModel
    {
        public Guid ComplaintId { get; set; }

        public ComplaintStatus Status { get; set; }
    }
}
