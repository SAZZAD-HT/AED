//using AuctionManagement_System.IRepository;
//using AuctionManagement_System.Models.ViewModels;
//using Microsoft.AspNetCore.Mvc;
//using OfficeOpenXml.Drawing;
//using OfficeOpenXml;
//using System.Drawing.Imaging;
//using Microsoft.AspNetCore.Http;
//using System.Drawing;
//using System.IO;
//using AuctionManagement_System.Models.Database.Tables;
//using Microsoft.EntityFrameworkCore;
//using System.Drawing.Drawing2D;
//using AuctionManagement_System.Models.Database;
//using AuctionManagement_System.Helper;
//using static System.Runtime.InteropServices.JavaScript.JSType;
//using ClosedXML.Excel;
//using DocumentFormat.OpenXml.Office2010.Excel;
//using DocumentFormat.OpenXml.Wordprocessing;
//using DocumentFormat.OpenXml.Spreadsheet;
//using System.Numerics;

//namespace AuctionManagement_System.Controllers
//{
//    public class AuctionController : Controller
//    {
//        private readonly IAuctionService auct;
//        private readonly AedDbContext _db;
//        private readonly EncryptionHelper _en;
//        public AuctionController(IAuctionService auct, AedDbContext db, EncryptionHelper en)
//        {
//            this.auct = auct;
//            _db = db;
//            _en = en;
//        }
//        public IActionResult Index()
//        {
//            return View();
//        }
//        public IActionResult CheckIfLoggedIn()
//        {
//            var userId = HttpContext.Session.GetInt32("Eid");
//            if (userId == null)
//            {
//                return RedirectToAction("Login"); // Redirect to login if the session is not available
//            }
//            return null; // Continue if logged in
//        } 
//        public IActionResult CheckIfAdmin()
//        {
//            var role = HttpContext.Session.GetString("Role");
//            if (role != "Admin")
//            {
//                return RedirectToAction("Login"); // Redirect to login if the session is not available
//            }
//            return null;
//        }
//        [HttpGet]
//        public async Task<IActionResult> Login()
//        {
//            HttpContext.Session.Clear();
//            await Task.Yield(); // Ensures the method is truly asynchronous
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> Login([FromBody] LoginViewModel model)
//        {
//            try
//            {
                
//                HttpContext.Session.SetInt32("UID", 0);
//                if (ModelState.IsValid)
//                {
//                    var email = EncryptionHelper.Encrypt(model.Username.ToLower().Trim());
//                    var Password = EncryptionHelper.Encrypt(model.Password);
//                    var loginSuccess = await _db.Users.Where(x => x.Email == email && x.Password == Password).FirstOrDefaultAsync(); 
//                    var data = await _db.Employees.Where(x => x.Email.Trim().ToLower() == model.Username.Trim().ToLower()||x.EmployeeCode.ToLower()== model.Username.Trim().ToLower()).FirstOrDefaultAsync();
//                    if (loginSuccess != null)
//                    {
//                        if (loginSuccess.Role == "SuperAdmin")
//                        {
//                            HttpContext.Session.SetInt32("Eid", data.EId);
//                            HttpContext.Session.SetString("MobileNumber", loginSuccess.MobileNumber);
//                            HttpContext.Session.SetString("Role", "janina");
//                            HttpContext.Session.SetString("Email", email);
//                            HttpContext.Session.SetInt32("UID", loginSuccess.UserId);

//                            if (loginSuccess.ISFirstLogin != true) 
//                            { 

//                                return Json(new { redirectUrl = Url.Action("DownloadBidInformationExcel", "Auction") });
//                            }
//                            else
//                            {
//                                var otpCode = OTPHelper.GenerateOTP();
//                                OTPHelper otpHelper = new OTPHelper(_db);
//                                otpHelper.SendOTP(EncryptionHelper.Decrypt(loginSuccess.MobileNumber), otpCode, data.EId);
//                                return Json(new { redirectUrl = Url.Action("OTPConfirmPage", "Auction") });
//                            }
//                        }
//                        if (loginSuccess.Role == "Admin")
//                        {
//                            HttpContext.Session.SetInt32("Eid", data.EId);
//                            HttpContext.Session.SetString("MobileNumber", loginSuccess.MobileNumber);
//                            HttpContext.Session.SetString("Role", "Admin");
//                            HttpContext.Session.SetString("Email", email);
//                            HttpContext.Session.SetInt32("UID", loginSuccess.UserId);
//                            if (loginSuccess.ISFirstLogin != true)
//                            {
//                                return Json(new { redirectUrl = Url.Action("EmployeeList", "Auction") });
//                            }
//                            else
//                            {
//                                var mb = EncryptionHelper.Decrypt(loginSuccess.MobileNumber);
//                                var otpCode = OTPHelper.GenerateOTP();
//                                OTPHelper otpHelper = new OTPHelper(_db);
//                                otpHelper.SendOTP(mb, otpCode, data.EId);
//                                return Json(new { redirectUrl = Url.Action("OTPConfirmPage", "Auction") });
//                            }
//                        }

//                            return RedirectToAction("EmployeeInfoSave");
//                    }

//                    var bidded = await _db.Bids.Where(x => x.UserId == data.EId).FirstOrDefaultAsync();
//                    if (bidded != null)
//                    {
//                        return Json(new { redirectUrl = Url.Action("AlreadyBidded", "Auction") });
//                    }
//                    if (data != null && loginSuccess == null && bidded == null)
//                    {
//                        HttpContext.Session.SetString("UserId", data.EmployeeCode);
//                        HttpContext.Session.SetString("UserName", data.Name);
//                        HttpContext.Session.SetString("Mobile", model.Password);
//                        HttpContext.Session.SetInt32("Eid", data.EId);
//                        HttpContext.Session.SetString("Role", "GU");

//                        var otpCode = OTPHelper.GenerateOTP();
//                        OTPHelper otpHelper = new OTPHelper(_db);
//                        otpHelper.SendOTP(model.Password, otpCode, data.EId);
                      
//                        return Json(new { redirectUrl = Url.Action("OTPConfirmPage", "Auction") });
//                    }
//                    else
//                    {
//                        return BadRequest(new { message = "Invalid username or password" });
//                    }
//                    //return BadRequest(new { message = "Invalid username or password" });
//                }
//                else
//                {
//                    return BadRequest(new { message = "Please Insert All the Data " });
//                }


//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }
//        [HttpGet]
//        public  IActionResult TermsPage()
//        {
//            var redirectResult = CheckIfLoggedIn();

//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }
//            return View();
//        }
//        [HttpGet]
//        public IActionResult OTPConfirmPage()
//        {
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> OTPConfirmPage(string OTP)
//        {
//            var ddt = (int)HttpContext.Session.GetInt32("UID");
//            var userId = (int)HttpContext.Session.GetInt32("Eid");

//            if (ddt == 0)
//            {
//                var otpRecord = _db.OTPs
//                    .Where(o => o.UserId == userId && o.OTPCode == OTP.ToString() && o.IsActive == true)
//                    .FirstOrDefault();

//                if (otpRecord != null && otpRecord.ExpiryTime > DateTime.Now)
//                {
//                    var emp = _db.Employees.Where(x => x.EId == userId).FirstOrDefault();
//                    if (emp != null)
//                    {
//                        var encryptEmail = EncryptionHelper.Encrypt(emp.Email);
//                        var email = _db.Users.Where(x => x.Email == encryptEmail).FirstOrDefault();
//                        if (email == null)
//                        {
//                            var admin = new User
//                            {
//                                Name = emp.Name,
//                                Role = "GU",
//                                UserType = "GeneralUser",
//                                Email = EncryptionHelper.Encrypt(emp.Email.ToLower()),
//                                MobileNumber = HttpContext.Session.GetString("Mobile") != null
//                                               ? EncryptionHelper.Encrypt(HttpContext.Session.GetString("Mobile") ?? "")
//                                               : "N/A",
//                                Password = "NAI",
//                                IsActive = true,
//                                IsVerified = true,
//                                CreatedTime = DateTime.Now,
//                                ISFirstLogin = true,
//                                ActionBy = (int)userId
//                            };
//                            _db.Users.Add(admin);
//                            await _db.SaveChangesAsync();
//                        }

//                        var datas = await _db.OTPs.Where(x => x.UserId == userId).ToListAsync();
//                        foreach (var item in datas)
//                        {
//                            item.IsActive = false;
//                        }
//                        await _db.SaveChangesAsync();
//                    }
//                    return RedirectToAction("TermsPage");
//                }
//                else
//                {
//                    TempData["ErrorMessage"] = "OTP is Not Valid";
//                    return RedirectToAction("OTPConfirmPage");
//                }
//            }
//            else
//            {
//                var otpRecord = _db.OTPs
//                    .Where(o => o.UserId == userId && o.OTPCode == OTP.ToString() && o.IsActive == true)
//                    .FirstOrDefault();
//                var stat = HttpContext.Session.GetString("Role");

//                if (otpRecord != null && otpRecord.ExpiryTime > DateTime.Now)
//                {
//                    if (stat == "Admin")
//                    {
//                        return RedirectToAction("PasswordAndIdSet", "Admin");
//                    }
//                    else
//                    {
//                        return RedirectToAction("PasswordAndIdSet", "Admin");
//                    }
//                }
//                else
//                {
//                    TempData["ErrorMessage"] = "OTP is Not Valid";
//                    return RedirectToAction("OTPConfirmPage");
//                }
//            }
//        }
//        [HttpGet]
//        public IActionResult ExcelEmployeeUploadPage()
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            return View();
//        }
//        [HttpGet]
//        public IActionResult ProductCatalogue()
//        {
//            var redirectResult = CheckIfLoggedIn();
//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }
//            return View();
//        }
//        [HttpPost]
//        public ActionResult SubmttedData(int id)
//        {


//            return RedirectToAction("ProductCatalogueDetails", new { id });
//        }
//        [HttpGet]
//        public async Task<IActionResult> ProductCatalogueDetails(int productId)
//        {
//            var redirectResult = CheckIfLoggedIn();
//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }
//            // Fetch the item details using the productId
//            var item = await _db.ItemMasters.FindAsync(productId);
//            if (item == null)
//            {
//                return NotFound();
//            }

//            // Set ViewBag properties for product details
//            ViewBag.ItemId = item.ItemId;
//            ViewBag.ItemName = item.ItemName;
//            ViewBag.Brand = item.Brand;
//            ViewBag.Model = item.Model;
//            ViewBag.Colour = item.Colour;
//            ViewBag.Location = item.Location;
//            ViewBag.CC = item.CC;
//            ViewBag.Type = item.Type;
//            ViewBag.Pic1 = item.pic1 != null ? "data:image/jpeg;base64," + Convert.ToBase64String(item.pic1) : Convert.ToBase64String(item.pic1);
//            ViewBag.Pic2 = item.pic2 != null ? "data:image/jpeg;base64," + Convert.ToBase64String(item.pic2) : Convert.ToBase64String(item.pic2);
//            ViewBag.Pic3 = item.pic3 != null ? "data:image/jpeg;base64," + Convert.ToBase64String(item.pic3) : Convert.ToBase64String(item.pic3);
//            ViewBag.Pic4 = item.pic4 != null ? "data:image/jpeg;base64," + Convert.ToBase64String(item.pic4) : Convert.ToBase64String(item.pic4);
//            ViewBag.CarNuber = item.CarNmmber??" ";

//            // Employee information
//            //TempData["EmployeeId"] = 1; // For testing; adjust as necessary
//            int EmpId = (int)HttpContext.Session.GetInt32("Eid");
//            var data = await _db.Employees.Where(x => x.EId == EmpId).FirstOrDefaultAsync();
//            if (data != null)
//            {
//                ViewBag.Employee = data;
//            }

//            // Return the view with the item details
//            return View(item);
//        }
//        [HttpGet]
//        public IActionResult ProductCatalogueDetailsMD(int id)
//        {

//            return RedirectToAction("ProductCatalogueDetails", new { id = id });
//        }
//        [HttpGet]
//        public IActionResult EmployeeInfoSave()
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult; 
//            }
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> UploadExcelAsync(IFormFile employeeFile)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }

//            if (employeeFile != null && employeeFile.Length > 0)
//            {
//                using (var stream = new MemoryStream())
//                {
//                    employeeFile.CopyTo(stream);

//                    List<EmployeeViewModel> employees = new List<EmployeeViewModel>();

//                    // Set the license context
//                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//                    // Use EPPlus or ClosedXML to parse the Excel file
//                    using (ExcelPackage package = new ExcelPackage(stream))
//                    {
//                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
//                        int rowCount = worksheet.Dimension.Rows;

//                        for (int row = 2; row <= rowCount; row++)
//                        {
//                            var employee = new EmployeeViewModel
//                            {
//                                EmployeeCode = worksheet.Cells[row, 2].Text,
//                                SfID = worksheet.Cells[row, 3].Text,
//                                Name = worksheet.Cells[row, 4].Text,
//                                Desigination = worksheet.Cells[row, 5].Text,
//                                Department = worksheet.Cells[row, 6].Text,
//                                Location = worksheet.Cells[row, 7].Text,
//                                Grade = worksheet.Cells[row, 8].Text,
//                                Gender = worksheet.Cells[row, 9].Text,
//                                Email = worksheet.Cells[row, 10].Text,
//                                // MobileNumber = worksheet.Cells[row, 11].Text
//                            };
//                            employees.Add(employee);
//                        }
//                    }
//                    var data = await auct.SaveEmployee(employees);
//                    if (data == "Saved")
//                    {
//                        return Json(new { success = true, message = "Employee data saved successfully!" });
//                    }
//                    else
//                    {
//                        return Json(new { success = true, message = "Employee Not Saved saved successfully!" });
//                    }
//                }
//            }

//            return Json(new { success = false, message = "File upload failed" });
//        }
//        [HttpGet]
//        public IActionResult EmployeeList(string? employeeCode)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            return View();
//        }
//        [HttpGet]
//        public async Task<IActionResult> EmployeeListJSON(string? employeeCode)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            var employees = await auct.GetAllEmployee(employeeCode);
//            return Json(employees);
//        }
//        [HttpGet]
//        public async Task<IActionResult> GetAllItemsID(int ItemId)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            var data = await auct.GetAllItemsID(ItemId);
//            return Json(data);
//        }

//        [HttpPost]
//        public async Task<IActionResult> UploadExcelItem(IFormFile item)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            if (item != null && item.Length > 0)
//            {
//                using (var stream = new MemoryStream())
//                {
//                    item.CopyTo(stream);

//                    List<ItemUpload> items = new List<ItemUpload>();

//                    ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

//                    using (ExcelPackage package = new ExcelPackage(stream))
//                    {
//                        ExcelWorksheet worksheet = package.Workbook.Worksheets[0];
//                        int rowCount = worksheet.Dimension.Rows;

//                        for (int row = 2; row <= rowCount; row++)
//                        {
//                            var inf = new ItemUpload
//                            {
//                                ItemName = worksheet.Cells[row, 1].Text,
//                                Brand = worksheet.Cells[row, 2].Text,
//                                Model = worksheet.Cells[row, 3].Text,
//                                Colour = worksheet.Cells[row, 6].Text,
//                                CC = worksheet.Cells[row, 5].Text,
//                                Location = worksheet.Cells[row, 7].Text,
//                                Type = worksheet.Cells[row, 4].Text,
//                                pic1 = worksheet.Cells[row, 8].Text,
//                                CarNumber = worksheet.Cells[row, 8].Text,
//                                pic2 = worksheet.Cells[row, 9].Text,
//                                pic3 = worksheet.Cells[row, 10].Text
//                            };
//                            items.Add(inf);
//                        }
//                    }

//                    var result = await auct.SaveItems(items);

//                    if (result == "Saved")
//                    {
//                        return Json(new { success = true, message = "Item data saved successfully!", data = items });
//                    }
//                    else
//                    {
//                        return Json(new { success = false, message = "Items not saved." });
//                    }
//                }
//            }
//            return Json(new { success = false, message = "File upload failed." });
//        }
//        [HttpGet]
//        public IActionResult IteamSave()
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            return View();
//        }

//        [HttpGet]
//        public IActionResult ItemList()
//        {
//            var redirectResult = CheckIfLoggedIn();

//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }
//            return View();
//        }
//        [HttpGet]
//        public async Task<IActionResult> ItemListJSON(string? ItemName)
//        {
//            var redirectResult = CheckIfLoggedIn();

//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }

//            var items = await auct.GetAllItems(ItemName);
//            return Json(items);
//        }
//        [HttpGet]
//        public async Task<IActionResult> ImageById(int ItemId, string PicNumber)
//        {
//            try
//            {
//                var item = await _db.ItemMasters
//                    .Where(dd => dd.ItemId == ItemId)
//                    .Select(dd => new
//                    {
//                        Pic1 = dd.pic1,
//                        Pic2 = dd.pic2,
//                        Pic3 = dd.pic3,
//                        Pic4 = dd.pic4
//                    })
//                    .FirstOrDefaultAsync();

//                if (item == null)
//                {
//                    return Json("No Data Found");
//                }

//                string imageBase64;

//                if (PicNumber == "pic1" && item.Pic1 != null)
//                {
//                    imageBase64 = "data:image/jpeg;base64," + Convert.ToBase64String( item.Pic1);
//                }
//                else if (PicNumber == "pic2" && item.Pic2 != null)
//                {
//                    imageBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(item.Pic2);
//                }
//                else if (PicNumber == "data:image/jpeg;base64," + "pic3" && item.Pic3 != null)
//                {
//                    imageBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(item.Pic3);
//                }
//                else if (PicNumber == "pic4" && item.Pic4 != null)
//                {
//                    imageBase64 = "data:image/jpeg;base64," + Convert.ToBase64String(item.Pic4);
//                }
//                else
//                {
//                    imageBase64 = "No Data Found";
//                }
//                return Json(imageBase64);
//            }
//            catch (Exception ex)
//            {
//                return BadRequest(new { message = ex.Message });
//            }
//        }

//        [HttpGet]
//        public async Task<IActionResult> ItemListAdmin(string? ItemName)
//        {
//            var redirectResult = CheckIfLoggedIn();

//            if (redirectResult != null)
//            {
//                return redirectResult; // Redirect if the user is not logged in
//            }

//            var items = await auct.GetALLCARS(ItemName);
//            return Json(items);
//        }
//        [HttpGet]
//        public async Task<IActionResult> ProductConfirmPage2(int itemId, string bidAmount, string tinNumber, int UserID)
//        {
//            try
//            {
//                var redirectResult = CheckIfLoggedIn();
//                if (redirectResult != null)
//                {
//                    return redirectResult; // Redirect if the user is not logged in
//                }
//                var ItemMaster = await _db.ItemMasters.Where(x => x.ItemId == itemId).FirstOrDefaultAsync();
//                var Employee = await _db.Employees.Where(x => x.EId == (int)HttpContext.Session.GetInt32("Eid")).FirstOrDefaultAsync();
//                var USer = await _db.Users.Where(x => x.Email == EncryptionHelper.Encrypt(Employee.Email.ToLower())).FirstOrDefaultAsync();
//                if (ItemMaster != null && Employee != null)
//                {
//                    ViewBag.Employee = Employee;
//                    ViewBag.ItemMaster = ItemMaster;
//                    ViewBag.BidAmount = bidAmount;
//                    ViewBag.TinNumber = tinNumber;
//                    var mobile = HttpContext.Session.GetString("Mobile");
//                    OTPHelper otpHelper = new OTPHelper(_db);
//                    //otpHelper.ConfirmBidMessage(mobile, Employee.Name);
//                }
//                else
//                {
//                    return BadRequest(new { message = "Try Again!!" });
//                }
//            }
//            catch (Exception ex)
//            {

//            }
//            return View();
//        }
//        [HttpPost]

//        public async Task<IActionResult> SaveBid([FromBody] BidSaveModel d)
//        {
//            try
//            {
//                int userID = (int)HttpContext.Session.GetInt32("Eid");
//                var Employee = await _db.Employees.Where(x => x.EId == (int)HttpContext.Session.GetInt32("Eid")).FirstOrDefaultAsync();
//                var USer = await _db.Users.Where(x => x.Email == EncryptionHelper.Encrypt(Employee.Email.ToLower())).FirstOrDefaultAsync();
//                var data = auct.SaveBid(d, userID);
//                if (data.Result == "Saved")
//                {
//                    var mobile = HttpContext.Session.GetString("Mobile");
//                    OTPHelper otpHelper = new OTPHelper(_db);
//                    otpHelper.ConfirmBidMessage(mobile, Employee.Name);
//                    return Json(new { success = true, message = "Bid data saved successfully!" });
//                }
//                else
//                {
//                    return Json(new { success = false, message = "Bid not saved!" });
//                }
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }
//        [HttpGet]
//        public IActionResult BiddingConfrmPage()
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult AlreadyBidded()
//        {
//            return View();
//        }
//        [HttpGet]
//        public IActionResult DownloadBidInformationExcel()
//        {
//            var role = HttpContext.Session.GetString("Role");
//            if(role!= "janina") 
//            {
//                return RedirectToAction("Login");
//            }
//            else
//            {
//                return View();
//            }
               
           
//        }
//        [HttpGet]
//        public async Task<IActionResult> DownloadBidInformationExcelfile()
//        {

//            var role = HttpContext.Session.GetString("Role");
//            if (role != "janina")
//            {
//                return RedirectToAction("Login");
//            }
//            else
//            {
               
//            var data = await auct.SuperAdminExcelSaveModelALL();

//            if (data == null || !data.Any())
//            {
//                return Content("No data available to export.");
//            }

//                using (var workbook = new XLWorkbook())
//                {
//                    var worksheet = workbook.Worksheets.Add("Bid Information");

//                    // Create headers
//                    worksheet.Cell(1, 1).Value = "Bid ID";
//                    worksheet.Cell(1, 2).Value = "Bid Amount";
//                    worksheet.Cell(1, 3).Value = "TIN Number";
//                    worksheet.Cell(1, 4).Value = "Employee Code";
//                    worksheet.Cell(1, 5).Value = "SfID";
//                    worksheet.Cell(1, 6).Value = "Name";
//                    worksheet.Cell(1, 7).Value = "Designation";
//                    worksheet.Cell(1, 8).Value = "Department";
//                    worksheet.Cell(1, 9).Value = "Location";
//                    worksheet.Cell(1, 10).Value = "Grade";
//                    worksheet.Cell(1, 11).Value = "Gender";
//                    worksheet.Cell(1, 12).Value = "Mobile Number";
//                    worksheet.Cell(1, 13).Value = "Email";
//                    worksheet.Cell(1, 14).Value = "Item ID";
//                    worksheet.Cell(1, 15).Value = "Item Name";
//                    worksheet.Cell(1, 16).Value = "Brand";
//                    worksheet.Cell(1, 17).Value = "Model";
//                    worksheet.Cell(1, 18).Value = "Colour";
//                    worksheet.Cell(1, 19).Value = "Location (Item)";

//                    // Fill data
//                    for (int i = 0; i < data.Count; i++)
//                    {
//                        var row = i + 2;
//                        worksheet.Cell(row, 1).Value = data[i].BidId;
//                        worksheet.Cell(row, 2).Value = data[i].BidAmount;
//                        worksheet.Cell(row, 3).Value = data[i].TinNumber;
//                        worksheet.Cell(row, 4).Value = data[i].EmployeeCode;
//                        worksheet.Cell(row, 5).Value = data[i].SfID;
//                        worksheet.Cell(row, 6).Value = data[i].Name;
//                        worksheet.Cell(row, 7).Value = data[i].Desigination;
//                        worksheet.Cell(row, 8).Value = data[i].Department;
//                        worksheet.Cell(row, 9).Value = data[i].Location;
//                        worksheet.Cell(row, 10).Value = data[i].Grade;
//                        worksheet.Cell(row, 11).Value = data[i].Gender;
//                        worksheet.Cell(row, 12).Value = data[i].MobileNumber;
//                        worksheet.Cell(row, 13).Value = data[i].Email;
//                        worksheet.Cell(row, 14).Value = data[i].ItemId;
//                        worksheet.Cell(row, 15).Value = data[i].ItemName;
//                        worksheet.Cell(row, 16).Value = data[i].Brand;
//                        worksheet.Cell(row, 17).Value = data[i].Model;
//                        worksheet.Cell(row, 18).Value = data[i].Colour;
//                        worksheet.Cell(row, 19).Value = data[i].LocationItem;
//                    }

//                    // Save Excel file to memory stream
//                    using (var stream = new MemoryStream())
//                    {
//                        workbook.SaveAs(stream);
//                        stream.Seek(0, SeekOrigin.Begin);

//                        // Return the Excel file as a download
//                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "BidInformation.xlsx");
//                    }
//                }
//            }
//        }
//        [HttpGet]
//        public IActionResult Logout()
//        {
//            return RedirectToAction("Login");
//        }
//        [HttpPost]
//        public async Task<IActionResult> SaveImage([FromBody] ImageSave model)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            try
//            {
//                if (ModelState.IsValid)
//                {
//                    // Save the base64 string in the database
//                    var item = await _db.ItemMasters.FindAsync(model.ItemId);
//                    if (item != null)
//                    {
//                        if (item.pic1 != null && item.pic2 != null && item.pic3 != null && item.pic4!=null)
//                        {
//                            return BadRequest(new { success = false, message = "Only 3 Image can be uploaded" });
//                        }
//                        if (item.pic1 == null)
//                        {
//                            item.pic1= Convert.FromBase64String(model.Base64Image);
//                        }
//                        else if (item.pic2 == null)
//                        {
//                            item.pic2= Convert.FromBase64String(model.Base64Image);
//                        }
//                        else if(item.pic3 == null)
//                        {
//                            item.pic3 = Convert.FromBase64String(model.Base64Image);
//                        } else if(item.pic4 == null)
//                        {
//                            item.pic4 = Convert.FromBase64String(model.Base64Image);
//                        }
//                        await _db.SaveChangesAsync();
//                        return Ok(new { success = true });
//                    }
//                    return NotFound(new { success = false, message = "Item not found." });
//                }

//                return BadRequest(new { success = false, message = "Invalid data." });
//            }
//            catch(Exception ex)
//            {
//                return BadRequest(new { success = false, message = ex.Message });
//            }
//        }
//        [HttpGet]
//        public IActionResult DeleteBid()
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            return View();
//        }
//        [HttpPost]
//        public async Task<IActionResult> DeleteBid(string EmpCode)
//        {
//            var redirectResult = CheckIfAdmin();

//            if (redirectResult != null)
//            {
//                return redirectResult;
//            }
//            try
//            {
//                var emp =await _db.Employees.Where(x => x.EmployeeCode == EmpCode.Trim()).FirstOrDefaultAsync();
//                if (emp != null)
//                {
//                    var data = await _db.Bids.Where(x => x.UserId == emp.EId).FirstOrDefaultAsync();
//                    if (data != null)
//                    {
//                        data.IsActive = false;
//                        _db.Bids.Update(data);
//                        await _db.SaveChangesAsync();
//                        return Json(new { success = true, message = "Bid deleted successfully!" });
//                    }
//                    return Json(new { success = false, message = "Bid not found." });
//                }
               
//                return Json(new { success = false, message = "Employee not found." });
//            }
//            catch (Exception ex)
//            {
//                return Json(new { success = false, message = ex.Message });
//            }
//        }
//    }
//}