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
    public class BranchService : IBranchService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BranchService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<BranchDto> GetByIdAsync(int id)
        {
            var branch = await _unitOfWork.Repository<Branch>().Query()
                .FirstOrDefaultAsync(b => b.Id == id && b.IsActive);

            if (branch == null)
                return null;

            return branch.ToDto();
        }

        public async Task<IEnumerable<BranchDto>> GetAllAsync()
        {
            var branches = await _unitOfWork.Repository<Branch>().Query()
                .Where(b => b.IsActive)
                .ToListAsync();

            return branches.ToDtoList();
        }

        public async Task<BranchDto> CreateAsync(CreateBranchDto createBranchDto)
        {
            var branch = new Branch
            {
                Name = createBranchDto.Name,
                Address = createBranchDto.Address,
                PhoneNumber = createBranchDto.PhoneNumber,
                Email = createBranchDto.Email,
                TableCount = createBranchDto.TableCount
            };

            await _unitOfWork.Repository<Branch>().AddAsync(branch);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(branch.Id);
        }

        public async Task<BranchDto> UpdateAsync(int id, UpdateBranchDto updateBranchDto)
        {
            var branch = await _unitOfWork.Repository<Branch>().GetByIdAsync(id);
            if (branch == null)
                throw new InvalidOperationException("Şube bulunamadı.");

            branch.Name = updateBranchDto.Name;
            branch.Address = updateBranchDto.Address;
            branch.PhoneNumber = updateBranchDto.PhoneNumber;
            branch.Email = updateBranchDto.Email;
            branch.TableCount = updateBranchDto.TableCount;
            branch.IsActive = updateBranchDto.IsActive;

            await _unitOfWork.Repository<Branch>().UpdateAsync(branch);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Branch>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<BranchDto>> GetActiveBranchesAsync()
        {
            var branches = await _unitOfWork.Repository<Branch>().Query()
                .Where(b => b.IsActive)
                .ToListAsync();

            return branches.ToDtoList();
        }
    }
} 