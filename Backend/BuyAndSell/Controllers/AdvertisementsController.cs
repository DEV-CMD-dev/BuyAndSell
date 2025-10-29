using BusinessLogic.DTOs;
using BusinessLogic.Interfaces;
using DataAccess.Data.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BuyAndSell.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdvertisementsController : ControllerBase
    {
        private readonly IAdvertisementsService advertisementsService;

        public AdvertisementsController(IAdvertisementsService advertisementsService)
        {
            this.advertisementsService = advertisementsService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var ads = await advertisementsService.GetAll();
            return Ok(ads);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var ad = await advertisementsService.Get(id);
            if (ad == null)
                return NotFound("Advertisement not found");

            return Ok(ad);
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody] AdvertisementsDTO dto)
        {
            await advertisementsService.Create(dto);
            return Ok();
        }



        [HttpPut("{id}")]
        public async Task<IActionResult> Edit(int id, [FromBody] Advertisement updatedAd)
        {
            await advertisementsService.Edit(id, updatedAd);
            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await advertisementsService.Delete(id);
            return NoContent();
        }



    }
}
