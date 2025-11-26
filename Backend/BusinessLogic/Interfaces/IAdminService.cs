using DataAccess.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task<bool> Blocking(string id);
        Task<bool> UnBlocking(string id);
        Task<User?> GetProfile(string id);
    }
}
