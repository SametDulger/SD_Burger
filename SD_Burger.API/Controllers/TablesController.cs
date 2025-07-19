using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TablesController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TablesController(ITableService tableService)
        {
            _tableService = tableService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<TableDto>>> GetAll()
        {
            try
            {
                var tables = await _tableService.GetAllAsync();
                return Ok(tables);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving tables.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TableDto>> GetById(int id)
        {
            try
            {
                var table = await _tableService.GetByIdAsync(id);
                return Ok(table);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while retrieving the table.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<TableDto>> Create([FromBody] TableDto tableDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var createdTable = await _tableService.CreateAsync(tableDto);
                return CreatedAtAction(nameof(GetById), new { id = createdTable.Id }, createdTable);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while creating the table.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<TableDto>> Update(int id, [FromBody] TableDto tableDto)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                var updatedTable = await _tableService.UpdateAsync(id, tableDto);
                return Ok(updatedTable);
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while updating the table.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _tableService.DeleteAsync(id);
                return NoContent();
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while deleting the table.", error = ex.Message });
            }
        }
    }
} 