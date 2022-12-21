using Microsoft.EntityFrameworkCore;

namespace EfisPieShop.Models
{
    public class PieRepository : IPieRepository
    {
        private readonly EfisPieShopDbContext _efisPieShopDbContext;

        //constructor
        public PieRepository(EfisPieShopDbContext efisPieShopDbContext)
        {
            _efisPieShopDbContext = efisPieShopDbContext;
        }

        public IEnumerable<Pie> AllPies
        {
            get
            {
                return _efisPieShopDbContext.Pies.Include(c => c.Category);
            }
  
        }

        public IEnumerable<Pie> PiesOfTheWeek
        {
            get
            {
                return _efisPieShopDbContext.Pies.Include(c => c.Category).Where(p=> (bool)p.IsPieOfTheWeek);
            }

        }

        public Pie? GetPieById(int pieId)
        { 
             return _efisPieShopDbContext.Pies.FirstOrDefault(p=> p.PieId == pieId);

        }

        public IEnumerable<Pie> SearchPies(string searchQuery)
        {
            return _efisPieShopDbContext.Pies.Where(p => p.Name.Contains(searchQuery));
        }
    }
}
