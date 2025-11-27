using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdvertisementsService
    {
        Task<List<AdvertisementDTO>> GetAll(int? categoryIdFilter, string? searchByTitle, string? searchByCity, decimal? minPrice, decimal? maxPrice);
        Task<AdvertisementDTO> Get(int? id);
        Task Create(CreateAdvertisementDTO dto);
        Task Edit(int id, EditAdvertisementDTO dto);
        Task Delete(int? id);
        Task Confirm(int id);
        Task<List<AdvertisementDTO>> GetPending();
    }
}
