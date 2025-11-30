using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
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
            return await _ctx.Advertisements
                .AsNoTracking()
                .Where(x => x.Status == (int)AdvertisementStatus.Pending)
                .OrderByDescending(x => x.CreatedAt)
                .Select(ad => new AdvertisementDTO
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
                    UserId = ad.UserId,

                    Status = (AdvertisementStatus)ad.Status
                })
                .ToListAsync();
        }

        public async Task Confirm(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);
            if (ad == null) return;

            ad.Status = (int)AdvertisementStatus.Confirmed;
            await _ctx.SaveChangesAsync();
        }

        public async Task Reject(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);
            if (ad == null) return;

            ad.Status = (int)AdvertisementStatus.Rejected;
            await _ctx.SaveChangesAsync();
        }
    }
}