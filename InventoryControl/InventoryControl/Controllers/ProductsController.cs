using InventoryControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IInventoryService _inventoryService;
        public ProductsController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Index()
        {
            var products = await _inventoryService.GetProductsWithStockAsync();
            return View(products);
        }

        public async Task<IActionResult> History(int id)
        {
            var history = await _inventoryService.GetMovementHistoryAsync(id);

            ViewBag.ProductName = history.FirstOrDefault()?.ProductName ?? "Producto no encontrado";
            ViewBag.ProductCode = history.FirstOrDefault()?.ProductCode ?? "Código no encontrado";

            return View(history);
        }
    }
}
