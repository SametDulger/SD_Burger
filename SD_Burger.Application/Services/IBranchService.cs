using SD_Burger.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SD_Burger.Application.Services
{
    public interface IBranchService
    {
        Task<BranchDto> GetByIdAsync(int id);
        Task<IEnumerable<BranchDto>> GetAllAsync();
        Task<BranchDto> CreateAsync(CreateBranchDto createBranchDto);
        Task<BranchDto> UpdateAsync(int id, UpdateBranchDto updateBranchDto);
        Task DeleteAsync(int id);
        Task<IEnumerable<BranchDto>> GetActiveBranchesAsync();
    }
} 