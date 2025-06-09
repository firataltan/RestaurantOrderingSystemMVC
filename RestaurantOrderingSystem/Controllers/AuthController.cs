using Microsoft.AspNetCore.Mvc;
using RestaurantOrderingSystem.Services;
using RestaurantOrderingSystem.Models.Entities;

namespace RestaurantOrderingSystem.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        public IActionResult Login()
        {
            // Eğer zaten giriş yapmışsa ana sayfaya yönlendir
            if (!string.IsNullOrEmpty(HttpContext.Session.GetString("UserId")))
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string username, string password, string returnUrl = null)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                TempData["Error"] = "Kullanıcı adı ve şifre gereklidir.";
                return View();
            }

            var user = await _authService.LoginAsync(username, password);
            if (user != null)
            {
                TempData["Success"] = $"Hoş geldiniz, {user.FullName ?? user.Username}!";
                
                // Role'e göre yönlendirme
                if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                {
                    return Redirect(returnUrl);
                }

                return user.Role switch
                {
                    UserRole.Admin => RedirectToAction("Index", "Admin"),
                    UserRole.Kitchen => RedirectToAction("Orders", "Kitchen"),
                    UserRole.User => RedirectToAction("Select", "Table"),
                    _ => RedirectToAction("Index", "Home")
                };
            }

            TempData["Error"] = "Geçersiz kullanıcı adı veya şifre.";
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await _authService.LogoutAsync();
            TempData["Success"] = "Başarıyla çıkış yaptınız.";
            return RedirectToAction("Login");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(string username, string password, string email, string fullName)
        {
            try
            {
                var user = await _authService.RegisterUserAsync(username, password, email, UserRole.User);
                TempData["Success"] = "Kayıt başarılı! Giriş yapabilirsiniz.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Kayıt sırasında hata oluştu: " + ex.Message;
                return View();
            }
        }
    }
} 