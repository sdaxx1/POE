using MessagePack;
using Microsoft.Build.Framework;

namespace POE_ST10152183.Models
{
    public class AdminAccounts
    {
        public AdminAccounts(int id, string adminUserName, string adminPassword)
        {
            Id = id;
            AdminUserName = adminUserName;
            AdminPassword = adminPassword;
        }

        public AdminAccounts()
        {
        }
        public int Id { get; set; }
        public string? AdminUserName { get; set; }
        public string? AdminPassword { get; set; }
    }
}
