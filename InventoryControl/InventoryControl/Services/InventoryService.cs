using InventoryControl.Data;
using InventoryControl.Enums;
using InventoryControl.Models.Dtos;
using InventoryControl.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace InventoryControl.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext _context;

        public InventoryService(ApplicationDbContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Método para calcular el stock de un producto
        /// </summary>
        /// <param name="productId">Id del producto</param>
        /// <returns>Cantidad de stock disponible</returns>
        public async Task<int> CalculateStockAsync(int productId)
        {
            var movements = await _context.Movements
                 .Where(m => m.ProductId == productId)
                 .ToListAsync();

            int inbound = movements.Where(m => m.MovementType == (int)MovementType.Inbound).Sum(m => m.Quantity);
            int outbound = movements.Where(m => m.MovementType == (int)MovementType.Outbound).Sum(m => m.Quantity);

            return inbound - outbound;
        }

        /// <summary>
        /// Método que obtiene todos los productos activos en el sistema
        /// </summary>
        /// <returns>Lista de productos activos</returns>
        public async Task<IEnumerable<ProductDto>> GetActiveProductsAsync()
        {
            var activeProducts = await _context.Products
                .Where(p => p.IsActive)
                .ToListAsync();

            return activeProducts.Select(p => new ProductDto
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                IsActive = p.IsActive,
            });
        }

        /// <summary>
        /// Método que obtiene el historial de movimientos de un producto
        /// </summary>
        /// <param name="productId">Id del producto</param>
        /// <returns>Lista de movimientos del producto</returns>
        public async Task<IEnumerable<MovementDto>> GetMovementHistoryAsync(int productId)
        {
            var movements = await _context.Movements
                 .Include(m => m.Product)
                 .Where(m => m.ProductId == productId)
                 .OrderByDescending(m => m.MovementDate)
                 .ToListAsync();

            return movements.Select(m => new MovementDto
            {
                Id = m.Id,
                ProductId = m.ProductId,
                ProductCode = m.Product?.Code ?? string.Empty,
                ProductName = m.Product?.Name ?? string.Empty,
                MovementType = m.MovementType,
                MovementTypeName = m.MovementType == 1 ? "Entrada" : "Salida",
                Quantity = m.Quantity,
                MovementDate = m.MovementDate,
                Remarks = m.Remarks ?? string.Empty
            });
        }

        /// <summary>
        /// Método para obtener un producto por su Id
        /// </summary>
        /// <param name="productId">Id del producto</param>
        /// <returns>El producto encontrado</returns>
        public async Task<ProductDto?> GetProductByIdAsync(int productId)
        {
            var product = await _context.Products.FindAsync(productId);
            if (product == null) return null;

            return new ProductDto
            {
                Id = product.Id,
                Code = product.Code,
                Name = product.Name,
                Description = product.Description ?? string.Empty,
                Price = product.Price,
                CategoryName = product.Category?.Name ?? "Sin categoría",
                IsActive = product.IsActive,
                CurrentStock = await CalculateStockAsync(product.Id)
            };
        }

        /// <summary>
        /// Método para obtener todos los productos con su stock actual
        /// </summary>
        /// <returns>Lista de productos con su stock actual</returns>
        public async Task<IEnumerable<ProductDto>> GetProductsWithStockAsync()
        {
            var products = await _context.Products
                .Include(p => p.Category)
                .Include(p => p.Movements)
                .ToListAsync();

            return products.Select(p => new ProductDto
            {
                Id = p.Id,
                Code = p.Code,
                Name = p.Name,
                Description = p.Description ?? string.Empty,
                Price = p.Price,
                CategoryName = p.Category?.Name ?? "Sin categoría",
                IsActive = p.IsActive,
                CurrentStock = p.Movements.Where(m => m.MovementType == 1).Sum(m => m.Quantity) -
                               p.Movements.Where(m => m.MovementType == 2).Sum(m => m.Quantity)
            });
        }

        /// <summary>
        /// Método que registra un movimiento de inventario
        /// </summary>
        /// <param name="movement">El movimiento a registrar</param>
        /// <returns>True si el movimiento se registró correctamente, false en caso contrario</returns>
        /// <exception cref="InvalidOperationException"></exception>
        public async Task<bool> RegisterMovementAsync(MovementDto movement)
        {
            if (movement.MovementType == (int)MovementType.Outbound)
            {
                int currentStock = await CalculateStockAsync(movement.ProductId);
                if (currentStock < movement.Quantity)
                {
                    throw new InvalidOperationException("Error: Stock insuficiente para realizar esta operación.");
                }
            }

            var movementEntity = new Movement
            {
                ProductId = movement.ProductId,
                MovementType = movement.MovementType,
                Quantity = movement.Quantity,
                MovementDate = DateTime.Now,
                Remarks = movement.Remarks
            };

            _context.Movements.Add(movementEntity);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
