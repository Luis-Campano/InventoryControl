namespace InventoryControl.Models.Dtos
{
    public class MovementDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public string ProductCode { get; set; } = string.Empty;
        public string ProductName { get; set; } = string.Empty;
        public string MovementTypeName { get; set; } = string.Empty;
        public int MovementType { get; set; }
        public int Quantity { get; set; }
        public DateTime MovementDate { get; set; }
        public string? Remarks { get; set; } = string.Empty;
    }
}
