using BusinessLogic.DTOs.Advertisements;
using BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BuyAndSell.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ModerationController : ControllerBase
    {
        private readonly IModerationService _moderationService;

        public ModerationController(IModerationService moderationService)
        {
            _moderationService = moderationService;
        }

        [HttpGet("pending")]
        public async Task<IActionResult> GetPending()
        {
            var result = await _moderationService.GetPending();
            return Ok(result);
        }

        [HttpPost("confirm/{id}")]
        public async Task<IActionResult> Confirm(int id)
        {
            await _moderationService.Confirm(id);
            return Ok("Оголошення підтверджено");
        }

        [HttpPost("reject/{id}")]
        public async Task<IActionResult> Reject(int id)
        {
            await _moderationService.Reject(id);
            return Ok("Оголошення відхилено");
        }
    }
}