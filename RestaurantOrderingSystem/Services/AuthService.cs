using Microsoft.EntityFrameworkCore;
using RestaurantOrderingSystem.Data;
using RestaurantOrderingSystem.Models.Entities;
using System.Security.Cryptography;
using System.Text;

namespace RestaurantOrderingSystem.Services
{
    public class AuthService : IAuthService
    {
        private readonly ApplicationDbContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthService(ApplicationDbContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<User?> LoginAsync(string username, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username && u.IsActive);

            if (user != null && VerifyPassword(password, user.Password))
            {
                user.LastLoginAt = DateTime.Now;
                await _context.SaveChangesAsync();

                // Session'a kullanıcı bilgilerini kaydet
                var session = _httpContextAccessor.HttpContext?.Session;
                if (session != null)
                {
                    session.SetString("UserId", user.Id.ToString());
                    session.SetString("Username", user.Username);
                    session.SetString("UserRole", user.Role.ToString());
                    session.SetString("FullName", user.FullName ?? user.Username);
                }

                return user;
            }

            return null;
        }

        public async Task<User> RegisterUserAsync(string username, string password, string email, UserRole role = UserRole.User)
        {
            var hashedPassword = HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashedPassword,
                Email = email,
                Role = role,
                CreatedAt = DateTime.Now
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return user;
        }

        public async Task<bool> ChangePasswordAsync(int userId, string oldPassword, string newPassword)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null && VerifyPassword(oldPassword, user.Password))
            {
                user.Password = HashPassword(newPassword);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task LogoutAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            if (session != null)
            {
                session.Clear();
            }
            await Task.CompletedTask;
        }

        public async Task<User?> GetCurrentUserAsync()
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var userIdString = session?.GetString("UserId");

            if (int.TryParse(userIdString, out int userId))
            {
                return await _context.Users
                    .Include(u => u.CurrentTable)
                    .FirstOrDefaultAsync(u => u.Id == userId);
            }

            return null;
        }

        public bool IsInRole(UserRole role)
        {
            var session = _httpContextAccessor.HttpContext?.Session;
            var userRoleString = session?.GetString("UserRole");

            if (Enum.TryParse<UserRole>(userRoleString, out UserRole userRole))
            {
                return userRole == role;
            }

            return false;
        }

        public string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password + "RestaurantSalt2025"));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public bool VerifyPassword(string password, string hash)
        {
            return HashPassword(password) == hash;
        }
    }
} 