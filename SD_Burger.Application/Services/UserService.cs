using Microsoft.EntityFrameworkCore;
using SD_Burger.Application.DTOs;
using SD_Burger.Application.Mappers;
using SD_Burger.Core.Entities;
using SD_Burger.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> GetByIdAsync(int id)
        {
            var user = await _unitOfWork.Repository<User>().Query()
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(u => u.Id == id && u.IsActive);

            return user?.ToDto();
        }

        public async Task<IEnumerable<UserDto>> GetAllAsync()
        {
            var users = await _unitOfWork.Repository<User>().Query()
                .Include(u => u.Branch)
                .Where(u => u.IsActive)
                .ToListAsync();

            return users.ToDtoList();
        }

        public async Task<UserDto> CreateAsync(CreateUserDto createUserDto)
        {
            var existingUser = await _unitOfWork.Repository<User>().Query()
                .FirstOrDefaultAsync(u => u.Username == createUserDto.Username || u.Email == createUserDto.Email);

            if (existingUser != null)
                throw new InvalidOperationException("Kullanıcı adı veya email zaten kullanımda.");

            var user = new User
            {
                Username = createUserDto.Username,
                Email = createUserDto.Email,
                PasswordHash = HashPassword(createUserDto.Password),
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                PhoneNumber = createUserDto.PhoneNumber,
                Role = createUserDto.Role,
                BranchId = createUserDto.BranchId
            };

            await _unitOfWork.Repository<User>().AddAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(user.Id);
        }

        public async Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto)
        {
            var user = await _unitOfWork.Repository<User>().GetByIdAsync(id);
            if (user == null)
                throw new InvalidOperationException("Kullanıcı bulunamadı.");

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.PhoneNumber = updateUserDto.PhoneNumber;
            user.Role = updateUserDto.Role;
            user.BranchId = updateUserDto.BranchId;

            await _unitOfWork.Repository<User>().UpdateAsync(user);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<User>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<UserDto> LoginAsync(LoginDto loginDto)
        {
            var hashedPassword = HashPassword(loginDto.Password);
            var user = await _unitOfWork.Repository<User>().Query()
                .Include(u => u.Branch)
                .FirstOrDefaultAsync(u => u.Username == loginDto.Username && 
                                        u.PasswordHash == hashedPassword && 
                                        u.IsActive);

            return user?.ToDto();
        }

        public async Task<IEnumerable<UserDto>> GetByRoleAsync(UserRole role)
        {
            var users = await _unitOfWork.Repository<User>().Query()
                .Include(u => u.Branch)
                .Where(u => u.Role == role && u.IsActive)
                .ToListAsync();

            return users.ToDtoList();
        }

        public async Task<IEnumerable<UserDto>> GetByBranchAsync(int branchId)
        {
            var users = await _unitOfWork.Repository<User>().Query()
                .Include(u => u.Branch)
                .Where(u => u.BranchId == branchId && u.IsActive)
                .ToListAsync();

            return users.ToDtoList();
        }

        private string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashedBytes);
            }
        }

        public async Task<UserReportDto> GetUserReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Today.AddDays(-30);
            var end = endDate ?? DateTime.Today;

            var users = await _unitOfWork.Repository<User>().Query()
                .Where(u => u.CreatedDate >= start && u.CreatedDate <= end)
                .ToListAsync();

            var report = new UserReportDto
            {
                StartDate = start,
                EndDate = end,
                TotalUsers = await GetActiveUsersCountAsync(),
                ActiveUsers = users.Count(u => u.IsActive),
                NewUsersThisMonth = users.Count(u => u.CreatedDate.Month == DateTime.Now.Month && u.CreatedDate.Year == DateTime.Now.Year)
            };

            return report;
        }

        public async Task<int> GetActiveUsersCountAsync()
        {
            return await _unitOfWork.Repository<User>().Query()
                .CountAsync(u => u.IsActive);
        }
    }
} 