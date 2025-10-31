using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AdvertisementsService: IAdvertisementsService
    {
        private readonly AppDbContext _ctx;

        public AdvertisementsService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<Advertisement>> GetAll()
        {
            return await _ctx.Advertisements.ToListAsync();
        }

        public async Task<Advertisement> Get(int? id)
        {
            if (!id.HasValue || id <= 0)
                throw new ArgumentException("Id має бути більше нуля");

            var ad = await _ctx.Advertisements.FindAsync(id.Value);

            if (ad == null)
                throw new KeyNotFoundException("Оголошення не  знайдено");

            return ad;
        }

        public async Task Create(AdvertisementsDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var ad = new Advertisement
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow
            };

            _ctx.Advertisements.Add(ad);
            await _ctx.SaveChangesAsync();
        }


        public async Task Edit(int id, Advertisement updatedAd)
        {
            if (updatedAd == null)
                throw new ArgumentNullException(nameof(updatedAd));

            var existingAd = await _ctx.Advertisements.FindAsync(id);
            if (existingAd == null)
                throw new KeyNotFoundException("Оголошення не знайдено");

            existingAd.Title = updatedAd.Title;
            existingAd.Description = updatedAd.Description;
            existingAd.Price = updatedAd.Price;
            existingAd.CreatedAt = updatedAd.CreatedAt;
            existingAd.CategoryId = updatedAd.CategoryId;
            existingAd.Category = updatedAd.Category;
            existingAd.UserId = updatedAd.UserId;
            existingAd.User = updatedAd.User;

            await _ctx.SaveChangesAsync();

        }

        public async Task Delete(int? id)
        {
            if (!id.HasValue || id <= 0)
                throw new ArgumentException("Id має бути більше нуля");

            var existingAd = await _ctx.Advertisements.FindAsync(id);
            if (existingAd == null)
                throw new KeyNotFoundException("Оголошення не знайдено");

            _ctx.Advertisements.Remove(existingAd);
            await _ctx.SaveChangesAsync();
        }

    }
}
