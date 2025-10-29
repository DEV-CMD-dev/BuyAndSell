using BusinessLogic.DTOs;
using DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdvertisementsService
    {
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> Get(int? id);
        Task Create(AdvertisementsDTO dto);
        Task Edit(int id, Advertisement updatedAd);
        Task Delete(int? id);
    }
}
