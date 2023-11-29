using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Common.Interfaces.Email;
using pet_store_backend.domain.Entities;

namespace pet_store_backend.api.Controllers;

[Route("[controller]")]
[AllowAnonymous]
public class EmailController : ApiController
{
    private readonly IEmailService _emailService;
    public EmailController(IEmailService emailService)
    {
        _emailService = emailService;
    }
    [HttpGet]
    public IActionResult TestEmail()
    {
        var message = new Message(new string[] { "20110629@student.hcmute.edu.vn" }, "Test", "This is Test Email");

        _emailService.SendEmail(message);

        return Problem(statusCode: StatusCodes.Status200OK,
            title: "Email Sent");
    }
}