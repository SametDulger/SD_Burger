namespace SD_Burger.Web.Models
{
    public class UserReportViewModel
    {
        public int TotalUsers { get; set; }
        public int ActiveUsers { get; set; }
        public int InactiveUsers { get; set; }
        public int NewUsersThisMonth { get; set; }
        public List<UserViewModel> Users { get; set; } = new();
        public List<UserViewModel> RecentUsers { get; set; } = new();
        public List<RoleDistributionViewModel> RoleDistribution { get; set; } = new();
        public List<UserRoleData> UserRoles { get; set; } = new();
        public List<UserViewModel> RecentRegistrations { get; set; } = new();
    }

    public class RoleDistributionViewModel
    {
        public string Role { get; set; } = string.Empty;
        public int Count { get; set; }
        public double Percentage { get; set; }
    }
} 