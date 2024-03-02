using BusinessLayer.ResquestModels;
using BusinessLayer.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace OrchidAuction_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService _emailService;
        public EmailController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        [HttpPost("SendOTPEmail")]
        public IActionResult SendEmail(EmailRequestModel request)
        {
            if (_emailService.SendEmail(request))
            {
                return Ok();
            }
            return BadRequest();
        }
    }
}
