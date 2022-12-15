using Microsoft.EntityFrameworkCore;

namespace EfisPieShop.Models
{
    public class EfisPieShopDbContext : DbContext
    {
        public EfisPieShopDbContext(DbContextOptions<EfisPieShopDbContext> options) : base(options)
        {
        }

        public DbSet<Category> Categories { get; set; }
        public DbSet<Pie> Pies { get; set; }
    }
}
