using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
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
                .Where(x => x.Status == AdvertisementStatus.Pending)
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

                    Status = ad.Status
                })
                .ToListAsync();
        }

        public async Task Confirm(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);

            if (ad == null)
                throw new KeyNotFoundException($"Оголошення з ID {id} не знайдено.");
            if (ad.Status == AdvertisementStatus.Rejected)
            {
                throw new InvalidOperationException("Неможливо підтвердити відхилене оголошення.");
            }
            if (ad.Status == AdvertisementStatus.Confirmed)
            {
                return;
            }
            ad.Status = AdvertisementStatus.Confirmed;
            await _ctx.SaveChangesAsync();
        }

        public async Task Reject(int id)
        {
            var ad = await _ctx.Advertisements.FindAsync(id);

            if (ad == null)
                throw new KeyNotFoundException($"Оголошення з ID {id} не знайдено.");
            if (ad.Status == AdvertisementStatus.Rejected)
            {
                return;
            }
            ad.Status = AdvertisementStatus.Rejected;
            await _ctx.SaveChangesAsync();
        }
    }
}