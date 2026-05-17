using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SanTheThao.Data;
using SanTheThao.Models;
using System.Security.Claims;

namespace SanTheThao.Pages.Admin
{
    [Authorize(Roles = "Admin")]
    public class NewsModel : PageModel
    {
        private readonly AppDbContext _db;

        public NewsModel(AppDbContext db) => _db = db;

        public List<NewsPost> Posts { get; set; } = new();

        [BindProperty] public string Title { get; set; } = string.Empty;
        [BindProperty] public string Slug { get; set; } = string.Empty;
        [BindProperty] public string Summary { get; set; } = string.Empty;
        [BindProperty] public string PostContent { get; set; } = string.Empty;
        [BindProperty] public string Category { get; set; } = string.Empty;

        public async Task OnGetAsync()
        {
            Posts = await _db.NewsPosts
                .Include(p => p.Author)
                .OrderByDescending(p => p.CreatedAt)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            var authorId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            // Tự tạo slug từ title nếu để trống
            var slug = string.IsNullOrEmpty(Slug)
                ? Title.ToLower()
                    .Replace(" ", "-")
                    .Replace("đ", "d")
                    .Replace("á", "a").Replace("à", "a").Replace("ả", "a").Replace("ã", "a").Replace("ạ", "a")
                    .Replace("ă", "a").Replace("ắ", "a").Replace("ằ", "a").Replace("ẳ", "a").Replace("ẵ", "a").Replace("ặ", "a")
                    .Replace("â", "a").Replace("ấ", "a").Replace("ầ", "a").Replace("ẩ", "a").Replace("ẫ", "a").Replace("ậ", "a")
                    .Replace("é", "e").Replace("è", "e").Replace("ẻ", "e").Replace("ẽ", "e").Replace("ẹ", "e")
                    .Replace("ê", "e").Replace("ế", "e").Replace("ề", "e").Replace("ể", "e").Replace("ễ", "e").Replace("ệ", "e")
                    .Replace("í", "i").Replace("ì", "i").Replace("ỉ", "i").Replace("ĩ", "i").Replace("ị", "i")
                    .Replace("ó", "o").Replace("ò", "o").Replace("ỏ", "o").Replace("õ", "o").Replace("ọ", "o")
                    .Replace("ô", "o").Replace("ố", "o").Replace("ồ", "o").Replace("ổ", "o").Replace("ỗ", "o").Replace("ộ", "o")
                    .Replace("ơ", "o").Replace("ớ", "o").Replace("ờ", "o").Replace("ở", "o").Replace("ỡ", "o").Replace("ợ", "o")
                    .Replace("ú", "u").Replace("ù", "u").Replace("ủ", "u").Replace("ũ", "u").Replace("ụ", "u")
                    .Replace("ư", "u").Replace("ứ", "u").Replace("ừ", "u").Replace("ử", "u").Replace("ữ", "u").Replace("ự", "u")
                    .Replace("ý", "y").Replace("ỳ", "y").Replace("ỷ", "y").Replace("ỹ", "y").Replace("ỵ", "y")
                : Slug;

            var post = new NewsPost
            {
                Title = Title,
                Slug = slug,
                Summary = Summary,
                Content = PostContent,
                Category = Category,
                AuthorId = authorId,
                IsPublished = true,
                CreatedAt = DateTime.Now
            };

            _db.NewsPosts.Add(post);
            await _db.SaveChangesAsync();
            TempData["Success"] = $"Đã đăng bài \"{Title}\"";
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostToggleAsync(int id)
        {
            var post = await _db.NewsPosts.FindAsync(id);
            if (post != null)
            {
                post.IsPublished = !post.IsPublished;
                await _db.SaveChangesAsync();
                TempData["Success"] = post.IsPublished ? "Đã đăng bài" : "Đã ẩn bài";
            }
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var post = await _db.NewsPosts.FindAsync(id);
            if (post != null)
            {
                _db.NewsPosts.Remove(post);
                await _db.SaveChangesAsync();
                TempData["Success"] = "Đã xóa bài viết";
            }
            return RedirectToPage();
        }
    }
}