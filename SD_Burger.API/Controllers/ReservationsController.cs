using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;
using SD_Burger.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationService _reservationService;

        public ReservationsController(IReservationService reservationService)
        {
            _reservationService = reservationService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetAll()
        {
            try
            {
                var reservations = await _reservationService.GetAllAsync();
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyonlar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id)
        {
            try
            {
                var reservation = await _reservationService.GetByIdAsync(id);
                if (reservation == null)
                    return NotFound(new { message = "Rezervasyon bulunamadı." });

                return Ok(reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyon getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<ReservationDto>> Create(CreateReservationDto createReservationDto)
        {
            try
            {
                var reservation = await _reservationService.CreateAsync(createReservationDto);
                return CreatedAtAction(nameof(GetById), new { id = reservation.Id }, reservation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyon oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<ReservationDto>> Update(int id, UpdateReservationDto updateReservationDto)
        {
            try
            {
                var reservation = await _reservationService.UpdateAsync(id, updateReservationDto);
                return Ok(reservation);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyon güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _reservationService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyon silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("date/{date:datetime}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByDate(DateTime date)
        {
            try
            {
                var reservations = await _reservationService.GetByDateAsync(date);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyonlar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("branch/{branchId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByBranch(int branchId)
        {
            try
            {
                var reservations = await _reservationService.GetByBranchAsync(branchId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyonlar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("table/{tableId}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByTable(int tableId)
        {
            try
            {
                var reservations = await _reservationService.GetByTableAsync(tableId);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyonlar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("status/{status}")]
        public async Task<ActionResult<IEnumerable<ReservationDto>>> GetByStatus(ReservationStatus status)
        {
            try
            {
                var reservations = await _reservationService.GetByStatusAsync(status);
                return Ok(reservations);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Rezervasyonlar getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 