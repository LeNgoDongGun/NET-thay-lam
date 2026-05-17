using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SanTheThao.Data;
using SanTheThao.Models;

namespace SanTheThao.Pages.News
{
    public class DetailModel : PageModel
    {
        private readonly AppDbContext _db;

        public DetailModel(AppDbContext db) => _db = db;

        public NewsPost? Post { get; set; }
        public List<NewsPost> RelatedPosts { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string Slug { get; set; } = string.Empty;

        public async Task<IActionResult> OnGetAsync()
        {
            Post = await _db.NewsPosts
                .Include(p => p.Author)
                .FirstOrDefaultAsync(p => p.Slug == Slug && p.IsPublished);

            if (Post == null) return NotFound();

            // Bài viết liên quan cùng danh mục
            RelatedPosts = await _db.NewsPosts
                .Where(p => p.Category == Post.Category && p.Id != Post.Id && p.IsPublished)
                .OrderByDescending(p => p.CreatedAt)
                .Take(3)
                .ToListAsync();

            return Page();
        }
    }
}
