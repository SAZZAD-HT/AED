using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class User
    {
        [Key]
        public int UserID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
    }
}
