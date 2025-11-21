using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;
using AutoMapper;

namespace BusinessLogic.Services    
{
    public class AdvertisementsService: IAdvertisementsService
    {
        private readonly AppDbContext _ctx;
        private readonly IMapper mapper;

        public AdvertisementsService(AppDbContext ctx, IMapper mapper)
        {
            _ctx = ctx;

            this.mapper = mapper;

        }

        public async Task<List<AdvertisementDTO>> GetAll(int? categoryIdFilter ,string? searchByTitle, string? searchByCity, decimal? minPrice, decimal? maxPrice)
        {
            var globalMin = await _ctx.Advertisements.MinAsync(x => x.Price);
            var globalMax = await _ctx.Advertisements.MaxAsync(x => x.Price);

            if ((minPrice.HasValue && minPrice.Value > globalMax) ||
                (maxPrice.HasValue && maxPrice.Value < globalMin) ||
                (minPrice.HasValue && maxPrice.HasValue && maxPrice < minPrice))
            {
                return new List<AdvertisementDTO>();
            }

            var query = _ctx.Advertisements.AsQueryable();

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

            var filtered = await query.ToListAsync();

            return (List<AdvertisementDTO>)mapper.Map<IList<AdvertisementDTO>>(filtered);
        }


        public async Task<AdvertisementDTO> Get(int? id)
        {
            if (!id.HasValue || id <= 0)
                throw new ArgumentException("Id має бути більше нуля", nameof(id));

            var ad = await _ctx.Advertisements
                .SingleOrDefaultAsync(a => a.Id == id.Value);

            if (ad == null)
                throw new KeyNotFoundException($"Оголошення з Id {id.Value} не знайдено");

            return mapper.Map<AdvertisementDTO>(ad);
        }


        public async Task Create(CreateAdvertisementDTO dto)
        {
            if (dto == null)
                throw new ArgumentNullException(nameof(dto));

            var ad = mapper.Map<Advertisement>(dto);

            ad.CreatedAt = DateTime.UtcNow;

            _ctx.Advertisements.Add(ad);
            await _ctx.SaveChangesAsync();
        }


        public async Task Edit(int id, EditAdvertisementDTO dto)
        {
            var existingAd = await _ctx.Advertisements.FindAsync(id);

            if (existingAd == null)
                throw new KeyNotFoundException("Оголошення не знайдено");

            mapper.Map(dto, existingAd);

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
