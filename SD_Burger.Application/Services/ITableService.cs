using SD_Burger.Application.DTOs;

namespace SD_Burger.Application.Services
{
    public interface ITableService
    {
        Task<IEnumerable<TableDto>> GetAllAsync();
        Task<TableDto> GetByIdAsync(int id);
        Task<TableDto> CreateAsync(CreateTableDto createTableDto);
        Task<TableDto> UpdateAsync(int id, UpdateTableDto updateTableDto);
        Task DeleteAsync(int id);
    }
} 