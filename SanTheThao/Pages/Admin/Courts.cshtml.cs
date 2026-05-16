using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SanTheThao.Data;
using SanTheThao.Models;

namespace SanTheThao.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class CourtsModel : PageModel
    {
        private readonly AppDbContext _db;

        public CourtsModel(AppDbContext db) => _db = db;

        public List<Court> Courts { get; set; } = new();
        public List<SportType> SportTypes { get; set; } = new();

        [BindProperty] public string CourtName { get; set; } = string.Empty;
        [BindProperty] public int SportTypeId { get; set; }
        [BindProperty] public decimal PricePerHour { get; set; }
        [BindProperty] public string Description { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Courts = await _db.Courts
                .Include(c => c.SportType)
                .OrderBy(c => c.SportTypeId).ThenBy(c => c.Name)
                .ToListAsync();
            SportTypes = await _db.SportTypes.ToListAsync();
        }

        // Thêm sân mới
        public async Task<IActionResult> OnPostAddAsync()
        {
            var court = new Court
            {
                Name         = CourtName,
                SportTypeId  = SportTypeId,
                PricePerHour = PricePerHour,
                Description  = Description,
                IsActive     = true
            };
            _db.Courts.Add(court);
            await _db.SaveChangesAsync();
            TempData["Message"] = $"Đã thêm sân \"{CourtName}\"";
            return RedirectToPage();
        }

        // Ẩn / Hiện sân
        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var court = await _db.Courts.FindAsync(id);
            if (court != null)
            {
                court.IsActive = !court.IsActive;
                await _db.SaveChangesAsync();
                TempData["Message"] = court.IsActive
                    ? $"Đã mở lại sân \"{court.Name}\""
                    : $"Đã ẩn sân \"{court.Name}\"";
            }
            return RedirectToPage();
        }
    }
}
