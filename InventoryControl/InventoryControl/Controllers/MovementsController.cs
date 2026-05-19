using InventoryControl.Models.Dtos;
using InventoryControl.Services;
using Microsoft.AspNetCore.Mvc;

namespace InventoryControl.Controllers
{
    public class MovementsController : Controller
    {
        private readonly IInventoryService _inventoryService;

        public MovementsController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.Products = await _inventoryService.GetActiveProductsAsync();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MovementDto dto)
        {
            try
            {
                if (dto.Quantity <= 0)
                {
                    ModelState.AddModelError("Quantity", "La cantidad debe ser mayor a 0");
                }

                if (ModelState.IsValid)
                {

                    await _inventoryService.RegisterMovementAsync(dto);
                    return RedirectToAction("Index", "Products");
                }
            }
            catch (InvalidOperationException ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
            }

            ViewBag.Products = await _inventoryService.GetActiveProductsAsync();
            return View(dto);
        }

    }
}
