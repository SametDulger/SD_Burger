using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IMenuItemService
    {
        Task<MenuItemDto> GetByIdAsync(int id);
        Task<IEnumerable<MenuItemDto>> GetAllAsync();
        Task<MenuItemDto> CreateAsync(CreateMenuItemDto createMenuItemDto);
        Task<MenuItemDto> UpdateAsync(int id, UpdateMenuItemDto updateMenuItemDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<MenuItemDto>> GetByCategoryAsync(int categoryId);
        Task<IEnumerable<MenuItemDto>> GetAvailableItemsAsync();
    }
} 