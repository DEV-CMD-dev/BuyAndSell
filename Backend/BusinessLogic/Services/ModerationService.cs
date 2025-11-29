using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ModerationService : IModerationService
    {
        private readonly AppDbContext _ctx;

        public ModerationService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<AdvertisementDTO>> GetPending()
        {
            var pending = await _ctx.Advertisements
                .Where(x => !x.IsConfirmed)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync();

            return pending.Select(ad => new AdvertisementDTO
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                City = ad.City,
                IsNew = ad.IsNew,
                ImageUrl = ad.ImageUrl,
                CreatedAt = ad.CreatedAt,
                CategoryId = ad.CategoryId,
                UserId = ad.UserId
            }).ToList();
        }

        public async Task Confirm(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);
            if (ad == null) return;

            ad.IsConfirmed = true;
            await _ctx.SaveChangesAsync();
        }

        public async Task Reject(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);
            if (ad == null) return;

            _ctx.Advertisements.Remove(ad);
            await _ctx.SaveChangesAsync();
        }
    }
}