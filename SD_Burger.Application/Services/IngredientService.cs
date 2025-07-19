using Microsoft.EntityFrameworkCore;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Mappers;
using SD_Burger.Core.Entities;
using SD_Burger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public class IngredientService : IIngredientService
    {
        private readonly IUnitOfWork _unitOfWork;

        public IngredientService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IngredientDto> GetByIdAsync(int id)
        {
            var ingredient = await _unitOfWork.Repository<Ingredient>().Query()
                .FirstOrDefaultAsync(i => i.Id == id && i.IsActive);

            if (ingredient == null)
                return null;

            return ingredient.ToDto();
        }

        public async Task<IEnumerable<IngredientDto>> GetAllAsync()
        {
            var ingredients = await _unitOfWork.Repository<Ingredient>().Query()
                .Where(i => i.IsActive)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return ingredients.ToDtoList();
        }

        public async Task<IngredientDto> CreateAsync(CreateIngredientDto createIngredientDto)
        {
            var ingredient = new Ingredient
            {
                Name = createIngredientDto.Name,
                Description = createIngredientDto.Description,
                Unit = createIngredientDto.Unit,
                UnitPrice = createIngredientDto.UnitPrice,
                MinimumStock = createIngredientDto.MinimumStock
            };

            await _unitOfWork.Repository<Ingredient>().AddAsync(ingredient);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(ingredient.Id);
        }

        public async Task<IngredientDto> UpdateAsync(int id, UpdateIngredientDto updateIngredientDto)
        {
            var ingredient = await _unitOfWork.Repository<Ingredient>().GetByIdAsync(id);
            if (ingredient == null)
                throw new InvalidOperationException("Malzeme bulunamadı.");

            ingredient.Name = updateIngredientDto.Name;
            ingredient.Description = updateIngredientDto.Description;
            ingredient.Unit = updateIngredientDto.Unit;
            ingredient.UnitPrice = updateIngredientDto.UnitPrice;
            ingredient.MinimumStock = updateIngredientDto.MinimumStock;
            ingredient.IsActive = updateIngredientDto.IsActive;

            await _unitOfWork.Repository<Ingredient>().UpdateAsync(ingredient);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Ingredient>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<IngredientDto>> GetActiveIngredientsAsync()
        {
            var ingredients = await _unitOfWork.Repository<Ingredient>().Query()
                .Where(i => i.IsActive)
                .OrderBy(i => i.Name)
                .ToListAsync();

            return ingredients.ToDtoList();
        }
    }
} 