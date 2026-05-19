namespace InventoryControl.Models.Entities
{
    public class Movement
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; } = DateTime.Now;
        public string? Remarks { get; set; }
        public Product? Product { get; set; }
    }
}
