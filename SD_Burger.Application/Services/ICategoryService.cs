using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryDto>> GetAllAsync();
        Task<CategoryDto> GetByIdAsync(int id);
        Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto);
        Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync();
    }
} 