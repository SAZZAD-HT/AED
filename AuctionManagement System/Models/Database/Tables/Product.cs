using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }
        public string TKT { get; set; }
        public string Shade { get; set; }
        public string FinishType { get; set; }
        public decimal Stock { get; set; }
        public decimal Reserve { get; set; }
        public decimal OrderQty { get; set; }
        public DateTime ExpectedShipment { get; set; }
        public DateTime ChangedExpectedDate { get; set; }
        public decimal PrdStock { get; set; }
        public decimal FreeStock { get; set; }
        public int Seq { get; set; }
        public bool IsActive { get; set; }
    }
}
