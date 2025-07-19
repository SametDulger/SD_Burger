using Microsoft.AspNetCore.Mvc;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BranchesController : ControllerBase
    {
        private readonly IBranchService _branchService;

        public BranchesController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetAll()
        {
            try
            {
                var branches = await _branchService.GetAllAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şubeler getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BranchDto>> GetById(int id)
        {
            try
            {
                var branch = await _branchService.GetByIdAsync(id);
                if (branch == null)
                    return NotFound(new { message = "Şube bulunamadı." });

                return Ok(branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şube getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<BranchDto>> Create(CreateBranchDto createBranchDto)
        {
            try
            {
                var branch = await _branchService.CreateAsync(createBranchDto);
                return CreatedAtAction(nameof(GetById), new { id = branch.Id }, branch);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şube oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<BranchDto>> Update(int id, UpdateBranchDto updateBranchDto)
        {
            try
            {
                var branch = await _branchService.UpdateAsync(id, updateBranchDto);
                return Ok(branch);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şube güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _branchService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Şube silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("active")]
        public async Task<ActionResult<IEnumerable<BranchDto>>> GetActiveBranches()
        {
            try
            {
                var branches = await _branchService.GetActiveBranchesAsync();
                return Ok(branches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Aktif şubeler getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 