using BanklyDemo.Core.Complaints;
using BanklyDemo.UIApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("api/emails")]
    [ApiController]
    [AllowAnonymous]
    public class SecurePlusEmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        private readonly TwilloCredentials _twillo;
        public SecurePlusEmailController(IEmailService emailService, IOptions<TwilloCredentials> twillio)
        {
            _emailService = emailService;
            _twillo = twillio.Value;
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

        [HttpGet("sms")]
        public IActionResult SendSMS(String phoneNumber, string name)
        {
            try
            {
                TwilioClient.Init(_twillo.Sid, _twillo.Auth);

                var message = MessageResource.Create(
                    body: $"Dear {name}, your SecurePlus account has been created successfully",
                    from: new Twilio.Types.PhoneNumber(_twillo.Phone),
                    to: new Twilio.Types.PhoneNumber($"+234{phoneNumber.Substring(1)}")
                );

                Console.WriteLine(message.Sid);

                return Ok();
            }
            catch (Exception ex)
            {
                throw;
            }

        }


    }
}
