using DataAccess.Data.Entities;

namespace BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task<bool> BlockUser(string id);
        Task<bool> UnBlockUser(string id);
        Task<User?> GetProfile(string id);
    }
}
