using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string username, string password);
        Task<User> RegisterUserAsync(string username, string password, string email, UserRole role = UserRole.User);
        Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword);
        Task LogoutAsync();
        Task<User?> GetCurrentUserAsync();
        bool IsInRole(UserRole role);
        string HashPassword(string password);
        bool VerifyPassword(string password, string hash);
    }
} 