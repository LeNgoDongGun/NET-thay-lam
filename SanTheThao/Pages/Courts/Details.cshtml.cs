using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SanTheThao.Models;
using SanTheThao.Services;

namespace SanTheThao.Pages.Courts
{
    public class DetailModel : PageModel
    {
        private readonly ICourtService _courtService;
        private readonly IBookingService _bookingService;

        public DetailModel(ICourtService courtService, IBookingService bookingService)
        {
            _courtService = courtService;
            _bookingService = bookingService;
        }

        public Court? Court { get; set; }
        public List<SanTheThao.Models.Booking> BookedSlots { get; set; } = new();

        // Khung giờ hoạt động: 6:00 - 22:00
        public List<TimeOnly> TimeSlots { get; set; } = Enumerable
            .Range(6, 16)
            .Select(h => new TimeOnly(h, 0))
            .ToList();

        [BindProperty(SupportsGet = true)]
        public int Id { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateOnly SelectedDate { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            if (SelectedDate == default)
                SelectedDate = DateOnly.FromDateTime(DateTime.Today);

            Court = await _courtService.GetCourtByIdAsync(Id);
            if (Court == null) return NotFound();

            BookedSlots = await _bookingService.GetBookedSlotsAsync(Id, SelectedDate);
            return Page();
        }

        public bool IsSlotBooked(TimeOnly start)
        {
            return BookedSlots.Any(b =>
                b.StartTime <= start && b.EndTime > start);
        }

    }
}