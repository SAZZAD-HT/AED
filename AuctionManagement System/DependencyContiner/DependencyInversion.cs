using AuctionManagement_System.Helper;

using AuctionManagement_System.Models.Database;

namespace AuctionManagement_System.DependencyContiner
{
    public class DependencyInversion
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //services.AddTransient<IAuctionService, AuctionService>();
            services.AddDbContext<AedDbContext>();
            services.AddScoped<EncryptionHelper>();
            //services.AddScoped<OTPHelper>();
        }
    }
}
