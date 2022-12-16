using EfisPieShop.Models;

namespace EfisPieShop.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Pie> PiesOfWeek { get; }

        public HomeViewModel(IEnumerable<Pie> piesOfTheWeek)
        {
            PiesOfWeek = piesOfTheWeek;
        }
    }
}
