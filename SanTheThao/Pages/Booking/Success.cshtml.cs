using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SanTheThao.Models;
using SanTheThao.Services;

namespace SanTheThao.Pages.Booking
{
    [Authorize]
    public class SuccessModel : PageModel
    {
        private readonly IBookingService _bookingService;

        public SuccessModel(IBookingService bookingService) => _bookingService = bookingService;

        public Models.Booking? Booking { get; set; }

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Booking = await _bookingService.GetBookingByIdAsync(Id);
            if (Booking == null) return NotFound();
            return Page();
        }
    }
}
