using SD_Burger.Application.DTOs;

namespace SD_Burger.Application.Services
{
    public interface IPaymentService
    {
        Task<IEnumerable<PaymentDto>> GetAllAsync();
        Task<PaymentDto> GetByIdAsync(int id);
        Task<PaymentDto> CreateAsync(PaymentDto paymentDto);
        Task<PaymentDto> UpdateAsync(int id, PaymentDto paymentDto);
        Task DeleteAsync(int id);
    }
} 