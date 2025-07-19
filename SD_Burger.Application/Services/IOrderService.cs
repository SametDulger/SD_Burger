using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IOrderService
    {
        Task<OrderDto> GetByIdAsync(int id);
        Task<IEnumerable<OrderDto>> GetAllAsync();
        Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto);
        Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateOrderDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<OrderDto>> GetByBranchAsync(int branchId);
        Task<IEnumerable<OrderDto>> GetByTableAsync(int tableId);
        Task<IEnumerable<OrderDto>> GetByStatusAsync(Core.Entities.OrderStatus status);
        Task<IEnumerable<OrderDto>> GetByWaiterAsync(int waiterId);
        Task<string> GenerateOrderNumberAsync();
        Task<SalesReportDto> GetSalesReportAsync(DateTime? startDate, DateTime? endDate);
        Task<int> GetTotalOrdersAsync();
        Task<decimal> GetTotalSalesAsync();
    }
} 