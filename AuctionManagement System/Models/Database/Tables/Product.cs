using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string TKT { get; set; }
        public string Name { get; set; }
        public string Shade { get; set; }
        public string? FinishType { get; set; }
        public int? Stock { get; set; }
        public int? Reserve { get; set; }
        public string? Length { get; set; }
        public int Price { get; set; }
       
       
        public int? PrdStock { get; set; }
        public int? FreeStock { get; set; }
        
        public bool IsActive { get; set; }
    }
}
