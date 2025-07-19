using SD_Burger.Application.DTOs;
using SD_Burger.Application.Mappers;
using SD_Burger.Core.Entities;
using SD_Burger.Core.Repositories;

namespace SD_Burger.Application.Services
{
    public class InventoryService : IInventoryService
    {
        private readonly IGenericRepository<Inventory> _inventoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public InventoryService(IGenericRepository<Inventory> inventoryRepository, IUnitOfWork unitOfWork)
        {
            _inventoryRepository = inventoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<InventoryDto>> GetAllAsync()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return inventories.Select(EntityMappers.ToInventoryDto);
        }

        public async Task<InventoryDto> GetByIdAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
                throw new ArgumentException($"Inventory with ID {id} not found.");

            return EntityMappers.ToInventoryDto(inventory);
        }

        public async Task<InventoryDto> CreateAsync(CreateInventoryDto createInventoryDto)
        {
            var inventory = new Inventory
            {
                IngredientId = createInventoryDto.IngredientId,
                BranchId = createInventoryDto.BranchId,
                CurrentStock = createInventoryDto.CurrentStock,
                Quantity = createInventoryDto.CurrentStock,
                Unit = "piece",
                LastUpdated = DateTime.UtcNow,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _inventoryRepository.AddAsync(inventory);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToInventoryDto(inventory);
        }

        public async Task<InventoryDto> UpdateAsync(int id, UpdateInventoryDto updateInventoryDto)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
                throw new ArgumentException($"Inventory with ID {id} not found.");

            inventory.IngredientId = updateInventoryDto.IngredientId;
            inventory.BranchId = updateInventoryDto.BranchId;
            inventory.CurrentStock = updateInventoryDto.CurrentStock;
            inventory.Quantity = updateInventoryDto.CurrentStock;
            inventory.IsActive = updateInventoryDto.IsActive;
            inventory.LastUpdated = DateTime.UtcNow;
            inventory.UpdatedDate = DateTime.UtcNow;

            await _inventoryRepository.UpdateAsync(inventory);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToInventoryDto(inventory);
        }

        public async Task DeleteAsync(int id)
        {
            var inventory = await _inventoryRepository.GetByIdAsync(id);
            if (inventory == null)
                throw new ArgumentException($"Inventory with ID {id} not found.");

            await _inventoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<InventoryReportDto> GetInventoryReportAsync()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            
            var report = new InventoryReportDto
            {
                TotalItems = inventories.Count(),
                LowStockItems = inventories.Count(i => i.CurrentStock <= i.MinimumStock),
                OutOfStockItems = inventories.Count(i => i.CurrentStock == 0),
                TotalValue = inventories.Sum(i => i.CurrentStock * (i.Ingredient?.UnitPrice ?? 0))
            };

            return report;
        }

        public async Task<int> GetLowStockItemsCountAsync()
        {
            var inventories = await _inventoryRepository.GetAllAsync();
            return inventories.Count(i => i.CurrentStock <= i.MinimumStock);
        }
    }
} 