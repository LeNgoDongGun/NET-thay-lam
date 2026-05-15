using Microsoft.EntityFrameworkCore;
using SanTheThao.Data;
using SanTheThao.DTOs;
using SanTheThao.Models;

namespace SanTheThao.Services
{
    public interface IAuthService
    {
        Task<User?> LoginAsync(string email, string password);
        Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto);
        Task<bool> EmailExistsAsync(string email);
    }

    public class AuthService : IAuthService
    {
        private readonly AppDbContext _db;

        public AuthService(AppDbContext db) => _db = db;

        public async Task<User?> LoginAsync(string email, string password)
        {
            var user = await _db.Users
                .FirstOrDefaultAsync(u => u.Email == email && u.IsActive);

            if (user == null) return null;
            if (!BCrypt.Net.BCrypt.Verify(password, user.PasswordHash)) return null;

            return user;
        }

        public async Task<(bool Success, string Message)> RegisterAsync(RegisterDto dto)
        {
            if (await EmailExistsAsync(dto.Email))
                return (false, "Email này đã được sử dụng");

            var user = new User
            {
                FullName = dto.FullName,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password),
                Role = "Customer",
                IsActive = true,
                CreatedAt = DateTime.Now
            };

            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            return (true, "Đăng ký thành công");
        }

        public async Task<bool> EmailExistsAsync(string email)
            => await _db.Users.AnyAsync(u => u.Email == email);
    }
}