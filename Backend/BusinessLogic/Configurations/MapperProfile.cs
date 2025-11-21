using AutoMapper;
using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.DTOs.Category;
using DataAccess.Data.Entities;

namespace BusinessLogic.Configurations
{
    public class MapperProfile:Profile
    {
        public MapperProfile()
        {
            CreateMap<CreateAdvertisementDTO, Advertisement>();
            CreateMap<AdvertisementDTO, Advertisement>().ReverseMap();
            CreateMap<EditAdvertisementDTO, Advertisement>();

            CreateMap<CategoryDto, Category>().ReverseMap();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<CategoryCreateDto, Category>();
        }
    }
}
