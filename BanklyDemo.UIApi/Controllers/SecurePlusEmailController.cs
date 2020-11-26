using BanklyDemo.Core.Complaints;
using BanklyDemo.UIApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("api/emails")]
    [ApiController]
    [AllowAnonymous]
    public class SecurePlusEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public SecurePlusEmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendMail([FromForm] MailRequest request)
        {
            try
            {
                await _emailService.SendEmailAsync(request);
                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
