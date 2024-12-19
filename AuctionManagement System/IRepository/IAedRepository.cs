using AuctionManagement_System.Models.Database.Tables;
using AuctionManagement_System.Models.ViewModels;

namespace AuctionManagement_System.IRepository
{
    public interface IAedRepository
    {
        public Task<User> Login(LoginViewModel model);
    }
}
