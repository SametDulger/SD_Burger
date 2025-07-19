using SD_Burger.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IReservationService
    {
        Task<ReservationDto> GetByIdAsync(int id);
        Task<IEnumerable<ReservationDto>> GetAllAsync();
        Task<ReservationDto> CreateAsync(CreateReservationDto createReservationDto);
        Task<ReservationDto> UpdateAsync(int id, UpdateReservationDto updateReservationDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<ReservationDto>> GetByDateAsync(DateTime date);
        Task<IEnumerable<ReservationDto>> GetByBranchAsync(int branchId);
        Task<IEnumerable<ReservationDto>> GetByTableAsync(int tableId);
        Task<IEnumerable<ReservationDto>> GetByStatusAsync(Core.Entities.ReservationStatus status);
    }
} 