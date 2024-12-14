using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using AuctionManagement_System.Models.Database;
using AuctionManagement_System.DependencyContiner; // Assuming this is where ApplicationDbContext is defined

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Add DbContext with connection string
builder.Services.AddDbContext<AedDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("Development")));

// Register custom services (Dependency Injection)
DependencyInversion.RegisterServices(builder.Services);

// Add session services
builder.Services.AddDistributedMemoryCache(); // For in-memory session storage
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout (e.g., 30 minutes)
    options.Cookie.HttpOnly = true; // Keep the session cookie HTTP only
    options.Cookie.IsEssential = true; // Mark the cookie as essential
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Enable session middleware
app.UseSession(); // Must be before UseAuthorization

app.UseAuthorization();

// Map default controller route
app.MapControllerRoute(
     name: "default",
     pattern: "{controller=AED}/{action=Login}/{id?}");
   // pattern: "{controller=Auction}/{action=DownloadBidInformationExcel}");

app.Run();
