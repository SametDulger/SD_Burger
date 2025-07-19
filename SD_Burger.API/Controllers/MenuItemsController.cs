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
    public class MenuItemsController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemsController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAll()
        {
            try
            {
                var menuItems = await _menuItemService.GetAllAsync();
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğeleri getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MenuItemDto>> GetById(int id)
        {
            try
            {
                var menuItem = await _menuItemService.GetByIdAsync(id);
                if (menuItem == null)
                    return NotFound(new { message = "Menü öğesi bulunamadı." });

                return Ok(menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğesi getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<MenuItemDto>> Create(CreateMenuItemDto createMenuItemDto)
        {
            try
            {
                var menuItem = await _menuItemService.CreateAsync(createMenuItemDto);
                return CreatedAtAction(nameof(GetById), new { id = menuItem.Id }, menuItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğesi oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<MenuItemDto>> Update(int id, UpdateMenuItemDto updateMenuItemDto)
        {
            try
            {
                var menuItem = await _menuItemService.UpdateAsync(id, updateMenuItemDto);
                return Ok(menuItem);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğesi güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _menuItemService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğesi silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("category/{categoryId}")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetByCategory(int categoryId)
        {
            try
            {
                var menuItems = await _menuItemService.GetByCategoryAsync(categoryId);
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Menü öğeleri getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("available")]
        public async Task<ActionResult<IEnumerable<MenuItemDto>>> GetAvailableItems()
        {
            try
            {
                var menuItems = await _menuItemService.GetAvailableItemsAsync();
                return Ok(menuItems);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Mevcut menü öğeleri getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 