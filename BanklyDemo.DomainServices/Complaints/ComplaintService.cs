using AutoMapper;
using BanklyDemo.Core.Complaints;
using BanklyDemo.Core.Complaints.Models;
using BanklyDemo.Core.Data;
using BanklyDemo.Core.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BanklyDemo.DomainServices.Complaints
{
    public class ComplaintService : IComplaintService
    {
        private readonly IComplaintRepository _complaintRepository;
        public ComplaintService(IComplaintRepository complaintRepository)
        {
            _complaintRepository = complaintRepository;
        }
        public async Task<Guid> AddComplaintAsync(ComplaintCreationModel complaint)
        {
            ArgumentGuard.NotNull(complaint, nameof(complaint));

            var complaintEntity = Mapper.Map<ComplaintEntity>(complaint);
            complaintEntity.Id = Guid.NewGuid();
            complaintEntity.CreatedTimeUtc = DateTime.Now;
            complaintEntity.Status = ComplaintStatus.Created;

           return await _complaintRepository.AddAsync(complaintEntity);
        }

        public async Task DeleteComplaintAsync(Guid complaintId)
        {
            ArgumentGuard.NotNull(complaintId, nameof(complaintId));

            var complaintEntity = await _complaintRepository.GetAsync(complaintId);

            await _complaintRepository.DeleteAsync(complaintEntity);
        }

        public async Task<IList<Complaint>> GetAllComplaintAsync()
        {
            var complaintEntities = await _complaintRepository.GetAllAsync();

            return Mapper.Map<IList<Complaint>>(complaintEntities);
        }

        public async Task<Complaint> GetComplaintAsync(Guid complaintId)
        {
            ArgumentGuard.NotNull(complaintId, nameof(complaintId));

            var complaintEntity = await _complaintRepository.GetAsync(complaintId);

            return Mapper.Map<Complaint>(complaintEntity);
        }

        public IList<Complaint> GetUserComplaints(Guid userId)
        {
            ArgumentGuard.NotNull(userId, nameof(userId));

            var complaintEntities = _complaintRepository.GetMany(c => c.UserId == userId);

            return Mapper.Map<IList<Complaint>>(complaintEntities);
        }

        public async Task UpdateComplaintAsync(Guid complaintId, ComplaintUpdateModel complaint)
        {
            ArgumentGuard.NotNull(complaintId, nameof(complaintId));
            ArgumentGuard.NotNull(complaint, nameof(complaint));

            var dbComplaint = await _complaintRepository.GetAsync(complaintId);

            var complaintEntity = Mapper.Map<ComplaintEntity>(complaint);

            complaintEntity.Id = complaintId;
            complaintEntity.Status = dbComplaint.Status;
            complaintEntity.UserId = dbComplaint.UserId;
            complaintEntity.ProductId = dbComplaint.ProductId;
            complaintEntity.CreatedTimeUtc = dbComplaint.CreatedTimeUtc;

            await _complaintRepository.UpdateAsync(complaintEntity);
        }

        public async Task UpdateComplaintStatus(ComplaintStatusUpdateModel model)
        {
            ArgumentGuard.NotNull(model.ComplaintId, nameof(model.ComplaintId));
            ArgumentGuard.NotNull(model.Status, nameof(model.Status));

            var complaintEntity = await _complaintRepository.GetAsync(model.ComplaintId);

            complaintEntity.Status = model.Status;

            await _complaintRepository.UpdateAsync(complaintEntity);
        }
    }
}
