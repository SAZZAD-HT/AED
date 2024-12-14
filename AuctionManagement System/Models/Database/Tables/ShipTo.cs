using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class ShipTo
    {
        [Key]
        public int SId { get; set; }
        public string ShipToCode { get; set; }
        public string ShipToName { get; set; }
    }
}
