using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using LinqKit;

namespace BusinessLogic.Services
{
    public class AdvertisementsService: IAdvertisementsService
    {
        private readonly AppDbContext _ctx;

        public AdvertisementsService(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<List<AdvertisementDTO>> GetAll(int? categoryIdFilter, string? searchByTitle, string? searchByCity, decimal? minPrice, decimal? maxPrice)
        {
            var globalMin = await _ctx.Advertisements.MinAsync(x => x.Price);
            var globalMax = await _ctx.Advertisements.MaxAsync(x => x.Price);

            if (minPrice.HasValue && minPrice.Value > globalMax)
                return new List<AdvertisementDTO>();

            if (maxPrice.HasValue && maxPrice.Value < globalMin)
                return new List<AdvertisementDTO>();

            if (minPrice.HasValue && maxPrice.HasValue && maxPrice < minPrice)
                return new List<AdvertisementDTO>();

            if (!string.IsNullOrWhiteSpace(searchByCity))
            {
                bool cityExists = await _ctx.Advertisements
                    .AnyAsync(a => a.City.ToLower().Contains(searchByCity.ToLower()));

                if (!cityExists)
                    return new List<AdvertisementDTO>();
            }

            if (!string.IsNullOrWhiteSpace(searchByTitle))
            {
                bool titleExists = await _ctx.Advertisements
                    .AnyAsync(a => a.Title.ToLower().Contains(searchByTitle.ToLower()));

                if (!titleExists)
                    return new List<AdvertisementDTO>();
            }

            if (categoryIdFilter.HasValue)
            {
                bool categoryExists = await _ctx.Advertisements
                    .AnyAsync(a => a.CategoryId == categoryIdFilter);

                if (!categoryExists)
                    return new List<AdvertisementDTO>();
            }

            var filterAd = PredicateBuilder.New<Advertisement>(true);

            if (categoryIdFilter != null)
            {
                filterAd = filterAd.And(x => x.CategoryId == categoryIdFilter);
            }

            if (!string.IsNullOrWhiteSpace(searchByTitle))
            {
                filterAd = filterAd.And(x => x.Title.ToLower().Contains(searchByTitle.ToLower()));
            }

            if (!string.IsNullOrWhiteSpace(searchByCity))
            {
                filterAd = filterAd.And(x => x.City.ToLower().Contains(searchByCity.ToLower()));
            }

            if(minPrice.HasValue)
            {
                filterAd = filterAd.And(x => x.Price >= minPrice.Value);
            }

            if (maxPrice.HasValue)
            {
                filterAd = filterAd.And(x => x.Price <= maxPrice.Value);
            }

            var query = _ctx.Advertisements.Where(filterAd);

            var filtered = await query.ToListAsync();

            if (filtered.Count == 0)
            {
                return await _ctx.Advertisements
                    .Select(ad => new AdvertisementDTO
                    {
                        Id = ad.Id,
                        Title = ad.Title,
                        Description = ad.Description,
                        Price = ad.Price,
                        CreatedAt = ad.CreatedAt,
                        CategoryId = ad.CategoryId,
                        UserId = ad.UserId
                    })
                    .ToListAsync();
            }

            return filtered
                .Select(ad => new AdvertisementDTO
                {
                    Id = ad.Id,
                    Title = ad.Title,
                    Description = ad.Description,
                    Price = ad.Price,
                    CreatedAt = ad.CreatedAt,
                    CategoryId = ad.CategoryId,
                    UserId = ad.UserId
                })
                .ToList();
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
                                   CreatedAt = a.CreatedAt,
                                   CategoryId = a.CategoryId,
                                   UserId = a.UserId
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

            var ad = new Advertisement
            {
                Title = dto.Title,
                Description = dto.Description,
                Price = dto.Price,
                CategoryId = dto.CategoryId,
                CreatedAt = DateTime.UtcNow,
                UserId = dto.UserId
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

    }
}
