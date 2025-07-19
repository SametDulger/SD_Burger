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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetAll()
        {
            try
            {
                var users = await _userService.GetAllAsync();
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcılar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetById(int id)
        {
            try
            {
                var user = await _userService.GetByIdAsync(id);
                if (user == null)
                    return NotFound(new { message = "Kullanıcı bulunamadı." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcı getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Create(CreateUserDto createUserDto)
        {
            try
            {
                var user = await _userService.CreateAsync(createUserDto);
                return CreatedAtAction(nameof(GetById), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcı oluşturulurken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<UserDto>> Update(int id, UpdateUserDto updateUserDto)
        {
            try
            {
                var user = await _userService.UpdateAsync(id, updateUserDto);
                return Ok(user);
            }
            catch (InvalidOperationException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcı güncellenirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _userService.DeleteAsync(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcı silinirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            try
            {
                var user = await _userService.LoginAsync(loginDto);
                if (user == null)
                    return Unauthorized(new { message = "Geçersiz kullanıcı adı veya şifre." });

                return Ok(user);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Giriş yapılırken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("role/{role}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByRole(UserRole role)
        {
            try
            {
                var users = await _userService.GetByRoleAsync(role);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcılar getirilirken hata oluştu.", error = ex.Message });
            }
        }

        [HttpGet("branch/{branchId}")]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetByBranch(int branchId)
        {
            try
            {
                var users = await _userService.GetByBranchAsync(branchId);
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Kullanıcılar getirilirken hata oluştu.", error = ex.Message });
            }
        }
    }
} 