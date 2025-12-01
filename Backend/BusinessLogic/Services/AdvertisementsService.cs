using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AdvertisementsService : IAdvertisementsService
    {
        private readonly AppDbContext _ctx;
        private readonly IBlobStorageService _blobStorageService;

        public AdvertisementsService(AppDbContext ctx, IBlobStorageService blobStorageService)
        {
            _ctx = ctx;
            _blobStorageService = blobStorageService;
        }

        public async Task<List<AdvertisementDTO>> GetAll(int? categoryIdFilter, string? searchByTitle, string? searchByCity, decimal? minPrice, decimal? maxPrice)
        {
            var baseQuery = _ctx.Advertisements.Where(x => x.Status == AdvertisementStatus.Confirmed);
            var globalMin = await baseQuery.AnyAsync() ? await baseQuery.MinAsync(x => x.Price) : 0;
            var globalMax = await baseQuery.AnyAsync() ? await baseQuery.MaxAsync(x => x.Price) : 0;

            if ((minPrice.HasValue && minPrice.Value > globalMax) ||
                (maxPrice.HasValue && maxPrice.Value < globalMin) ||
                (minPrice.HasValue && maxPrice.HasValue && maxPrice < minPrice))
            {
                return new List<AdvertisementDTO>();
            }

            var query = baseQuery.AsQueryable();

            if (categoryIdFilter.HasValue)
                query = query.Where(x => x.CategoryId == categoryIdFilter.Value);

            if (!string.IsNullOrWhiteSpace(searchByTitle))
                query = query.Where(x => x.Title.ToLower().Contains(searchByTitle.ToLower()));

            if (!string.IsNullOrWhiteSpace(searchByCity))
                query = query.Where(x => x.City.ToLower().Contains(searchByCity.ToLower()));

            if (minPrice.HasValue)
                query = query.Where(x => x.Price >= minPrice.Value);

            if (maxPrice.HasValue)
                query = query.Where(x => x.Price <= maxPrice.Value);

            var filtered = await query.Select(ad => new AdvertisementDTO
            {
                Id = ad.Id,
                Title = ad.Title,
                Description = ad.Description,
                Price = ad.Price,
                City = ad.City,
                IsNew = ad.IsNew,
                CreatedAt = ad.CreatedAt,
                CategoryId = ad.CategoryId,
                UserId = ad.UserId,
                ImageUrl = ad.ImageUrl,
                Status = ad.Status
            }).ToListAsync();

            return filtered;
        }

        public async Task<AdvertisementDTO> Get(int? id)
        {
            if (!id.HasValue || id <= 0)
                throw new ArgumentException("Id має бути більше нуля");

            var ad = await _ctx.Advertisements
                               .Where(a => a.Id == id.Value)
                               .Select(a => new AdvertisementDTO
                               {
                                   Id = a.Id,
                                   Title = a.Title,
                                   Description = a.Description,
                                   Price = a.Price,
                                   City = a.City,
                                   IsNew = a.IsNew,
                                   CreatedAt = a.CreatedAt,
                                   CategoryId = a.CategoryId,
                                   ImageUrl = a.ImageUrl,
                                   UserId = a.UserId,
                                   Status = (AdvertisementStatus)a.Status
                               })
                               .FirstOrDefaultAsync();

            if (ad == null)
                throw new KeyNotFoundException("Оголошення не знайдено");

            return ad;
        }


        public async Task Create(CreateAdvertisementDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var imageUrl = await HandleImageUpload(dto.Image);

            var ad = new Advertisement
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                City = dto.City,
                IsNew = dto.isNew,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId,
                ImageUrl = imageUrl,
                Status = a.Status
            };

            _ctx.Advertisements.Add(ad);
            await _ctx.SaveChangesAsync();
        }


        public async Task Edit(int id, EditAdvertisementDTO dto)
        {
            var existingAd = await _ctx.Advertisements.FindAsync(id);
            if (existingAd == null)
                throw new KeyNotFoundException("Оголошення не знайдено");

            existingAd.Title = dto.Title;
            existingAd.Description = dto.Description;
            existingAd.Price = dto.Price;
            existingAd.CategoryId = dto.CategoryId;

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

        private async Task<string?> HandleImageUpload(IFormFile? image)
        {
            if (image == null)
                return null;

            using var stream = image.OpenReadStream();

            return await _blobStorageService.UploadAsync(
                stream,
                $"{Guid.NewGuid()}-{image.FileName}",
                "advertisements"
            );
        }
    }
}