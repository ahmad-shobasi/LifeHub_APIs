using Azure;
using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models;
using LifeHub_APIs.services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LifeHub_APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController(IAuthService service) : ControllerBase
    {
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginDto request)
        {
            var response = await service.LoginAsync(request);
            if (response is null)
                return BadRequest("user name or password incorrect");
            return Ok(response);
        }
        public async Task<ActionResult<LoginResponse>> RefreshTokens(int userId, string refreshToken)
        {
            var response = service.RefreshTokensAsync(userId, refreshToken);
            if (response is null)
                return BadRequest();
            return Ok(response);
        }
    }
}
