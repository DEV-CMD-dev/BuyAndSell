using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace BusinessLogic.DTOs.Advertisements
{
    public class CreateAdvertisementDTO
    {
        public string Title { get; set; }
        public IFormFile Image { get; set; }
        [MinLength (80)]
        public string Description { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public bool isNew { get; set; }

        public int CategoryId { get; set; }

        public string UserId { get; set; }
    }
}
