using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class IngredientsController : ControllerBase
    {
        private readonly IIngredientService _ingredientService;

        public IngredientsController(IIngredientService ingredientService)
        {
            _ingredientService = ingredientService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetAll()
        {
            try
            {
                var ingredients = await _ingredientService.GetAllAsync();
                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Malzemeler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDto>> GetById(int id)
        {
            try
            {
                var ingredient = await _ingredientService.GetByIdAsync(id);
                if (ingredient == null)
                    return NotFound(new { message = "Malzeme bulunamadı." });

                return Ok(ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Malzeme getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<IngredientDto>> Create(CreateIngredientDto createIngredientDto)
        {
            try
            {
                var ingredient = await _ingredientService.CreateAsync(createIngredientDto);
                return CreatedAtAction(nameof(GetById), new { id = ingredient.Id }, ingredient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Malzeme oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<IngredientDto>> Update(int id, UpdateIngredientDto updateIngredientDto)
        {
            try
            {
                var ingredient = await _ingredientService.UpdateAsync(id, updateIngredientDto);
                return Ok(ingredient);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Malzeme güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _ingredientService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Malzeme silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<IngredientDto>>> GetActiveIngredients()
        {
            try
            {
                var ingredients = await _ingredientService.GetActiveIngredientsAsync();
                return Ok(ingredients);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Aktif malzemeler getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 