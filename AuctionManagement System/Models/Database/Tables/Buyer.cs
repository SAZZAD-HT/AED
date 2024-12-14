using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class Buyer
    {
        [Key]   
        public int BId { get; set; }
        public string BuyerName { get; set; }
        public string BuyerCode { get; set; }
    }
}
