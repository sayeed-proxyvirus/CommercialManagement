using Microsoft.AspNetCore.Mvc;
using CommercialManagement.Models.ViewModels;
using CommercialManagement.Services;
using System.Security.Cryptography;
using System.Text;
using CommercialManagement.Models.Users;
using CommercialManagement.Services.Logins.LoginServices;

namespace CommercialManagement.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserService _userService;
        private readonly ILogger<AccountController> _logger;

        public AccountController(UserService userService, ILogger<AccountController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If already logged in, redirect to home
            if (HttpContext.Session.GetInt32("UserId") != null)
            {
                return RedirectToAction("Index", "Home");
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(LoginViewModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Get user by Username
                var user = _userService.GetUserByUsername(model.UserName);

                // NULL CHECK #1 - User might not exist
                if (user == null)
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    _logger.LogWarning("Failed login attempt for username: {Username}", model.UserName);
                    return View(model);
                }

                // NULL CHECK #2 - user.Upassword might be null
                if (string.IsNullOrEmpty(user.Upassword))
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    _logger.LogWarning("User {Username} has null password in database", model.UserName);
                    return View(model);
                }

                // Verify password hash
                if (!VerifyPassword(model.Password, user.Upassword))
                {
                    TempData["ErrorMessage"] = "Invalid username or password.";
                    _logger.LogWarning("Failed login attempt (wrong password) for username: {Username}", model.UserName);
                    return View(model);
                }

                // NULL CHECK #3 - Set session with null-coalescing
                HttpContext.Session.SetInt32("UserId", user.UserID);
                HttpContext.Session.SetString("UserName", user.UserName ?? "");
                HttpContext.Session.SetString("FullName", user.FullName ?? "");
                HttpContext.Session.SetString("Email", user.Email ?? "");

                _logger.LogInformation("User {Username} logged in successfully", user.UserName);

                // Redirect to home or return URL
                if (!string.IsNullOrEmpty(model.ReturnUrl) && Url.IsLocalUrl(model.ReturnUrl))
                {
                    return Redirect(model.ReturnUrl);
                }

                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during login for username: {Username}", model.UserName);
                TempData["ErrorMessage"] = "An error occurred during login. Please try again.";
                return View(model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Logout()
        {
            try
            {
                var username = HttpContext.Session.GetString("UserName");
                HttpContext.Session.Clear();

                _logger.LogInformation("User {Username} logged out", username);
                TempData["SuccessMessage"] = "You have been logged out successfully.";
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error during logout");
                return RedirectToAction("Login");
            }
        }

        [HttpGet]
        public IActionResult ChangePassword()
        {
            if (HttpContext.Session.GetInt32("UserId") == null)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ChangePassword(ChangePasswordViewModel model)
        {
            try
            {
                var userId = HttpContext.Session.GetInt32("UserId");

                if (userId == null)
                {
                    return RedirectToAction("Login");
                }
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                var user = _userService.GetUserById(userId.Value);

                if (user == null)
                {
                    TempData["ErrorMessage"] = "User not found.";
                    return View(model);
                }

                // Verify current password
                if (!VerifyPassword(model.CurrentPassword, user.Upassword))
                {
                    TempData["ErrorMessage"] = "Current password is incorrect.";
                    return View(model);
                }

                // Hash new password
                user.Upassword = HashPassword(model.NewPassword);

                // Update user
                if (_userService.UpdateUser(user))
                {
                    TempData["SuccessMessage"] = "Password changed successfully.";
                    _logger.LogInformation("User {UserId} changed password", userId);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["ErrorMessage"] = "Failed to change password. Please try again.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error changing password");
                TempData["ErrorMessage"] = "An error occurred. Please try again.";
                return View(model);
            }
        }

        // Hash password using SHA256
        public static string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }

        // Verify password against hash
        public static bool VerifyPassword(string password, string hash)
        {
            string hashOfInput = HashPassword(password);
            return StringComparer.OrdinalIgnoreCase.Compare(hashOfInput, hash) == 0;
        }
    }
}