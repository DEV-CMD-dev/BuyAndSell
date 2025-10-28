using DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdvertisementsService
    {
        Task<List<Advertisement>> GetAll();
        Task<Advertisement> Get(int? id);
        Task Create(Advertisement ad);
        Task Edit(int id, Advertisement updatedAd);
        Task Delete(int? id);
    }
}
