using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Runtime.Intrinsics.X86;

namespace DataAccess.Data
{
    public static class DbInitializer
    {
        public static void SeedUsers(this ModelBuilder modelBuilder)
        {
            var hasher = new PasswordHasher<User>();

            var users = new List<User>
            {
                new User
                {
                    Id = "user1",
                    UserName = "john.doe",
                    NormalizedUserName = "JOHN.DOE",
                    Email = "john.doe@example.com",
                    NormalizedEmail = "JOHN.DOE@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Name = "John",
                    Surname = "Doe",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null!, "Password123!")
                },
                new User
                {
                    Id = "user2",
                    UserName = "jane.smith",
                    NormalizedUserName = "JANE.SMITH",
                    Email = "jane.smith@example.com",
                    NormalizedEmail = "JANE.SMITH@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Name = "Jane",
                    Surname = "Smith",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null!, "Password123!")
                },
                new User
                {
                    Id = "admin",
                    UserName = "admin",
                    NormalizedUserName = "ADMIN",
                    Email = "admin@example.com",
                    NormalizedEmail = "ADMIN@EXAMPLE.COM",
                    EmailConfirmed = true,
                    Name = "Admin",
                    Surname = "User",
                    CreatedAt = DateTime.UtcNow,
                    PasswordHash = hasher.HashPassword(null!, "AdminPassword123!")
                }
            };

            modelBuilder.Entity<User>().HasData(users);
        }

        public static void SeedCategories(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(new List<Category>()
            {
                new() { Id = 1, Name = "Electronics" },
                new() { Id = 2, Name = "Sport" },
                new() { Id = 3, Name = "Fashion" },
                new() { Id = 4, Name = "Home & Garden" },
                new() { Id = 5, Name = "Transport" },
                new() { Id = 6, Name = "Toys & Hobbies" },
                new() { Id = 7, Name = "Musical Instruments" },
                new() { Id = 8, Name = "Art" },
                new() { Id = 9, Name = "Other" }
            });
        }

        public static void SeedAdvertisements(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Advertisement>().HasData(new List<Advertisement>()
            {
                new()
                {
                    Id = 1,
                    Title = "iPhone X",
                    Description = "Smartphone, excellent condition",
                    Price = 650,
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = 1,
                    UserId = "user1"
                },
                new()
                {
                    Id = 2,
                    Title = "Nike T-Shirt",
                    Description = "Size L, new",
                    Price = 25.5M,
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = 3,
                    UserId = "user1"
                },
                new()
                {
                    Id = 3,
                    Title = "Samsung S23",
                    Description = "Latest model, almost new",
                    Price = 1200,
                    CreatedAt = DateTime.UtcNow,
                    CategoryId = 1,
                    UserId = "user2"
                }
            });
        }


    }
}