using EfisPieShop.Models;

namespace EfisPieShop.ViewModels
{
    public class CategoriesViewModel
    {
        public IEnumerable<Category> Cats { get; set; }


        public CategoriesViewModel(IEnumerable<Category> cats)
        {
            Cats = cats;
        }
    }
}
