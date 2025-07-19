using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IIngredientService
    {
        Task<IEnumerable<IngredientDto>> GetAllAsync();
        Task<IngredientDto> GetByIdAsync(int id);
        Task<IngredientDto> CreateAsync(CreateIngredientDto createIngredientDto);
        Task<IngredientDto> UpdateAsync(int id, UpdateIngredientDto updateIngredientDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<IngredientDto>> GetActiveIngredientsAsync();
    }
} 