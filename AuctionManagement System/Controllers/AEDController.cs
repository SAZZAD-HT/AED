using AuctionManagement_System.Helper;
using AuctionManagement_System.IRepository;
using AuctionManagement_System.Models.Database;
using AuctionManagement_System.Models.Database.Tables;
using AuctionManagement_System.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuctionManagement_System.Controllers
{
    public class AEDController : Controller
    {
        private readonly IAedRepository _aedRepository;
        private readonly AedDbContext _db;
        public AEDController(IAedRepository aedRepository,AedDbContext bb)
        {
            _aedRepository = aedRepository;
            _db = bb;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
        {
            try
            {

               
                if (ModelState.IsValid)
                {
                    
                    model.Password = EncryptionHelper.Encrypt(model.Password);
                    var loginSuccess = await _aedRepository.Login(model);
                   
                    if ( loginSuccess != null)
                    {
                        HttpContext.Session.SetString("UseName", loginSuccess.Name);
                        return Json(new { redirectUrl = Url.Action("Entry", "AED") });
                    }
                    else
                    {
                        return BadRequest(new { message = "Invalid username or password" });
                    }
                   
                }
                else
                {
                    return BadRequest(new { message = "Please Insert All the Data " });
                }


            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
        [HttpGet]
        public IActionResult Entry()
        {
            return View();
        }
        [HttpPost]

        public IActionResult Entry([FromBody]  SAVEdata d)
        {
            DataSaveHeader dataSaveHeader = new DataSaveHeader();
            dataSaveHeader.CID = int.Parse(d.CID);
            dataSaveHeader.SId = int.Parse(d.SId);
            dataSaveHeader.BId = 1;//int.Parse(d.BId);           
            dataSaveHeader.DyeCond = d.DyeCond;
            dataSaveHeader.OrderType = d.OrderType;
            dataSaveHeader.TrackingNo = d.TrackingNo;
            dataSaveHeader.Description = d.Description??"";
            dataSaveHeader.OrderNo = d.OrderNo;
            dataSaveHeader.CusPo = d.CusPo??"";
            dataSaveHeader.IsActive = true;
            _db.DataSaveHeaders.Add(dataSaveHeader);
            _db.SaveChanges();
            foreach (var item in d.DataSaveRows)
            {
                DataSaveRow dataSaveRow = new DataSaveRow();
                dataSaveRow.DsHeaderId = dataSaveHeader.DsID;
                dataSaveRow.ItemNo = 1;// item.ItemNo;
                dataSaveRow.ProductId = 2;//item.ProductId;
                dataSaveRow.FinishType = item.FinishType;
                dataSaveRow.OrderQty = int.Parse(item.OrderQty);
                dataSaveRow.ChangedShipmentDate = string.IsNullOrEmpty(item.ChangedShipmentDate) ? (DateTime?)null : DateTime.Parse(item.ChangedShipmentDate);
                dataSaveRow.FreeStock = int.Parse(item.FreeStock);
                dataSaveRow.ChangedShipmentDate = null;
                dataSaveRow.FreeStock = int.Parse(item.FreeStock);
                dataSaveRow.seq = int.Parse(item.seq);
                dataSaveRow.IsActive = true;
                _db.DataSaveRows.Add(dataSaveRow);
                _db.SaveChanges();
            }
            return Json(new { redirectUrl = Url.Action("Entry", "AED") });  
        }
        [HttpGet]
        public async Task<IActionResult> GetCustomerDDL(string cus)
        {
            try
            {
                var data = await (from d in _db.Customers
                                  where cus == null || d.CustomerName.Contains(cus)
                                  select new
                                  {
                                      CustomerId = d.CID,
                                      CustomerCode = d.CustomerCode,
                                      CustomerName = d.CustomerName
                                  }).ToListAsync();
                return Json(data);

            }
            catch(Exception ex)
            {
                return null;

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetShipCustomerDDL(int CID)
        {
            try
            {
                var data = await (from s in _db.ShipTos
                                  where s.CID==CID
                                  select new
                                  {
                                      SId=s.SId,
                                      ShipToCode=s.ShipToCode,
                                      ShipToName=s.ShipToName

                                  }).ToListAsync();
                return Json(data);

            }
            catch (Exception ex)
            {
                return null;

            }
        }

        [HttpGet]
        public async Task<IActionResult> GetBuyerDDL(string buyer)
        {
            try
            {
                var data = await (from s in _db.Buyers
                                  where buyer==null || s.BuyerName.Contains(buyer)
                                  select new
                                  {
                                      BId=s.BId ,
                                      BuyerName=s.BuyerName ,
                                      BuyerCode=s.BuyerCode,


                                  }).ToListAsync();
                return Json(data);

            }
            catch (Exception ex)
            {
                return null;

            }
        }
        [HttpGet]
        public async Task<IActionResult> GetProductDDL()
        {
            try
            {
                var data = await (from s in _db.ProductTables
                select new
                {
                    ProductId = s.ProductId,
                    TKT = s.TKT,
                    Shade = s.Shade,
                    FinishType = s.FinishType,
                    Stock = s.Stock,                   
                    PrdStock = s.PrdStock,
                    FreeStock = s.FreeStock,
                    Length= s.Length,
                    Name=s.Name
                }).ToListAsync();
                return Json(data);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

    
    [HttpGet]
        public async Task<IActionResult> GetProductById(int id)
        {
            try
            {
                var data = await (from s in _db.ProductTables
                                  where s.ProductId==id
                                  select new
                                  {
                                      ProductId = s.ProductId,
                                      TKT = s.TKT,
                                      Shade = s.Shade,
                                      FinishType = s.FinishType,
                                      Stock = s.Stock,
                                      PrdStock = s.PrdStock,
                                      FreeStock = s.FreeStock,
                                      Length = s.Length,
                                      Price=s.Price
                                  }).FirstOrDefaultAsync();
                return Json(data);
            }
            catch (Exception ex)
            {
                return null;

            }
        }
    
        public async Task<IActionResult> GetItemById(int id)
        {
            try
            {
                var data = await (from s in _db.ItemTables
                                  where s.ItemNo == id
                                  select new
                                  {
                                      ItemNo = s.ItemNo,
                                      Description = s.Description,
                                      Name = s.Name,

                                  }).FirstOrDefaultAsync();
                return Json(data);
            }
            catch (Exception ex)
            {
                return null;

            }
        }

    }
}
