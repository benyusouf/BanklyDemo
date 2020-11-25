using BanklyDemo.Core.Complaints;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("api/emails")]
    [ApiController]
    [Authorize]
    public class SecurePlusEmailController : ControllerBase
    {
        public SecurePlusEmailController(IComplaintService complaintService)
        {
        }
    }
}
