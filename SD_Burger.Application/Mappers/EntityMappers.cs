using SD_Burger.Application.DTOs;
using SD_Burger.Core.Entities;
using System.Collections.Generic;
using System.Linq;

namespace SD_Burger.Application.Mappers
{
    public static class EntityMappers
    {
        public static BranchDto ToBranchDto(this Branch branch)
        {
            if (branch == null) return null;
            
            return new BranchDto
            {
                Id = branch.Id,
                Name = branch.Name ?? string.Empty,
                Address = branch.Address ?? string.Empty,
                PhoneNumber = branch.PhoneNumber ?? string.Empty,
                Email = branch.Email ?? string.Empty,
                TableCount = branch.TableCount,
                IsActive = branch.IsActive,
                CreatedDate = branch.CreatedDate
            };
        }

        public static MenuItemDto ToMenuItemDto(this MenuItem menuItem)
        {
            if (menuItem == null) return null;
            
            return new MenuItemDto
            {
                Id = menuItem.Id,
                Name = menuItem.Name ?? string.Empty,
                Description = menuItem.Description ?? string.Empty,
                Price = menuItem.Price,
                ImageUrl = menuItem.ImageUrl ?? string.Empty,
                IsAvailable = menuItem.IsAvailable,
                CategoryId = menuItem.CategoryId,
                CategoryName = menuItem.Category?.Name ?? string.Empty,
                CreatedDate = menuItem.CreatedDate
            };
        }

        public static OrderDto ToOrderDto(this Order order)
        {
            if (order == null) return null;
            
            return new OrderDto
            {
                Id = order.Id,
                OrderNumber = order.OrderNumber ?? string.Empty,
                OrderDate = order.OrderDate,
                Status = order.Status,
                Priority = order.Priority,
                TotalAmount = order.TotalAmount,
                CustomerName = order.CustomerName ?? string.Empty,
                CustomerPhone = order.CustomerPhone ?? string.Empty,
                Notes = order.Notes ?? string.Empty,
                TableId = order.TableId,
                TableNumber = order.Table?.TableNumber ?? 0,
                BranchId = order.BranchId,
                BranchName = order.Branch?.Name ?? string.Empty,
                WaiterId = order.WaiterId,
                WaiterName = order.Waiter != null ? $"{order.Waiter.FirstName} {order.Waiter.LastName}" : string.Empty,
                CreatedDate = order.CreatedDate,
                OrderItems = order.OrderItems?.Select(oi => oi.ToOrderItemDto()).ToList() ?? new List<OrderItemDto>()
            };
        }

        public static OrderItemDto ToOrderItemDto(this OrderItem orderItem)
        {
            if (orderItem == null) return null;
            
            return new OrderItemDto
            {
                Id = orderItem.Id,
                MenuItemId = orderItem.MenuItemId,
                MenuItemName = orderItem.MenuItem?.Name ?? string.Empty,
                Quantity = orderItem.Quantity,
                UnitPrice = orderItem.UnitPrice,
                TotalPrice = orderItem.TotalPrice,
                SpecialInstructions = orderItem.SpecialInstructions ?? string.Empty
            };
        }

        public static ReservationDto ToReservationDto(this Reservation reservation)
        {
            if (reservation == null) return null;
            
            return new ReservationDto
            {
                Id = reservation.Id,
                ReservationDate = reservation.ReservationDate,
                ReservationTime = reservation.ReservationTime,
                GuestCount = reservation.GuestCount,
                CustomerName = reservation.CustomerName ?? string.Empty,
                CustomerPhone = reservation.CustomerPhone ?? string.Empty,
                CustomerEmail = reservation.CustomerEmail ?? string.Empty,
                SpecialRequests = reservation.SpecialRequests ?? string.Empty,
                Status = reservation.Status,
                TableId = reservation.TableId,
                TableNumber = reservation.Table?.TableNumber ?? 0,
                BranchId = reservation.BranchId,
                BranchName = reservation.Branch?.Name ?? string.Empty,
                UserId = reservation.UserId,
                UserName = reservation.User != null ? $"{reservation.User.FirstName} {reservation.User.LastName}" : string.Empty,
                CreatedDate = reservation.CreatedDate
            };
        }

        public static UserDto ToUserDto(this User user)
        {
            if (user == null) return null;
            
            return new UserDto
            {
                Id = user.Id,
                Username = user.Username ?? string.Empty,
                Email = user.Email ?? string.Empty,
                FirstName = user.FirstName ?? string.Empty,
                LastName = user.LastName ?? string.Empty,
                PhoneNumber = user.PhoneNumber ?? string.Empty,
                Role = user.Role,
                BranchId = user.BranchId,
                BranchName = user.Branch?.Name ?? string.Empty,
                CreatedDate = user.CreatedDate,
                IsActive = user.IsActive
            };
        }

        public static CategoryDto ToCategoryDto(this Category category)
        {
            if (category == null) return null;
            
            return new CategoryDto
            {
                Id = category.Id,
                Name = category.Name ?? string.Empty,
                Description = category.Description ?? string.Empty,
                ImageUrl = category.ImageUrl ?? string.Empty,
                DisplayOrder = category.DisplayOrder,
                IsActive = category.IsActive,
                CreatedDate = category.CreatedDate,
                UpdatedDate = category.UpdatedDate
            };
        }

        public static IngredientDto ToIngredientDto(this Ingredient ingredient)
        {
            if (ingredient == null) return null;
            
            return new IngredientDto
            {
                Id = ingredient.Id,
                Name = ingredient.Name ?? string.Empty,
                Description = ingredient.Description ?? string.Empty,
                Unit = ingredient.Unit,
                UnitPrice = ingredient.UnitPrice,
                MinimumStock = ingredient.MinimumStock,
                IsActive = ingredient.IsActive,
                CreatedDate = ingredient.CreatedDate,
                UpdatedDate = ingredient.UpdatedDate
            };
        }

        public static TableDto ToTableDto(this Table table)
        {
            if (table == null) return null;
            
            return new TableDto
            {
                Id = table.Id,
                TableNumber = table.TableNumber,
                Capacity = table.Capacity,
                Status = table.Status,
                BranchId = table.BranchId,
                BranchName = table.Branch?.Name ?? string.Empty,
                IsActive = table.IsActive,
                CreatedDate = table.CreatedDate,
                UpdatedDate = table.UpdatedDate
            };
        }

        public static InventoryDto ToInventoryDto(this Inventory inventory)
        {
            if (inventory == null) return null;
            
            return new InventoryDto
            {
                Id = inventory.Id,
                IngredientId = inventory.IngredientId,
                IngredientName = inventory.Ingredient?.Name ?? string.Empty,
                BranchId = inventory.BranchId,
                BranchName = inventory.Branch?.Name ?? string.Empty,
                CurrentStock = inventory.CurrentStock,
                LastUpdated = inventory.LastUpdated,
                IsActive = inventory.IsActive,
                CreatedDate = inventory.CreatedDate,
                UpdatedDate = inventory.UpdatedDate
            };
        }

        public static PaymentDto ToPaymentDto(this Payment payment)
        {
            if (payment == null) return null;
            
            return new PaymentDto
            {
                Id = payment.Id,
                OrderId = payment.OrderId,
                OrderNumber = payment.Order?.OrderNumber ?? string.Empty,
                Amount = payment.Amount,
                PaymentMethod = payment.PaymentMethod,
                Status = payment.Status,
                PaymentDate = payment.PaymentDate,
                TransactionId = payment.TransactionId ?? string.Empty,
                Notes = payment.Notes ?? string.Empty,
                IsActive = payment.IsActive,
                CreatedDate = payment.CreatedDate,
                UpdatedDate = payment.UpdatedDate
            };
        }

        // Legacy methods for backward compatibility
        public static BranchDto ToDto(this Branch branch) => branch.ToBranchDto();
        public static MenuItemDto ToDto(this MenuItem menuItem) => menuItem.ToMenuItemDto();
        public static OrderDto ToDto(this Order order) => order.ToOrderDto();
        public static OrderItemDto ToDto(this OrderItem orderItem) => orderItem.ToOrderItemDto();
        public static ReservationDto ToDto(this Reservation reservation) => reservation.ToReservationDto();
        public static UserDto ToDto(this User user) => user.ToUserDto();
        public static CategoryDto ToDto(this Category category) => category.ToCategoryDto();
        public static IngredientDto ToDto(this Ingredient ingredient) => ingredient.ToIngredientDto();
        public static TableDto ToDto(this Table table) => table.ToTableDto();
        public static InventoryDto ToDto(this Inventory inventory) => inventory.ToInventoryDto();
        public static PaymentDto ToDto(this Payment payment) => payment.ToPaymentDto();

        // List mapping methods
        public static IEnumerable<BranchDto> ToBranchDtoList(this IEnumerable<Branch> branches)
        {
            return branches?.Select(b => b.ToBranchDto()) ?? new List<BranchDto>();
        }

        public static IEnumerable<MenuItemDto> ToMenuItemDtoList(this IEnumerable<MenuItem> menuItems)
        {
            return menuItems?.Select(m => m.ToMenuItemDto()) ?? new List<MenuItemDto>();
        }

        public static IEnumerable<OrderDto> ToOrderDtoList(this IEnumerable<Order> orders)
        {
            return orders?.Select(o => o.ToOrderDto()) ?? new List<OrderDto>();
        }

        public static IEnumerable<ReservationDto> ToReservationDtoList(this IEnumerable<Reservation> reservations)
        {
            return reservations?.Select(r => r.ToReservationDto()) ?? new List<ReservationDto>();
        }

        public static IEnumerable<UserDto> ToUserDtoList(this IEnumerable<User> users)
        {
            return users?.Select(u => u.ToUserDto()) ?? new List<UserDto>();
        }

        public static IEnumerable<CategoryDto> ToCategoryDtoList(this IEnumerable<Category> categories)
        {
            return categories?.Select(c => c.ToCategoryDto()) ?? new List<CategoryDto>();
        }

        public static IEnumerable<IngredientDto> ToIngredientDtoList(this IEnumerable<Ingredient> ingredients)
        {
            return ingredients?.Select(i => i.ToIngredientDto()) ?? new List<IngredientDto>();
        }

        public static IEnumerable<TableDto> ToTableDtoList(this IEnumerable<Table> tables)
        {
            return tables?.Select(t => t.ToTableDto()) ?? new List<TableDto>();
        }

        public static IEnumerable<InventoryDto> ToInventoryDtoList(this IEnumerable<Inventory> inventories)
        {
            return inventories?.Select(i => i.ToInventoryDto()) ?? new List<InventoryDto>();
        }

        public static IEnumerable<PaymentDto> ToPaymentDtoList(this IEnumerable<Payment> payments)
        {
            return payments?.Select(p => p.ToPaymentDto()) ?? new List<PaymentDto>();
        }

        // Legacy list methods for backward compatibility
        public static IEnumerable<BranchDto> ToDtoList(this IEnumerable<Branch> branches) => branches.ToBranchDtoList();
        public static IEnumerable<MenuItemDto> ToDtoList(this IEnumerable<MenuItem> menuItems) => menuItems.ToMenuItemDtoList();
        public static IEnumerable<OrderDto> ToDtoList(this IEnumerable<Order> orders) => orders.ToOrderDtoList();
        public static IEnumerable<ReservationDto> ToDtoList(this IEnumerable<Reservation> reservations) => reservations.ToReservationDtoList();
        public static IEnumerable<UserDto> ToDtoList(this IEnumerable<User> users) => users.ToUserDtoList();
        public static IEnumerable<CategoryDto> ToDtoList(this IEnumerable<Category> categories) => categories.ToCategoryDtoList();
        public static IEnumerable<IngredientDto> ToDtoList(this IEnumerable<Ingredient> ingredients) => ingredients.ToIngredientDtoList();
        public static IEnumerable<TableDto> ToDtoList(this IEnumerable<Table> tables) => tables.ToTableDtoList();
        public static IEnumerable<InventoryDto> ToDtoList(this IEnumerable<Inventory> inventories) => inventories.ToInventoryDtoList();
        public static IEnumerable<PaymentDto> ToDtoList(this IEnumerable<Payment> payments) => payments.ToPaymentDtoList();
    }
} 