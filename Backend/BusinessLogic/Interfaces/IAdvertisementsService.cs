using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdvertisementsService
    {
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> Get(int? id);
        Task Create(CreateAdvertisementDTO dto);
        Task Edit(int id, EditAdvertisementDTO dto);
        Task Delete(int? id);
    }
}
