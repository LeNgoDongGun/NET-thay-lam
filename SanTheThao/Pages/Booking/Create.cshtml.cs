using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SanTheThao.DTOs;
using SanTheThao.Models;
using SanTheThao.Services;
using System.Security.Claims;

namespace SanTheThao.Pages.Booking
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly IBookingService _bookingService;

        public CreateModel(ICourtService courtService, IBookingService bookingService)
        {
            _courtService   = courtService;
            _bookingService = bookingService;
        }

        public Court? Court { get; set; }
        public decimal TotalPrice { get; set; }
        public string? ErrorMessage { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CourtId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Date { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string Start { get; set; } = string.Empty;

        [BindProperty(SupportsGet = true)]
        public string End { get; set; } = string.Empty;

        [BindProperty]
        public string? Note { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Court = await _courtService.GetCourtByIdAsync(CourtId);
            if (Court == null) return NotFound();

            var startTime = TimeOnly.Parse(Start);
            var endTime   = TimeOnly.Parse(End);
            var hours     = (decimal)(endTime - startTime).TotalHours;
            TotalPrice    = Court.PricePerHour * hours;

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Court = await _courtService.GetCourtByIdAsync(CourtId);
            if (Court == null) return NotFound();

            var bookingDate = DateOnly.Parse(Date);
            var startTime   = TimeOnly.Parse(Start);
            var endTime     = TimeOnly.Parse(End);

            // Kiểm tra còn trống không
            var available = await _bookingService.IsCourtAvailableAsync(CourtId, bookingDate, startTime, endTime);
            if (!available)
            {
                ErrorMessage = "Khung giờ này đã được đặt, vui lòng chọn giờ khác!";
                var hours  = (decimal)(endTime - startTime).TotalHours;
                TotalPrice = Court.PricePerHour * hours;
                return Page();
            }

            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            var dto = new BookingDto
            {
                CourtId     = CourtId,
                UserId      = userId,
                BookingDate = bookingDate,
                StartTime   = startTime,
                EndTime     = endTime,
                Note        = Note
            };

            var booking = await _bookingService.CreateBookingAsync(dto);

            return RedirectToPage("/Booking/Success", new { id = booking.Id });
        }
    }
}
