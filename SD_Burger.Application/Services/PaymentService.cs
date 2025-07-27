using SD_Burger.Application.DTOs;
using SD_Burger.Application.Mappers;
using SD_Burger.Core.Entities;
using SD_Burger.Core.Repositories;

namespace SD_Burger.Application.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IGenericRepository<Payment> _paymentRepository;
        private readonly IUnitOfWork _unitOfWork;

        public PaymentService(IGenericRepository<Payment> paymentRepository, IUnitOfWork unitOfWork)
        {
            _paymentRepository = paymentRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<PaymentDto>> GetAllAsync()
        {
            var payments = await _paymentRepository.GetAllAsync();
            return payments.Select(EntityMappers.ToPaymentDto);
        }

        public async Task<PaymentDto> GetByIdAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new ArgumentException($"Payment with ID {id} not found.");

            return EntityMappers.ToPaymentDto(payment);
        }

        public async Task<PaymentDto> CreateAsync(CreatePaymentDto createPaymentDto)
        {
            var payment = new Payment
            {
                OrderId = createPaymentDto.OrderId,
                Amount = createPaymentDto.Amount,
                PaymentMethod = createPaymentDto.PaymentMethod,
                Status = createPaymentDto.Status,
                TransactionId = createPaymentDto.TransactionId,
                PaymentDate = createPaymentDto.PaymentDate,
                Notes = createPaymentDto.Notes,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToPaymentDto(payment);
        }

        public async Task<PaymentDto> UpdateAsync(int id, UpdatePaymentDto updatePaymentDto)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new ArgumentException($"Payment with ID {id} not found.");

            payment.Amount = updatePaymentDto.Amount;
            payment.PaymentMethod = updatePaymentDto.PaymentMethod;
            payment.Status = updatePaymentDto.Status;
            payment.TransactionId = updatePaymentDto.TransactionId;
            payment.PaymentDate = updatePaymentDto.PaymentDate;
            payment.Notes = updatePaymentDto.Notes;
            payment.UpdatedDate = DateTime.UtcNow;

            await _paymentRepository.UpdateAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToPaymentDto(payment);
        }

        public async Task DeleteAsync(int id)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new ArgumentException($"Payment with ID {id} not found.");

            await _paymentRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
} 