using AuctionManagement_System.Models.Database.Tables;
using DocumentFormat.OpenXml.Spreadsheet;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagement_System.Models.Database
{
    public class AedDbContext:DbContext
    {
        public AedDbContext(DbContextOptions<AedDbContext> options)
        : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<ShipTo> ShipTos { get; set; }
        public DbSet<Buyer> Buyers { get; set; }
        public DbSet<DataSaveHeader> DataSaveHeaders { get; set; }
        public DbSet<Iteam> ItemTables { get; set; }
        public DbSet<Product> ProductTables { get; set; }
        public DbSet<DataSaveRow> DataSaveRows { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
