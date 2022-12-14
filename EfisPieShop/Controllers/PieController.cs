using EfisPieShop.Models;
using EfisPieShop.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EfisPieShop.Controllers
{
    public class PieController : Controller
    {
        private readonly IPieRepository _pieRepository;
        private readonly ICategoryRepository _categoryRepository;


        // This is a constructor injection
        // Because we registered the IPieRepository and the ICategoryRepository as private fields
        // an instance of these that is of the MockPieRepository and the MockCategoryRepository will be injected
        // So will get instances after the constructor has run
        public PieController(IPieRepository pieRepository, ICategoryRepository categoryRepository)
        {
            _pieRepository = pieRepository;
            _categoryRepository = categoryRepository;
        }

        //Our first Action Method
        public IActionResult List()
        {
            //ViewBag.CurrentCategory = "Cheese cakes";
            //return View(_pieRepository.AllPies);  

            PieListViewModel piesListViewModel = new PieListViewModel(_pieRepository.AllPies, "Cheese cakes");
            return View(piesListViewModel);
        }
    }
}
