using AuctionManagement_System.IRepository;
using AuctionManagement_System.Models.Database;
using AuctionManagement_System.Models.Database.Tables;
using AuctionManagement_System.Models.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagement_System.Repository
{
    public class AedRepository : IAedRepository
    {
        private readonly  AedDbContext _db;


        public AedRepository(AedDbContext context)
        {
            _db = context;
        }

        public async Task<User> Login(LoginViewModel model)
        {
            try
            {
                var data = await _db.Users.Where(x => x.Email == model.Username && x.Password == model.Password).FirstOrDefaultAsync();
                if (data != null)
                {
                    
                    return data;
                }
                else
                {
                    return null;
                }

            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
