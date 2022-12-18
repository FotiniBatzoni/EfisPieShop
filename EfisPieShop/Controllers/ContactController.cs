using Microsoft.AspNetCore.Mvc;

namespace EfisPieShop.Controllers
{
    public class ContactController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
