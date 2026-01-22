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
        [HttpPost("Register")]
        public async Task<ActionResult<UserResponseDto?>> Register(LoginDto request)
        {
            var user = await service.RegisterAsync(request);
            var userDto = user is null ? null : new UserResponseDto
            {
                Id = user.Id,
                UserName = user.Username
            };
            return userDto is null ? BadRequest("User name already exist") : Ok(user);
        }
        [HttpPost("Login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginDto request)
        {
            var response = await service.LoginAsync(request);
            if (response is null)
                return BadRequest("user name or password incorrect");
            return Ok(response);
        }
        [HttpPost("Refresh-token")]
        public async Task<ActionResult<LoginResponse>> RefreshTokens(RefreshTokenRequest request)
        {
            var response = await service.RefreshTokensAsync(request.UserId,request.RefreshToken);
            if (response is null)
                return BadRequest();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User?>> getUser(int id)
        {
            return await service.GetUserAsync(id);
        }
    }
}
