using AutoMapper;
using BusinessLogic.DTOs.Category;
using BusinessLogic.Interfaces;
using DataAccess.Data;
using DataAccess.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _context;
        private readonly IMapper mapper;

        public CategoryService(AppDbContext context, IMapper _mapper)
        {
            _context = context;
            mapper = _mapper;
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _context.Categories.ToListAsync();
            return mapper.Map<List<CategoryDto>>(categories);
        }

        public async Task<CategoryDto?> GetByIdAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return null;

            return mapper.Map<CategoryDto>(category);

        }

        public async Task<CategoryDto> CreateAsync(CategoryCreateDto dto)
        {
            var category = mapper.Map<Category>(dto);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return mapper.Map<CategoryDto>(category);
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDto dto)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
                return false;

            mapper.Map(dto, category);

            await _context.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _context.Categories.FindAsync(id);
            if (category == null) return false;

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
