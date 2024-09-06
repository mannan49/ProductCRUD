namespace ProductCRUD.Models
{
    public class UpdateProductDto
    {
        public required string Name { get; set; }
        public required string Image { get; set; }
        public string? Description { get; set; }
        public required string Category { get; set; }
        public required int Price { get; set; }
        public DateTime Created { get; set; } = DateTime.Now;
        public required int Rating { get; set; }
        public required int Available { get; set; }
    }
}
