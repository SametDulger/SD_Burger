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
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> GetByIdAsync(int id)
        {
            var category = await _unitOfWork.Repository<Category>().Query()
                .FirstOrDefaultAsync(c => c.Id == id && c.IsActive);

            if (category == null)
                return null;

            return category.ToDto();
        }

        public async Task<IEnumerable<CategoryDto>> GetAllAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().Query()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();

            return categories.ToDtoList();
        }

        public async Task<CategoryDto> CreateAsync(CreateCategoryDto createCategoryDto)
        {
            var category = new Category
            {
                Name = createCategoryDto.Name,
                Description = createCategoryDto.Description,
                ImageUrl = createCategoryDto.ImageUrl,
                DisplayOrder = createCategoryDto.DisplayOrder
            };

            await _unitOfWork.Repository<Category>().AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(category.Id);
        }

        public async Task<CategoryDto> UpdateAsync(int id, UpdateCategoryDto updateCategoryDto)
        {
            var category = await _unitOfWork.Repository<Category>().GetByIdAsync(id);
            if (category == null)
                throw new InvalidOperationException("Kategori bulunamadı.");

            category.Name = updateCategoryDto.Name;
            category.Description = updateCategoryDto.Description;
            category.ImageUrl = updateCategoryDto.ImageUrl;
            category.DisplayOrder = updateCategoryDto.DisplayOrder;
            category.IsActive = updateCategoryDto.IsActive;

            await _unitOfWork.Repository<Category>().UpdateAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Category>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<CategoryDto>> GetActiveCategoriesAsync()
        {
            var categories = await _unitOfWork.Repository<Category>().Query()
                .Where(c => c.IsActive)
                .OrderBy(c => c.DisplayOrder)
                .ToListAsync();

            return categories.ToDtoList();
        }
    }
} 