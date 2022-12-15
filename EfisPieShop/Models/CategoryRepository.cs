namespace EfisPieShop.Models
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly EfisPieShopDbContext _efisPieShopDbContext;

        //constructor
        public CategoryRepository(EfisPieShopDbContext efisPieShopDbContext)
        {
            _efisPieShopDbContext = efisPieShopDbContext;
        }


        public IEnumerable<Category> AllCategories
        {
            get
            {
                return _efisPieShopDbContext.Categories.OrderBy(p => p.CategoryName);
            }
        }
    }
}
