namespace AuctionManagement_System.Models.ViewModels
{
    public class AdminViewModel
    {
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }
        
        public string Email { get; set; }
        public string MobileNumber { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedTime { get; set; }
    }
}
