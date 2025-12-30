using LifeHub_APIs.Data;
using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace LifeHub_APIs.services
{
    public class AuthService(AppDbContext context, TokenService tokens): IAuthService
    {
        public async Task<LoginResponse?> LoginAsync(LoginDto request)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Username == request.UserName);
            if (user is null)
                return null;
            var requestedPassword = new PasswordHasher<User>().VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if (requestedPassword == PasswordVerificationResult.Failed)
                return null;
            return await tokens.GetTokenResponse(user);
        }
        public async Task<LoginResponse> RefreshTokensAsync(int userId, string refreshToken)
        {
            return new LoginResponse();
        }
    }
}
