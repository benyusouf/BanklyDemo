using BanklyDemo.Core.Complaints.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.Core.Complaints
{
    public interface IComplaintService
    {
        Task<Complaint> GetComplaintAsync(Guid complaintId);

        Task<IList<Complaint>> GetAllComplaintAsync();

        IList<Complaint> GetUserComplaints(Guid userId);

        Task<Guid> AddComplaintAsync(ComplaintCreationModel complaint);

        Task UpdateComplaintAsync(Guid complaintId, ComplaintUpdateModel complaint);

        Task DeleteComplaintAsync(Guid complaintId);

        Task UpdateComplaintStatus(ComplaintStatusUpdateModel model);
    }
}
