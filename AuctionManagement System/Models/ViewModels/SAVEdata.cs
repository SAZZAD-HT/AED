using System.ComponentModel.DataAnnotations;

namespace AuctionManagement_System.Models.ViewModels
{
    public class SAVEdata
    {
       
            
            public string CID { get; set; }
            public string SId { get; set; }
            public string BId { get; set; }
            public string DyeCond { get; set; }
            public string OrderType { get; set; }
            public string TrackingNo { get; set; }
            public string Description { get; set; }
            public string OrderNo { get; set; }
            public string CusPo { get; set; }
           
            public List<DataSaveRow> DataSaveRows { get; set; }
        
        public class DataSaveRow
        {
           
          
            public string?ItemNo { get; set; }
            public string? ProductId { get; set; }
            public string? FinishType { get; set; }
            public string? OrderQty { get; set; }
            public string? ExpectedShipment { get; set; }
            public string? ChangedShipmentDate { get; set; }
            public string? FreeStock { get; set; }
            public string? seq { get; set; }

        }
    }
}
