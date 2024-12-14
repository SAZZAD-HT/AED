using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.Database.Tables
{
    public class DataSaveRow
    {
        [Key]
        public int DsRowId { get; set; }
        public int DsHeaderId { get; set; }
        public int ItemNo { get; set; }
        public int ProductId { get; set; }
        public string FinishType { get; set; }
        public Decimal OrderQty { get; set; }
        public DateTime ExpectedShipment { get; set; }
        public DateTime? ChangedShipmentDate { get; set; }
        public int FreeStock { get; set; }
        public int seq { get; set; }
        public bool IsActive { get; set; }

    }
}
