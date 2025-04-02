using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using JOIEnergy.Services.Auth;

namespace JOIEnergy.Controllers
{
    [ApiController]
    [Route("auth")]
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        [SwaggerOperation(
            Summary = "Authenticate user",
            Description = "Authenticates user and returns JWT token",
            OperationId = "Login",
            Tags = new[] { "Authentication" }
        )]
        [SwaggerResponse(200, "Authentication successful")]
        [SwaggerResponse(401, "Invalid credentials")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var response = await _authService.ValidateCredentialsAsync(request.Username, request.Password);
            return StatusCode(response.StatusCode, response);
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
} 