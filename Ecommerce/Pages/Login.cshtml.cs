using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Ecommerce.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        public string Password { get; set; }
        public string Message { get; set; }
        public void OnGet()
        {
            Message = "";
        }
        public IActionResult OnPost()
        {
            if (Username == "admin" && Password == "123")
            {
                return RedirectToPage("/Index");
            }
            else
            {
                Message = "Sai tên đăng nhập hoặc mật khẩu.";
                return Page();
            }
        }
    }
}
