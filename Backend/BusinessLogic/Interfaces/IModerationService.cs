using BusinessLogic.DTOs;
using BusinessLogic.DTOs.Advertisements;
using DataAccess.Data.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IModerationService
    {
        Task Reject(int id);
        Task Confirm(int id);
        Task<List<AdvertisementDTO>> GetPending();
    }
}
