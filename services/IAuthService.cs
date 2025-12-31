using LifeHub_APIs.Dtos;
using LifeHub_APIs.Models;

namespace LifeHub_APIs.services
{
    public interface IAuthService
    {
        Task<User?> RegisterAsync(LoginDto user);
        Task<LoginResponse?> LoginAsync(LoginDto user);
        Task<LoginResponse?> RefreshTokensAsync(int userId, string refreshToken);
        Task<User?> GetUserAsync(int id);
    }
}
