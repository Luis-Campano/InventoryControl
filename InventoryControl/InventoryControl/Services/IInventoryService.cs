using InventoryControl.Models.Dtos;

namespace InventoryControl.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<ProductDto>> GetProductsWithStockAsync();
        Task<int> CalculateStockAsync(int productId);
        Task<bool> RegisterMovementAsync(MovementDto movement);
        Task<IEnumerable<MovementDto>> GetMovementHistoryAsync(int productId);
        Task<ProductDto?> GetProductByIdAsync(int productId);
        Task<IEnumerable<ProductDto>> GetActiveProductsAsync();
    }
}
