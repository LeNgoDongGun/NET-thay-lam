using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ECommerce.Models;

namespace Ecommerce.Pages
{
    public class RegisterModel : PageModel
    {
        [BindProperty]
        public UserModel User { get; set; }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("Success");
        }
    }
}
