namespace BusinessLogic.DTOs.Advertisements
{
    public class EditAdvertisementDTO
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool? IsNew { get; set; }
        public string? City { get; set; }

        public int? CategoryId { get; set; }
    }
}
