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
    public class ReservationService : IReservationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReservationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<ReservationDto> GetByIdAsync(int id)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .FirstOrDefaultAsync(r => r.Id == id && r.IsActive);

            return reservation?.ToDto();
        }

        public async Task<IEnumerable<ReservationDto>> GetAllAsync()
        {
            var reservations = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .Where(r => r.IsActive)
                .ToListAsync();

            return reservations.ToDtoList();
        }

        public async Task<ReservationDto> CreateAsync(CreateReservationDto createReservationDto)
        {
            var reservation = new Reservation
            {
                ReservationDate = createReservationDto.ReservationDate,
                ReservationTime = createReservationDto.ReservationTime,
                GuestCount = createReservationDto.GuestCount,
                CustomerName = createReservationDto.CustomerName,
                CustomerPhone = createReservationDto.CustomerPhone,
                CustomerEmail = createReservationDto.CustomerEmail,
                SpecialRequests = createReservationDto.SpecialRequests,
                Status = ReservationStatus.Pending,
                TableId = createReservationDto.TableId,
                BranchId = createReservationDto.BranchId,
                UserId = createReservationDto.UserId
            };

            await _unitOfWork.Repository<Reservation>().AddAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(reservation.Id);
        }

        public async Task<ReservationDto> UpdateAsync(int id, UpdateReservationDto updateReservationDto)
        {
            var reservation = await _unitOfWork.Repository<Reservation>().GetByIdAsync(id);
            if (reservation == null)
                throw new InvalidOperationException("Rezervasyon bulunamadı.");

            reservation.ReservationDate = updateReservationDto.ReservationDate;
            reservation.ReservationTime = updateReservationDto.ReservationTime;
            reservation.GuestCount = updateReservationDto.GuestCount;
            reservation.CustomerName = updateReservationDto.CustomerName;
            reservation.CustomerPhone = updateReservationDto.CustomerPhone;
            reservation.CustomerEmail = updateReservationDto.CustomerEmail;
            reservation.SpecialRequests = updateReservationDto.SpecialRequests;
            reservation.Status = updateReservationDto.Status;
            reservation.TableId = updateReservationDto.TableId;

            await _unitOfWork.Repository<Reservation>().UpdateAsync(reservation);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Reservation>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<ReservationDto>> GetByDateAsync(DateTime date)
        {
            var reservations = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .Where(r => r.ReservationDate.Date == date.Date && r.IsActive)
                .ToListAsync();

            return reservations.ToDtoList();
        }

        public async Task<IEnumerable<ReservationDto>> GetByBranchAsync(int branchId)
        {
            var reservations = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .Where(r => r.BranchId == branchId && r.IsActive)
                .ToListAsync();

            return reservations.ToDtoList();
        }

        public async Task<IEnumerable<ReservationDto>> GetByTableAsync(int tableId)
        {
            var reservations = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .Where(r => r.TableId == tableId && r.IsActive)
                .ToListAsync();

            return reservations.ToDtoList();
        }

        public async Task<IEnumerable<ReservationDto>> GetByStatusAsync(ReservationStatus status)
        {
            var reservations = await _unitOfWork.Repository<Reservation>().Query()
                .Include(r => r.Table)
                .Include(r => r.Branch)
                .Include(r => r.User)
                .Where(r => r.Status == status && r.IsActive)
                .ToListAsync();

            return reservations.ToDtoList();
        }
    }
} 