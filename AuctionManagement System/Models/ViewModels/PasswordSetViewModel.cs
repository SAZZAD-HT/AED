using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.ViewModels
{
    public class PasswordSetViewModel
    {
        public string Password { get; set; } 
        public string ConfirmPassword { get; set; }
    }
}
