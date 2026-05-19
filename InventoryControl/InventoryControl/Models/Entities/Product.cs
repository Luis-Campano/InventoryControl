namespace InventoryControl.Models.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Code { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public bool IsActive { get; set; } = true;
        public int CategoryId { get; set; }

        public Category? Category { get; set; }
        public ICollection<Movement> Movements { get; set; } = new List<Movement>();
    }
}
