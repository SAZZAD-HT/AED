using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class Customer
    {
        [Key]
        public int CID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerCode { get; set; }
    }
}
