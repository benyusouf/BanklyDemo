using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BanklyDemo.UIApi.Controllers
{
    [Route("api/secrets")]
    [ApiController]
    [Authorize]
    public class SecretsController : ControllerBase
    {

        [HttpGet("message")]
        public string SecretMessage()
        {
            return "this is a secret message";
        }
    }
}