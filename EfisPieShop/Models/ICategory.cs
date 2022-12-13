namespace EfisPieShop.Models
{
    public interface ICategory
    {
        IEnumerable<Category> AllCategories { get; }
    }
}
