namespace BusinessLogic.DTOs
{
    public class AdvertisementDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string? ImageUrl { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public bool? IsNew { get; set; }
        public string? City { get; set; }
        public string CategoryName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public int? CategoryId { get; set; }

        public string? UserId { get; set; }
    }
}
