using EfisPieShop.Models;
using Microsoft.AspNetCore.Mvc;

namespace EfisPieShop.Controllers
{
    public class HomeController : Controller
    {
        //Dependency injection
        private readonly IPieRepository _pieRepository;


        // This is a constructor injection
        // Because we registered the IPieRepository and the ICategoryRepository as private fields
        // an instance of these that is of the MockPieRepository and the MockCategoryRepository will be injected
        // So will get instances after the constructor has run
        public HomeController(IPieRepository pieRepository)
        {
            _pieRepository = pieRepository;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
