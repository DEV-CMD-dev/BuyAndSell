using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class AdminService: IAdminService
    {
        private readonly UserManager<User> _userManager;

        public AdminService(UserManager<User> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Blocking(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            await _userManager.SetLockoutEnabledAsync(user, true);

            await _userManager.SetLockoutEndDateAsync(user, DateTimeOffset.MaxValue);

            return true;
        }
        public async Task<bool> UnBlocking(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
                return false;

            await _userManager.SetLockoutEnabledAsync(user, false);

            await _userManager.SetLockoutEndDateAsync(user, null);

            return true;
        }
        public async Task<User?> GetProfile(string id)
        {
            return await _userManager.FindByIdAsync(id);
        }

    }
}
