using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class DataSaveHeader
    {
        [Key]   
        public int DsID { get; set; }
        public int CID { get; set; }
        public int SId { get; set; }
        public int BId { get; set; }
        public string DyeCond { get; set; }
        public string OrderType { get; set; }
        public string TrackingNo { get; set; }
        public string Description { get; set; }
        public string OrderNo { get; set; }
        public string CusPo { get; set; }
        public bool IsActive { get; set; }
    }
}
