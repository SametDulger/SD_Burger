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

        public async Task<PaymentDto> CreateAsync(PaymentDto paymentDto)
        {
            var payment = new Payment
            {
                OrderId = paymentDto.OrderId,
                Amount = paymentDto.Amount,
                PaymentMethod = paymentDto.PaymentMethod,
                Status = paymentDto.Status,
                TransactionId = paymentDto.TransactionId,
                PaymentDate = paymentDto.PaymentDate,
                Notes = paymentDto.Notes,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _paymentRepository.AddAsync(payment);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToPaymentDto(payment);
        }

        public async Task<PaymentDto> UpdateAsync(int id, PaymentDto paymentDto)
        {
            var payment = await _paymentRepository.GetByIdAsync(id);
            if (payment == null)
                throw new ArgumentException($"Payment with ID {id} not found.");

            payment.OrderId = paymentDto.OrderId;
            payment.Amount = paymentDto.Amount;
            payment.PaymentMethod = paymentDto.PaymentMethod;
            payment.Status = paymentDto.Status;
            payment.TransactionId = paymentDto.TransactionId;
            payment.PaymentDate = paymentDto.PaymentDate;
            payment.Notes = paymentDto.Notes;
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