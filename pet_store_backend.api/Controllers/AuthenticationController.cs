using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Authentication.Commands.ForgotPassword;
using pet_store_backend.application.Authentication.Commands.Register;
using pet_store_backend.application.Authentication.Commands.ResetPassword;
using pet_store_backend.application.Authentication.Commands.Verify;
using pet_store_backend.application.Authentication.Queries.Login;
using pet_store_backend.contracts;
using pet_store_backend.contracts.Authentication;

namespace pet_store_backend.api.Controllers;

[Route("auth")]
[AllowAnonymous]
public class AuthenticationController : ApiController
{
    private readonly ISender _mediator;
    private readonly IMapper _mapper;
    public AuthenticationController(ISender mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    [HttpPost]
    [Route("register")]
    public async Task<IActionResult> Register(RegisterRequest request)
    {
        var command = _mapper.Map<RegisterCommand>(request);

        var authResult = await _mediator.Send(command);

        return authResult.Match(authResult => Ok(_mapper.Map<MessageResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var query = _mapper.Map<LoginQuery>(request);

        var authResult = await _mediator.Send(query);

        return authResult.Match(authResult => Ok(_mapper.Map<AuthenticationResponse>(authResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("verify")]
    public async Task<IActionResult> Verify([FromQuery] string verificationToken)
    {
        var query = new VerifyCommand(verificationToken);

        var verifyResult = await _mediator.Send(query);

        return verifyResult.Match(verifyResult => Ok(_mapper.Map<MessageResponse>(verifyResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("forgot-password")]
    public async Task<IActionResult> ForgotPassword([FromQuery] string email)
    {
        var command = new ForgotPasswordCommand(email);
        var resetPasswordResult = await _mediator.Send(command);

        return resetPasswordResult.Match(resetResult => Ok(_mapper.Map<MessageResponse>(resetResult)),
            errors => Problem(errors));
    }

    [HttpPost]
    [Route("reset-password")]
    public async Task<IActionResult> ResetPassword(PasswordResetRequest request)
    {
        var command = _mapper.Map<ResetPasswordCommand>(request);
        var resetPasswordResult = await _mediator.Send(command);

        return resetPasswordResult.Match(resetResult => Ok(_mapper.Map<MessageResponse>(resetResult)),
            errors => Problem(errors));
    }
}

