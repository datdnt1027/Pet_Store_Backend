using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using pet_store_backend.application.Services.Authentication;
using pet_store_backend.contracts.Authentication;

namespace pet_store_backend.api.Controllers
{
    [Route("auth")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService;
        public AuthenticationController(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;
        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(RegisterRequest request)
        {
            var authResult = _authenticationService.Register(request.FirstName, request.LastName, request.Email, request.Password);
            var authResponse = new AuthenticationResponse(authResult.Id, authResult.FirstName, authResult.LastName, authResult.Email ,authResult.Token);
            return Ok(authResponse);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(LoginRequest request)
        {
            var authResult = _authenticationService.Login(request.Email, request.Password);
            var authResponse = new AuthenticationResponse(authResult.Id, authResult.FirstName, authResult.LastName, authResult.Email, authResult.Token);
            return Ok(authResponse);
        }
    }
}
