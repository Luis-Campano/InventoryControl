using InventoryControl.Models.Dtos;
using InventoryControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Controllers
{
    [ApiController]
    [Route("api/inventory")]
    public class InventoryApiController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryApiController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        /// <summary>
        /// Obtiene la lista de productos con su stock actual
        /// </summary>
        [HttpGet("products")]
        public async Task<IActionResult> GetProducts()
        {
            var products = await _inventoryService.GetProductsWithStockAsync();
            return Ok(products);
        }


        /// <summary>
        /// Registra un movimiento de inventario (entrada o salida) para un producto específico
        /// </summary>
        /// <param name="movement">El movimiento a registrar</param>
        [HttpPost("movement")]
        public async Task<IActionResult> RegisterMovement([FromBody] MovementDto movement)
        {
            try
            {
                if (movement.Quantity <= 0) return BadRequest(new { message = "Cantidad inválida." });

                await _inventoryService.RegisterMovementAsync(movement);
                return Ok(new { message = "Movimiento procesado correctamente." });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { error = ex.Message });
            }
        }
    }
}
