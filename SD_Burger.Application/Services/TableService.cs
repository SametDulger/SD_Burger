using SD_Burger.Application.DTOs;
using SD_Burger.Application.Mappers;
using SD_Burger.Core.Entities;
using SD_Burger.Core.Repositories;

namespace SD_Burger.Application.Services
{
    public class TableService : ITableService
    {
        private readonly IGenericRepository<Table> _tableRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TableService(IGenericRepository<Table> tableRepository, IUnitOfWork unitOfWork)
        {
            _tableRepository = tableRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<TableDto>> GetAllAsync()
        {
            var tables = await _tableRepository.GetAllAsync();
            return tables.Select(EntityMappers.ToTableDto);
        }

        public async Task<TableDto> GetByIdAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                throw new ArgumentException($"Table with ID {id} not found.");

            return EntityMappers.ToTableDto(table);
        }

        public async Task<TableDto> CreateAsync(CreateTableDto createTableDto)
        {
            var table = new Table
            {
                TableNumber = createTableDto.TableNumber,
                Capacity = createTableDto.Capacity,
                Status = createTableDto.Status,
                BranchId = createTableDto.BranchId,
                CreatedDate = DateTime.UtcNow,
                UpdatedDate = DateTime.UtcNow
            };

            await _tableRepository.AddAsync(table);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToTableDto(table);
        }

        public async Task<TableDto> UpdateAsync(int id, UpdateTableDto updateTableDto)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                throw new ArgumentException($"Table with ID {id} not found.");

            table.TableNumber = updateTableDto.TableNumber;
            table.Capacity = updateTableDto.Capacity;
            table.Status = updateTableDto.Status;
            table.BranchId = updateTableDto.BranchId;
            table.UpdatedDate = DateTime.UtcNow;

            await _tableRepository.UpdateAsync(table);
            await _unitOfWork.SaveChangesAsync();

            return EntityMappers.ToTableDto(table);
        }

        public async Task DeleteAsync(int id)
        {
            var table = await _tableRepository.GetByIdAsync(id);
            if (table == null)
                throw new ArgumentException($"Table with ID {id} not found.");

            await _tableRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }
    }
} 