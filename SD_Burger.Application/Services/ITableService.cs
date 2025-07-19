using SD_Burger.Application.DTOs;

namespace SD_Burger.Application.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllAsync();
        Task<TableDto> GetByIdAsync(int id);
        Task<TableDto> CreateAsync(TableDto tableDto);
        Task<TableDto> UpdateAsync(int id, TableDto tableDto);
        Task DeleteAsync(int id);
    }
} 