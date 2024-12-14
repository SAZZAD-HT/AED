using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class Iteam
    {
        [Key]
        public int ItemNo { get; set; }
        public string Description { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Length { get; set; }
        public bool IsActive { get; set; }
    }
}
