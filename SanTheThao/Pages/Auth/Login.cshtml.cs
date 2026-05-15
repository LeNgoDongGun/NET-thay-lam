using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SanTheThao.DTOs;
using SanTheThao.Services;
using System.Security.Claims;

namespace SanTheThao.Pages.Auth
{
    public class LoginModel : PageModel
    {
        private readonly IAuthService _auth;

        public LoginModel(IAuthService auth) => _auth = auth;

        [BindProperty]
        public LoginDto Input { get; set; } = new();

        public string? ErrorMessage { get; set; }

        public IActionResult OnGet()
        {
            if (User.Identity?.IsAuthenticated == true)
                return RedirectToPage("/Index");
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();

            var user = await _auth.LoginAsync(Input.Email, Input.Password);

            if (user == null)
            {
                ErrorMessage = "Email hoặc mật khẩu không đúng";
                return Page();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name,           user.FullName),
                new Claim(ClaimTypes.Email,          user.Email),
                new Claim(ClaimTypes.Role,           user.Role),
            };

            var identity  = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                principal,
                new AuthenticationProperties { IsPersistent = Input.RememberMe }
            );

            return user.Role == "Admin"
                ? RedirectToPage("/Admin/Dashboard")
                : RedirectToPage("/Index");
        }
    }
}
