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
    public class MenuItemService : IMenuItemService
    {
        private readonly IUnitOfWork _unitOfWork;

        public MenuItemService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<MenuItemDto> GetByIdAsync(int id)
        {
            var menuItem = await _unitOfWork.Repository<MenuItem>().Query()
                .Include(m => m.Category)
                .FirstOrDefaultAsync(m => m.Id == id && m.IsActive);

            if (menuItem == null)
                return null;

            return menuItem.ToDto();
        }

        public async Task<IEnumerable<MenuItemDto>> GetAllAsync()
        {
            var menuItems = await _unitOfWork.Repository<MenuItem>().Query()
                .Include(m => m.Category)
                .Where(m => m.IsActive)
                .ToListAsync();

            return menuItems.ToDtoList();
        }

        public async Task<MenuItemDto> CreateAsync(CreateMenuItemDto createMenuItemDto)
        {
            var menuItem = new MenuItem
            {
                Name = createMenuItemDto.Name,
                Description = createMenuItemDto.Description,
                Price = createMenuItemDto.Price,
                ImageUrl = createMenuItemDto.ImageUrl,
                IsAvailable = createMenuItemDto.IsAvailable,
                CategoryId = createMenuItemDto.CategoryId
            };

            await _unitOfWork.Repository<MenuItem>().AddAsync(menuItem);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(menuItem.Id);
        }

        public async Task<MenuItemDto> UpdateAsync(int id, UpdateMenuItemDto updateMenuItemDto)
        {
            var menuItem = await _unitOfWork.Repository<MenuItem>().GetByIdAsync(id);
            if (menuItem == null)
                throw new InvalidOperationException("Menü öğesi bulunamadı.");

            menuItem.Name = updateMenuItemDto.Name;
            menuItem.Description = updateMenuItemDto.Description;
            menuItem.Price = updateMenuItemDto.Price;
            menuItem.ImageUrl = updateMenuItemDto.ImageUrl;
            menuItem.IsAvailable = updateMenuItemDto.IsAvailable;
            menuItem.CategoryId = updateMenuItemDto.CategoryId;

            await _unitOfWork.Repository<MenuItem>().UpdateAsync(menuItem);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<MenuItem>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<MenuItemDto>> GetByCategoryAsync(int categoryId)
        {
            var menuItems = await _unitOfWork.Repository<MenuItem>().Query()
                .Include(m => m.Category)
                .Where(m => m.CategoryId == categoryId && m.IsActive)
                .ToListAsync();

            return menuItems.ToDtoList();
        }

        public async Task<IEnumerable<MenuItemDto>> GetAvailableItemsAsync()
        {
            var menuItems = await _unitOfWork.Repository<MenuItem>().Query()
                .Include(m => m.Category)
                .Where(m => m.IsAvailable && m.IsActive)
                .ToListAsync();

            return menuItems.ToDtoList();
        }
    }
} 