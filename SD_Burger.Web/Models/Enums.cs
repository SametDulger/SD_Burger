namespace SD_Burger.Web.Models
{
    public enum UserRole
    {
        Admin = 0,
        Manager = 1,
        Waiter = 2,
        Cashier = 3
    }

    public enum TableStatus
    {
        Available = 0,
        Occupied = 1,
        Reserved = 2,
        Maintenance = 3
    }

    public enum ReservationStatus
    {
        Confirmed = 0,
        Pending = 1,
        Cancelled = 2,
        Completed = 3
    }

    public enum PaymentMethod
    {
        Cash = 0,
        CreditCard = 1,
        DebitCard = 2,
        OnlinePayment = 3,
        MobilePayment = 4
    }

    public enum PaymentStatus
    {
        Pending = 0,
        Completed = 1,
        Failed = 2,
        Refunded = 3,
        Cancelled = 4
    }

    public enum OrderStatus
    {
        Pending = 0,
        Preparing = 1,
        Ready = 2,
        Served = 3,
        Completed = 4,
        Cancelled = 5
    }

    public enum OrderPriority
    {
        Low = 0,
        Normal = 1,
        High = 2,
        Urgent = 3
    }
} 