using LifeHub_APIs.Models;
using LifeHub_APIs.Data;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace LifeHub_APIs.services
{
    public class TokenService(IConfiguration configuration, AppDbContext context)
    {
        public async Task<LoginResponse> GetTokenResponse(User user)
        {
            return new LoginResponse
            {
                AccessToken = GenerateAccessToken(user),
                RefreshToken = await GenerateAndSaveRefreshToken(user),
                UserName = user.Username,
                ExpirationDate = new JwtSecurityTokenHandler().ReadJwtToken(GenerateAccessToken(user)).ValidTo
            };
        }
        public async Task<User?> validateRefreshToken(int userId, string refreshToken)
        {
            var user = await context.Users.FindAsync(userId);
            if (user is null || user.RefreshToken != refreshToken
               || user.RefreshTokenExpiryTime <= DateTime.UtcNow)
                return null;
            return user;
        }

        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Username),
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTSettings:SecretKey"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            DateTime tokenTime = DateTime.UtcNow.AddMinutes(15);
            var tokenDescriptor = new JwtSecurityToken
            (
                issuer: configuration["JWTSettings:Issuer"]!,
                audience: configuration["JWTSettings:Audience"]!,
                claims: claims,
                expires: tokenTime,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
        }
       
        private async Task<string> GenerateAndSaveRefreshToken(User user) {
            var refreshToken = GenerateRefreshToken();
            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);
            await context.SaveChangesAsync();
            return refreshToken;
        }

        
        private string GenerateRefreshToken()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }
    }
}
