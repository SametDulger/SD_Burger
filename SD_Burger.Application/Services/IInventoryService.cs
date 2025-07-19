using SD_Burger.Application.DTOs;

namespace SD_Burger.Application.Services
{
    public interface IInventoryService
    {
        Task<IEnumerable<InventoryDto>> GetAllAsync();
        Task<InventoryDto> GetByIdAsync(int id);
        Task<InventoryDto> CreateAsync(CreateInventoryDto createInventoryDto);
        Task<InventoryDto> UpdateAsync(int id, UpdateInventoryDto updateInventoryDto);
        Task DeleteAsync(int id);
        Task<InventoryReportDto> GetInventoryReportAsync();
        Task<int> GetLowStockItemsCountAsync();
    }
} 