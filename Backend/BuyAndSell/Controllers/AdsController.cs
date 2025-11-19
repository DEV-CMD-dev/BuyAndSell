using System.Globalization;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BuyAndSell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdsController : ControllerBase
    {
        private readonly AppDbContext db;
        private readonly IWebHostEnvironment _env;

        public AdsController(AppDbContext db, IWebHostEnvironment env)
        {
            this.db = db;
            _env = env;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ads = await db.Advertisements
                .OrderByDescending(a => a.CreatedAt)
                .Select(a => new
                {
                    id = a.Id,
                    title = a.Title,
                    description = a.Description,
                    price = a.Price,
                    location = a.City,
                    imageUrl = a.ImageUrl,
                    createdAt = a.CreatedAt
                })
                .ToListAsync();

            return Ok(ads);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var a = await db.Advertisements.FindAsync(id);
            if (a == null) return NotFound();

            return Ok(new
            {
                id = a.Id,
                title = a.Title,
                description = a.Description,
                price = a.Price,
                location = a.City,
                imageUrl = a.ImageUrl,
                createdAt = a.CreatedAt
            });
        }

        [HttpPost]
        public async Task<IActionResult> Create(
            [FromForm] string title,
            [FromForm] string? description,
            [FromForm] string price,
            [FromForm] string? location,
            [FromForm] string? category,
            [FromForm] List<IFormFile>? images
        )
        {
            if (string.IsNullOrWhiteSpace(title))
                return BadRequest(new { message = "Назва обов'язкова" });

            if (!decimal.TryParse(price?.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var parsedPrice))
                return BadRequest(new { message = "Некоректна ціна" });

            string? imagePath = null;

            if (images != null && images.Count > 0)
            {
                var file = images[0];
                if (file.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_env.WebRootPath ?? Directory.GetCurrentDirectory(), "wwwroot", "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }

                    imagePath = "/uploads/" + uniqueFileName;
                }
            }

            var ad = new Advertisement
            {
                Title = title.Trim(),
                Description = description?.Trim() ?? "",
                Price = parsedPrice,
                City = location?.Trim(),
                ImageUrl = imagePath,
                CreatedAt = DateTime.UtcNow
            };

            db.Advertisements.Add(ad);
            await db.SaveChangesAsync();

            return CreatedAtAction(nameof(GetById), new { id = ad.Id }, new { id = ad.Id });
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            [FromForm] string title,
            [FromForm] string? description,
            [FromForm] string price
        )
        {
            var a = await db.Advertisements.FindAsync(id);
            if (a == null) return NotFound();

            if (string.IsNullOrWhiteSpace(title)) return BadRequest("title");
            if (!decimal.TryParse(price.Replace(',', '.'), NumberStyles.Any, CultureInfo.InvariantCulture, out var p))
                return BadRequest("price");

            a.Title = title.Trim();
            a.Description = description?.Trim();
            a.Price = p;

            await db.SaveChangesAsync();
            return Ok(new { id = a.Id });
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            var a = await db.Advertisements.FindAsync(id);
            if (a == null) return NotFound();
            
            db.Advertisements.Remove(a);
            await db.SaveChangesAsync();
            
            return NoContent();
        }
    }
}