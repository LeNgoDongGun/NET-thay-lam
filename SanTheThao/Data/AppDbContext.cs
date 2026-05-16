using Microsoft.EntityFrameworkCore;
using SanTheThao.Models;

namespace SanTheThao.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<SportType> SportTypes { get; set; }
        public DbSet<Court> Courts { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Decimal precision
            modelBuilder.Entity<Court>()
                .Property(c => c.PricePerHour)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Booking>()
                .Property(b => b.TotalPrice)
                .HasColumnType("decimal(18,2)");

            // ===== SEED DATA =====

            // Sport Types
            modelBuilder.Entity<SportType>().HasData(
                new SportType { Id = 1, Name = "Bóng đá", Icon = "⚽", Description = "Sân bóng đá mini 5v5, 7v7" },
                new SportType { Id = 2, Name = "Cầu lông", Icon = "🏸", Description = "Sân cầu lông tiêu chuẩn" },
                new SportType { Id = 3, Name = "Bóng chuyền", Icon = "🏐", Description = "Sân bóng chuyền trong nhà" },
                new SportType { Id = 4, Name = "Bóng rổ", Icon = "🏀", Description = "Sân bóng rổ 3v3 và 5v5" },
                new SportType { Id = 5, Name = "Pickleball", Icon = "🎾", Description = "Sân Pickleball tiêu chuẩn" }
            );

            // Courts
            modelBuilder.Entity<Court>().HasData(
                // Bóng đá
                new Court { Id = 1, Name = "Sân Bóng Đá A1", SportTypeId = 1, PricePerHour = 200000 },
                new Court { Id = 2, Name = "Sân Bóng Đá A2", SportTypeId = 1, PricePerHour = 200000 },
                new Court { Id = 3, Name = "Sân Bóng Đá B1", SportTypeId = 1, PricePerHour = 250000 },
                // Cầu lông
                new Court { Id = 4, Name = "Sân Cầu Lông 1", SportTypeId = 2, PricePerHour = 80000 },
                new Court { Id = 5, Name = "Sân Cầu Lông 2", SportTypeId = 2, PricePerHour = 80000 },
                // Bóng chuyền
                new Court { Id = 6, Name = "Sân Bóng Chuyền 1", SportTypeId = 3, PricePerHour = 150000 },
                // Bóng rổ
                new Court { Id = 7, Name = "Sân Bóng Rổ 1", SportTypeId = 4, PricePerHour = 120000 },
                new Court { Id = 8, Name = "Sân Bóng Rổ 2", SportTypeId = 4, PricePerHour = 120000 },
                // Pickleball
                new Court { Id = 9, Name = "Sân Pickleball 1", SportTypeId = 5, PricePerHour = 100000 },
                new Court { Id = 10, Name = "Sân Pickleball 2", SportTypeId = 5, PricePerHour = 100000 }
            );

            // Admin mặc định (password: Admin@123)
            // PasswordHash được tạo sẵn tĩnh, không dùng BCrypt động
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    FullName = "Admin",
                    Email = "admin@santhethao.com",
                    PhoneNumber = "0900000000",
                    PasswordHash = "$2a$11$43uD9/LEK4I160H3wmYi7urqfovNxCADfQxW03.5WDaxu1zQSwSMS",
                    Role = "Admin",
                    IsActive = true,
                    CreatedAt = new DateTime(2025, 1, 1)
                }
            );
        }
    }
}