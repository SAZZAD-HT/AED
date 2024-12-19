using AuctionManagement_System.Helper;
using AuctionManagement_System.IRepository;
using AuctionManagement_System.Models.Database;
using AuctionManagement_System.Repository;

namespace AuctionManagement_System.DependencyContiner
{
    public class DependencyInversion
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddTransient<IAedRepository, AedRepository>();
            services.AddDbContext<AedDbContext>();
            services.AddScoped<EncryptionHelper>();
            //services.AddScoped<OTPHelper>();
        }
    }
}
