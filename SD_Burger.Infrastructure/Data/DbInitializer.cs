using Microsoft.EntityFrameworkCore;
using SD_Burger.Core.Entities;

namespace SD_Burger.Infrastructure.Data
{
    public static class DbInitializer
    {
        public static void SeedData(ModelBuilder modelBuilder)
        {
            var now = new DateTime(2024, 1, 1, 12, 0, 0);
            var yesterday = now.AddDays(-1);
            var twoDaysAgo = now.AddDays(-2);
            var tomorrow = now.AddDays(1);
            var dayAfterTomorrow = now.AddDays(2);

            // Branches
            modelBuilder.Entity<Branch>().HasData(
                new Branch
                {
                    Id = 1,
                    Name = "Merkez Şube",
                    Address = "Atatürk Caddesi No:123, İstanbul",
                    PhoneNumber = "0212 555 0123",
                    Email = "merkez@sdburger.com",
                    TableCount = 15,
                    CreatedDate = now,
                    IsActive = true
                },
                new Branch
                {
                    Id = 2,
                    Name = "Kadıköy Şube",
                    Address = "Moda Caddesi No:45, İstanbul",
                    PhoneNumber = "0216 555 0456",
                    Email = "kadikoy@sdburger.com",
                    TableCount = 12,
                    CreatedDate = now,
                    IsActive = true
                },
                new Branch
                {
                    Id = 3,
                    Name = "Beşiktaş Şube",
                    Address = "Barbaros Bulvarı No:67, İstanbul",
                    PhoneNumber = "0212 555 0789",
                    Email = "besiktas@sdburger.com",
                    TableCount = 18,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // Categories
            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    Id = 1,
                    Name = "Burgerler",
                    Description = "Özel soslu ve taze malzemelerle hazırlanan burgerler",
                    ImageUrl = "/images/categories/burgers.jpg",
                    DisplayOrder = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new Category
                {
                    Id = 2,
                    Name = "İçecekler",
                    Description = "Soğuk ve sıcak içecekler",
                    ImageUrl = "/images/categories/drinks.jpg",
                    DisplayOrder = 2,
                    CreatedDate = now,
                    IsActive = true
                },
                new Category
                {
                    Id = 3,
                    Name = "Tatlılar",
                    Description = "Ev yapımı tatlılar",
                    ImageUrl = "/images/categories/desserts.jpg",
                    DisplayOrder = 3,
                    CreatedDate = now,
                    IsActive = true
                },
                new Category
                {
                    Id = 4,
                    Name = "Yan Ürünler",
                    Description = "Patates kızartması, soğan halkası vb.",
                    ImageUrl = "/images/categories/sides.jpg",
                    DisplayOrder = 4,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // Ingredients
            modelBuilder.Entity<Ingredient>().HasData(
                new Ingredient
                {
                    Id = 1,
                    Name = "Dana Eti",
                    Description = "Taze dana eti",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 120.00m,
                    MinimumStock = 50,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 2,
                    Name = "Tavuk Eti",
                    Description = "Taze tavuk göğsü",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 45.00m,
                    MinimumStock = 30,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 3,
                    Name = "Ekmek",
                    Description = "Taze burger ekmeği",
                    Unit = IngredientUnit.Piece,
                    UnitPrice = 2.50m,
                    MinimumStock = 200,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 4,
                    Name = "Marul",
                    Description = "Taze marul yaprağı",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 8.00m,
                    MinimumStock = 20,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 5,
                    Name = "Domates",
                    Description = "Taze domates",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 12.00m,
                    MinimumStock = 25,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 6,
                    Name = "Soğan",
                    Description = "Taze soğan",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 6.00m,
                    MinimumStock = 30,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 7,
                    Name = "Peynir",
                    Description = "Cheddar peyniri",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 35.00m,
                    MinimumStock = 15,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 8,
                    Name = "Patates",
                    Description = "Taze patates",
                    Unit = IngredientUnit.Kilogram,
                    UnitPrice = 5.00m,
                    MinimumStock = 100,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 9,
                    Name = "Kola",
                    Description = "Coca Cola",
                    Unit = IngredientUnit.Piece,
                    UnitPrice = 8.00m,
                    MinimumStock = 150,
                    CreatedDate = now,
                    IsActive = true
                },
                new Ingredient
                {
                    Id = 10,
                    Name = "Süt",
                    Description = "Taze süt",
                    Unit = IngredientUnit.Liter,
                    UnitPrice = 15.00m,
                    MinimumStock = 20,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // Tables
            modelBuilder.Entity<Table>().HasData(
                new Table { Id = 1, TableNumber = 1, Capacity = 4, Status = TableStatus.Available, BranchId = 1, CreatedDate = now, IsActive = true },
                new Table { Id = 2, TableNumber = 2, Capacity = 4, Status = TableStatus.Available, BranchId = 1, CreatedDate = now, IsActive = true },
                new Table { Id = 3, TableNumber = 3, Capacity = 6, Status = TableStatus.Available, BranchId = 1, CreatedDate = now, IsActive = true },
                new Table { Id = 4, TableNumber = 4, Capacity = 2, Status = TableStatus.Available, BranchId = 1, CreatedDate = now, IsActive = true },
                new Table { Id = 5, TableNumber = 5, Capacity = 4, Status = TableStatus.Available, BranchId = 1, CreatedDate = now, IsActive = true },
                new Table { Id = 6, TableNumber = 1, Capacity = 4, Status = TableStatus.Available, BranchId = 2, CreatedDate = now, IsActive = true },
                new Table { Id = 7, TableNumber = 2, Capacity = 4, Status = TableStatus.Available, BranchId = 2, CreatedDate = now, IsActive = true },
                new Table { Id = 8, TableNumber = 3, Capacity = 6, Status = TableStatus.Available, BranchId = 2, CreatedDate = now, IsActive = true },
                new Table { Id = 9, TableNumber = 1, Capacity = 4, Status = TableStatus.Available, BranchId = 3, CreatedDate = now, IsActive = true },
                new Table { Id = 10, TableNumber = 2, Capacity = 4, Status = TableStatus.Available, BranchId = 3, CreatedDate = now, IsActive = true },
                new Table { Id = 11, TableNumber = 3, Capacity = 6, Status = TableStatus.Available, BranchId = 3, CreatedDate = now, IsActive = true },
                new Table { Id = 12, TableNumber = 4, Capacity = 8, Status = TableStatus.Available, BranchId = 3, CreatedDate = now, IsActive = true }
            );

            // Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Username = "admin",
                    Email = "admin@sdburger.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbXpQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQ==",
                    FirstName = "Admin",
                    LastName = "User",
                    PhoneNumber = "0555 123 4567",
                    Role = UserRole.Admin,
                    BranchId = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new User
                {
                    Id = 2,
                    Username = "waiter1",
                    Email = "waiter1@sdburger.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbXpQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQ==",
                    FirstName = "Ahmet",
                    LastName = "Yılmaz",
                    PhoneNumber = "0555 234 5678",
                    Role = UserRole.Waiter,
                    BranchId = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new User
                {
                    Id = 3,
                    Username = "waiter2",
                    Email = "waiter2@sdburger.com",
                    PasswordHash = "AQAAAAIAAYagAAAAELbXpQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQJ8B4iQ==",
                    FirstName = "Ayşe",
                    LastName = "Demir",
                    PhoneNumber = "0555 345 6789",
                    Role = UserRole.Waiter,
                    BranchId = 2,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // MenuItems
            modelBuilder.Entity<MenuItem>().HasData(
                new MenuItem
                {
                    Id = 1,
                    Name = "Klasik Burger",
                    Description = "Dana eti, marul, domates, soğan ve özel sos ile",
                    Price = 45.00m,
                    ImageUrl = "/images/menu/classic-burger.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 2,
                    Name = "Tavuk Burger",
                    Description = "Tavuk göğsü, marul, domates ve ranch sos ile",
                    Price = 35.00m,
                    ImageUrl = "/images/menu/chicken-burger.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 3,
                    Name = "Çifte Burger",
                    Description = "İki kat dana eti, çifte peynir ve özel sos ile",
                    Price = 65.00m,
                    ImageUrl = "/images/menu/double-burger.jpg",
                    IsAvailable = true,
                    CategoryId = 1,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 4,
                    Name = "Patates Kızartması",
                    Description = "Taze patates kızartması",
                    Price = 15.00m,
                    ImageUrl = "/images/menu/fries.jpg",
                    IsAvailable = true,
                    CategoryId = 4,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 5,
                    Name = "Soğan Halkası",
                    Description = "Çıtır çıtır soğan halkası",
                    Price = 18.00m,
                    ImageUrl = "/images/menu/onion-rings.jpg",
                    IsAvailable = true,
                    CategoryId = 4,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 6,
                    Name = "Kola",
                    Description = "Coca Cola 330ml",
                    Price = 12.00m,
                    ImageUrl = "/images/menu/cola.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 7,
                    Name = "Milkshake",
                    Description = "Çikolata, vanilya veya çilek aromalı",
                    Price = 20.00m,
                    ImageUrl = "/images/menu/milkshake.jpg",
                    IsAvailable = true,
                    CategoryId = 2,
                    CreatedDate = now,
                    IsActive = true
                },
                new MenuItem
                {
                    Id = 8,
                    Name = "Çikolatalı Brownie",
                    Description = "Sıcak çikolata soslu brownie",
                    Price = 25.00m,
                    ImageUrl = "/images/menu/brownie.jpg",
                    IsAvailable = true,
                    CategoryId = 3,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // MenuItemIngredients
            modelBuilder.Entity<MenuItemIngredient>().HasData(
                new MenuItemIngredient { Id = 1, MenuItemId = 1, IngredientId = 1, Quantity = 0.15m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 2, MenuItemId = 1, IngredientId = 3, Quantity = 1, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 3, MenuItemId = 1, IngredientId = 4, Quantity = 0.02m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 4, MenuItemId = 1, IngredientId = 5, Quantity = 0.03m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 5, MenuItemId = 1, IngredientId = 6, Quantity = 0.02m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 6, MenuItemId = 2, IngredientId = 2, Quantity = 0.12m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 7, MenuItemId = 2, IngredientId = 3, Quantity = 1, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 8, MenuItemId = 2, IngredientId = 4, Quantity = 0.02m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 9, MenuItemId = 3, IngredientId = 1, Quantity = 0.25m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 10, MenuItemId = 3, IngredientId = 7, Quantity = 0.05m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 11, MenuItemId = 4, IngredientId = 8, Quantity = 0.2m, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 12, MenuItemId = 6, IngredientId = 9, Quantity = 1, CreatedDate = now, IsActive = true },
                new MenuItemIngredient { Id = 13, MenuItemId = 7, IngredientId = 10, Quantity = 0.3m, CreatedDate = now, IsActive = true }
            );

            // Inventories
            modelBuilder.Entity<Inventory>().HasData(
                new Inventory { Id = 1, IngredientId = 1, BranchId = 1, CurrentStock = 25.5m, Quantity = 25.5m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 2, IngredientId = 2, BranchId = 1, CurrentStock = 18.2m, Quantity = 18.2m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 3, IngredientId = 3, BranchId = 1, CurrentStock = 150, Quantity = 150, Unit = "adet", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 4, IngredientId = 4, BranchId = 1, CurrentStock = 12.5m, Quantity = 12.5m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 5, IngredientId = 5, BranchId = 1, CurrentStock = 15.8m, Quantity = 15.8m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 6, IngredientId = 6, BranchId = 1, CurrentStock = 20.3m, Quantity = 20.3m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 7, IngredientId = 7, BranchId = 1, CurrentStock = 8.7m, Quantity = 8.7m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 8, IngredientId = 8, BranchId = 1, CurrentStock = 45.2m, Quantity = 45.2m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 9, IngredientId = 9, BranchId = 1, CurrentStock = 120, Quantity = 120, Unit = "adet", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 10, IngredientId = 10, BranchId = 1, CurrentStock = 12.5m, Quantity = 12.5m, Unit = "L", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 11, IngredientId = 1, BranchId = 2, CurrentStock = 22.1m, Quantity = 22.1m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 12, IngredientId = 2, BranchId = 2, CurrentStock = 16.8m, Quantity = 16.8m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 13, IngredientId = 3, BranchId = 2, CurrentStock = 130, Quantity = 130, Unit = "adet", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 14, IngredientId = 1, BranchId = 3, CurrentStock = 28.9m, Quantity = 28.9m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 15, IngredientId = 2, BranchId = 3, CurrentStock = 19.5m, Quantity = 19.5m, Unit = "kg", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now },
                new Inventory { Id = 16, IngredientId = 3, BranchId = 3, CurrentStock = 180, Quantity = 180, Unit = "adet", LastUpdated = now, IsActive = true, CreatedDate = now, UpdatedDate = now }
            );

            // Sample Orders
            modelBuilder.Entity<Order>().HasData(
                new Order
                {
                    Id = 1,
                    OrderNumber = "ORD-2024-001",
                    OrderDate = twoDaysAgo,
                    Status = OrderStatus.Served,
                    Priority = OrderPriority.Urgent,
                    TotalAmount = 67.00m,
                    CustomerName = "Mehmet Yılmaz",
                    CustomerPhone = "0555 111 2222",
                    Notes = "Soğan halkası ekstra çıtır olsun",
                    TableId = 1,
                    BranchId = 1,
                    WaiterId = 2,
                    CreatedDate = twoDaysAgo,
                    IsActive = true
                },
                new Order
                {
                    Id = 2,
                    OrderNumber = "ORD-2024-002",
                    OrderDate = yesterday,
                    Status = OrderStatus.Served,
                    Priority = OrderPriority.Normal,
                    TotalAmount = 45.00m,
                    CustomerName = "Ayşe Demir",
                    CustomerPhone = "0555 333 4444",
                    Notes = "",
                    TableId = 3,
                    BranchId = 1,
                    WaiterId = 2,
                    CreatedDate = yesterday,
                    IsActive = true
                },
                new Order
                {
                    Id = 3,
                    OrderNumber = "ORD-2024-003",
                    OrderDate = now,
                    Status = OrderStatus.Preparing,
                    Priority = OrderPriority.Urgent,
                    TotalAmount = 89.00m,
                    CustomerName = "Ali Kaya",
                    CustomerPhone = "0555 555 6666",
                    Notes = "Çifte burger ekstra peynirli olsun",
                    TableId = 2,
                    BranchId = 2,
                    WaiterId = 3,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // OrderItems
            modelBuilder.Entity<OrderItem>().HasData(
                new OrderItem { Id = 1, OrderId = 1, MenuItemId = 1, Quantity = 1, UnitPrice = 45.00m, TotalPrice = 45.00m, SpecialInstructions = "", CreatedDate = twoDaysAgo, IsActive = true },
                new OrderItem { Id = 2, OrderId = 1, MenuItemId = 5, Quantity = 1, UnitPrice = 18.00m, TotalPrice = 18.00m, SpecialInstructions = "Ekstra çıtır", CreatedDate = twoDaysAgo, IsActive = true },
                new OrderItem { Id = 3, OrderId = 1, MenuItemId = 6, Quantity = 1, UnitPrice = 12.00m, TotalPrice = 12.00m, SpecialInstructions = "", CreatedDate = twoDaysAgo, IsActive = true },
                new OrderItem { Id = 4, OrderId = 2, MenuItemId = 1, Quantity = 1, UnitPrice = 45.00m, TotalPrice = 45.00m, SpecialInstructions = "", CreatedDate = yesterday, IsActive = true },
                new OrderItem { Id = 5, OrderId = 3, MenuItemId = 3, Quantity = 1, UnitPrice = 65.00m, TotalPrice = 65.00m, SpecialInstructions = "Ekstra peynir", CreatedDate = now, IsActive = true },
                new OrderItem { Id = 6, OrderId = 3, MenuItemId = 4, Quantity = 1, UnitPrice = 15.00m, TotalPrice = 15.00m, SpecialInstructions = "", CreatedDate = now, IsActive = true },
                new OrderItem { Id = 7, OrderId = 3, MenuItemId = 6, Quantity = 1, UnitPrice = 12.00m, TotalPrice = 12.00m, SpecialInstructions = "", CreatedDate = now, IsActive = true }
            );

            // Sample Reservations
            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationDate = tomorrow,
                    ReservationTime = new TimeSpan(19, 0, 0),
                    GuestCount = 4,
                    CustomerName = "Fatma Özkan",
                    CustomerPhone = "0555 777 8888",
                    CustomerEmail = "fatma@email.com",
                    SpecialRequests = "Pencere kenarı masa olsun",
                    Status = ReservationStatus.Confirmed,
                    TableId = 1,
                    BranchId = 1,
                    UserId = 2,
                    CreatedDate = now,
                    IsActive = true
                },
                new Reservation
                {
                    Id = 2,
                    ReservationDate = dayAfterTomorrow,
                    ReservationTime = new TimeSpan(20, 30, 0),
                    GuestCount = 6,
                    CustomerName = "Mustafa Çelik",
                    CustomerPhone = "0555 999 0000",
                    CustomerEmail = "mustafa@email.com",
                    SpecialRequests = "",
                    Status = ReservationStatus.Confirmed,
                    TableId = 3,
                    BranchId = 2,
                    UserId = 3,
                    CreatedDate = now,
                    IsActive = true
                }
            );

            // Sample Payments
            modelBuilder.Entity<Payment>().HasData(
                new Payment
                {
                    Id = 1,
                    OrderId = 1,
                    Amount = 67.00m,
                    PaymentMethod = PaymentMethod.Cash,
                    Status = PaymentStatus.Completed,
                    PaymentDate = twoDaysAgo,
                    TransactionId = "TXN-001",
                    Notes = "Nakit ödeme",
                    IsActive = true,
                    CreatedDate = twoDaysAgo,
                    UpdatedDate = twoDaysAgo
                },
                new Payment
                {
                    Id = 2,
                    OrderId = 2,
                    Amount = 45.00m,
                    PaymentMethod = PaymentMethod.CreditCard,
                    Status = PaymentStatus.Completed,
                    PaymentDate = yesterday,
                    TransactionId = "TXN-002",
                    Notes = "Kredi kartı ile ödeme",
                    IsActive = true,
                    CreatedDate = yesterday,
                    UpdatedDate = yesterday
                }
            );
        }
    }
} 