using BusinessLogic.Interfaces;
using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdminController : ControllerBase
    {
        private readonly IAdminService _adminService;

        public AdminController(IAdminService adminService)
        {
            _adminService = adminService;
        }

        [HttpPost("block/{id}")]
        public async Task<IActionResult> BlockUser(string id)
        {
            var result = await _adminService.BlockUser(id);

            if (!result)
                return NotFound("Користувача не знайдено");

            return Ok("Користувач заблокований");
        }

        [HttpPost("unblock/{id}")]
        public async Task<IActionResult> UnblockUser(string id)
        {
            var result = await _adminService.UnBlockUser(id);

            if (!result)
                return NotFound("Користувача не знайдено");

            return Ok("Користувач розблокований");
        }

        [HttpGet("profile/{id}")]
        public async Task<IActionResult> GetProfile(string id)
        {
            var user = await _adminService.GetProfile(id);

            if (user == null)
                return NotFound("Користувача не знайдено");

            return Ok(user);
        }
    }
}
