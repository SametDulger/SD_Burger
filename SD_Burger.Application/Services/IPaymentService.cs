using SD_Burger.Application.DTOs;

namespace SD_Burger.Application.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto> GetByIdAsync(int id);
        Task<PaymentDto> CreateAsync(CreatePaymentDto createPaymentDto);
        Task<PaymentDto> UpdateAsync(int id, UpdatePaymentDto updatePaymentDto);
        Task DeleteAsync(int id);
    }
} 