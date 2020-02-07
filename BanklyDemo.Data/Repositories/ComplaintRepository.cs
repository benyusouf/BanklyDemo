using BanklyDemo.Core.Complaints.Models;
using BanklyDemo.Core.Data;
using System;
using System.Collections.Generic;
using System.Text;

namespace BanklyDemo.Data.Repositories
{
    internal class ComplaintRepository : DataRepository<ComplaintEntity>, IComplaintRepository
    {
        private readonly BanklyDemoDbContext _dbContext;

        public ComplaintRepository(BanklyDemoDbContext dbContext)
            : base(dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
