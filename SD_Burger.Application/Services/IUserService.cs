using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IUserService
    {
        Task<UserDto> GetByIdAsync(int id);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto> CreateAsync(CreateUserDto createUserDto);
        Task<UserDto> UpdateAsync(int id, UpdateUserDto updateUserDto);
        Task DeleteAsync(int id);
        Task<UserDto> LoginAsync(LoginDto loginDto);
        Task<IEnumerable<UserDto>> GetByRoleAsync(Core.Entities.UserRole role);
        Task<IEnumerable<UserDto>> GetByBranchAsync(int branchId);
        Task<UserReportDto> GetUserReportAsync(DateTime? startDate, DateTime? endDate);
        Task<int> GetActiveUsersCountAsync();
    }
} 