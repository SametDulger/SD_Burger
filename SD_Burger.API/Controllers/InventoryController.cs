using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class InventoryController : ControllerBase
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<InventoryDto>>> GetAll()
        {
            try
            {
                var inventories = await _inventoryService.GetAllAsync();
                return Ok(inventories);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Envanter listesi yüklenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<InventoryDto>> GetById(int id)
        {
            try
            {
                var inventory = await _inventoryService.GetByIdAsync(id);
                return Ok(inventory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Envanter öğesi getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<InventoryDto>> Create([FromBody] CreateInventoryDto createInventoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdInventory = await _inventoryService.CreateAsync(createInventoryDto);
                return CreatedAtAction(nameof(GetById), new { id = createdInventory.Id }, createdInventory);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Envanter öğesi oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<InventoryDto>> Update(int id, [FromBody] UpdateInventoryDto updateInventoryDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedInventory = await _inventoryService.UpdateAsync(id, updateInventoryDto);
                return Ok(updatedInventory);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Envanter öğesi güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _inventoryService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Envanter öğesi silinirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 