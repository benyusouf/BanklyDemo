using BanklyDemo.Core.Complaints;
using BanklyDemo.Core.Complaints.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("api/complaints")]
    [ApiController]
    [Authorize]
    public class ComplaintsController: ControllerBase
    {
        private readonly IComplaintService _complaintService;
        public ComplaintsController(IComplaintService complaintService)
        {
            _complaintService = complaintService;
        }

        [HttpGet("{complaintId:Guid}/summary")]
        public async Task<Complaint> GetComplaintAsync(Guid complaintId)
        {
            return await _complaintService.GetComplaintAsync(complaintId);
        }

        [HttpGet("summaries")]
        public async Task<IList<Complaint>> GetAllComplaintsAsync()
        {
            return await _complaintService.GetAllComplaintAsync();
        }

        [HttpGet("{userId:Guid}/complaints")]
        public IList<Complaint> GetUserComplaints(Guid userId)
        {
            return _complaintService.GetUserComplaints(userId);
        }

        [HttpPost("add")]
        public async Task<Guid> AddComplaintAsync([FromForm]ComplaintCreationModel complaint)
        {
            return await _complaintService.AddComplaintAsync(complaint);
        }

        [HttpPut("{complaintId:Guid}/update")]
        public async Task UpdateComplaintAsync(Guid complaintId, [FromForm]ComplaintUpdateModel complaint)
        {
            await _complaintService.UpdateComplaintAsync(complaintId, complaint);
        }

        [HttpDelete("{complaintId:Guid}")]
        public async Task DeleteComplaintAsync(Guid complaintId)
        {
            await _complaintService.DeleteComplaintAsync(complaintId);
        }

        [HttpPut("/update_status")]
        public async Task UpdateComplaintStatus([FromForm]ComplaintStatusUpdateModel model)
        {
            await _complaintService.UpdateComplaintStatus(model);
        }
    }
}
