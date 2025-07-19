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
    public class OrderService : IOrderService
    {
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<OrderDto> GetByIdAsync(int id)
        {
            var order = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .FirstOrDefaultAsync(o => o.Id == id && o.IsActive);

            return order?.ToDto();
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            var orders = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.IsActive)
                .ToListAsync();

            return orders.ToDtoList();
        }

        public async Task<OrderDto> CreateAsync(CreateOrderDto createOrderDto)
        {
            var orderNumber = await GenerateOrderNumberAsync();
            var totalAmount = 0m;

            var order = new Order
            {
                OrderNumber = orderNumber,
                OrderDate = DateTime.Now,
                Status = OrderStatus.Received,
                Priority = createOrderDto.Priority,
                CustomerName = createOrderDto.CustomerName,
                CustomerPhone = createOrderDto.CustomerPhone,
                Notes = createOrderDto.Notes,
                TableId = createOrderDto.TableId,
                BranchId = createOrderDto.BranchId,
                WaiterId = createOrderDto.WaiterId
            };

            await _unitOfWork.Repository<Order>().AddAsync(order);
            await _unitOfWork.SaveChangesAsync();

            if (createOrderDto.OrderItems != null)
            {
                foreach (var item in createOrderDto.OrderItems)
                {
                    var menuItem = await _unitOfWork.Repository<MenuItem>().GetByIdAsync(item.MenuItemId);
                    if (menuItem != null)
                    {
                        var orderItem = new OrderItem
                        {
                            OrderId = order.Id,
                            MenuItemId = item.MenuItemId,
                            Quantity = item.Quantity,
                            UnitPrice = menuItem.Price,
                            TotalPrice = menuItem.Price * item.Quantity,
                            SpecialInstructions = item.SpecialInstructions
                        };

                        await _unitOfWork.Repository<OrderItem>().AddAsync(orderItem);
                        totalAmount += orderItem.TotalPrice;
                    }
                }

                order.TotalAmount = totalAmount;
                await _unitOfWork.Repository<Order>().UpdateAsync(order);
                await _unitOfWork.SaveChangesAsync();
            }

            return await GetByIdAsync(order.Id);
        }

        public async Task<OrderDto> UpdateAsync(int id, UpdateOrderDto updateOrderDto)
        {
            var order = await _unitOfWork.Repository<Order>().GetByIdAsync(id);
            if (order == null)
                throw new InvalidOperationException("Sipariş bulunamadı.");

            order.Status = updateOrderDto.Status;
            order.Priority = updateOrderDto.Priority;
            order.CustomerName = updateOrderDto.CustomerName;
            order.CustomerPhone = updateOrderDto.CustomerPhone;
            order.Notes = updateOrderDto.Notes;
            order.WaiterId = updateOrderDto.WaiterId;

            await _unitOfWork.Repository<Order>().UpdateAsync(order);
            await _unitOfWork.SaveChangesAsync();

            return await GetByIdAsync(id);
        }

        public async Task DeleteAsync(int id)
        {
            await _unitOfWork.Repository<Order>().DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();
        }

        public async Task<IEnumerable<OrderDto>> GetByBranchAsync(int branchId)
        {
            var orders = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.BranchId == branchId && o.IsActive)
                .ToListAsync();

            return orders.ToDtoList();
        }

        public async Task<IEnumerable<OrderDto>> GetByTableAsync(int tableId)
        {
            var orders = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.TableId == tableId && o.IsActive)
                .ToListAsync();

            return orders.ToDtoList();
        }

        public async Task<IEnumerable<OrderDto>> GetByStatusAsync(OrderStatus status)
        {
            var orders = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.Status == status && o.IsActive)
                .ToListAsync();

            return orders.ToDtoList();
        }

        public async Task<IEnumerable<OrderDto>> GetByWaiterAsync(int waiterId)
        {
            var orders = await _unitOfWork.Repository<Order>().Query()
                .Include(o => o.Table)
                .Include(o => o.Branch)
                .Include(o => o.Waiter)
                .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
                .Where(o => o.WaiterId == waiterId && o.IsActive)
                .ToListAsync();

            return orders.ToDtoList();
        }

        public async Task<string> GenerateOrderNumberAsync()
        {
            var today = DateTime.Now.Date;
            var orderCount = await _unitOfWork.Repository<Order>().Query()
                .CountAsync(o => o.OrderDate.Date == today);

            return $"ORD-{today:yyyyMMdd}-{orderCount + 1:D4}";
        }

        public async Task<SalesReportDto> GetSalesReportAsync(DateTime? startDate, DateTime? endDate)
        {
            var start = startDate ?? DateTime.Today.AddDays(-30);
            var end = endDate ?? DateTime.Today;

            var orders = await _unitOfWork.Repository<Order>().Query()
                .Where(o => o.OrderDate >= start && o.OrderDate <= end && o.IsActive)
                .ToListAsync();

            var report = new SalesReportDto
            {
                StartDate = start,
                EndDate = end,
                TotalSales = orders.Sum(o => o.TotalAmount),
                TotalOrders = orders.Count,
                AverageOrderValue = orders.Any() ? orders.Average(o => o.TotalAmount) : 0,
                ActiveOrders = orders.Count(o => o.Status != OrderStatus.Completed && o.Status != OrderStatus.Cancelled)
            };

            return report;
        }

        public async Task<int> GetTotalOrdersAsync()
        {
            return await _unitOfWork.Repository<Order>().Query()
                .CountAsync(o => o.IsActive);
        }

        public async Task<decimal> GetTotalSalesAsync()
        {
            return await _unitOfWork.Repository<Order>().Query()
                .Where(o => o.IsActive)
                .SumAsync(o => o.TotalAmount);
        }
    }
} 