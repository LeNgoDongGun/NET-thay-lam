using Microsoft.AspNetCore.Mvc.RazorPages;
using SanTheThao.Models;
using SanTheThao.Services;

namespace SanTheThao.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ICourtService _courtService;

        public IndexModel(ICourtService courtService) => _courtService = courtService;

        public List<SportType> SportTypes { get; set; } = new();

        public async Task OnGetAsync()
        {
            SportTypes = await _courtService.GetAllSportTypesAsync();
        }
    }
}