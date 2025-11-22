using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Interfaces
{
    public interface IAdminService
    {
        Task Blocking(string id);
        Task UnBlocking(string id);
        Task GerProfile(string id);
    }
}
