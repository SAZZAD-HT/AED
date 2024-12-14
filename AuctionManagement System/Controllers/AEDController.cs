using AuctionManagement_System.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace AuctionManagement_System.Controllers
{
    public class AEDController : Controller
    {
        private readonly IAedRepository _aedRepository;

        public AEDController(IAedRepository aedRepository)
        {
            _aedRepository = aedRepository;
        }
        public IActionResult Login()
        {
            return View();
        }
    }
}
