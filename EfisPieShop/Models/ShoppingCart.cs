using Microsoft.EntityFrameworkCore;

namespace EfisPieShop.Models
{
        public class ShoppingCart : IShoppingCart
        {
            private readonly EfisPieShopDbContext _efisPieShopDbContext;

            public string? ShoppingCartId { get; set; }

            public List<ShoppingCartItem> ShoppingCartItems { get; set; } = default!;

            private ShoppingCart(EfisPieShopDbContext efisPieShopDbContext)
            {
                _efisPieShopDbContext = efisPieShopDbContext;
            }

        //It checks if there is already a CartId available for the user
        //If not it will create a new GUID and restore that value as CartId
        //When the user is returning we will be able to find the existing CartId an it will use that

        //Session is available through Dependency Injection system GetCart(IServiceProvider services)
        public static ShoppingCart GetCart(IServiceProvider services)
            {
                //Try to get access to the Session
                ISession? session = services.GetRequiredService<IHttpContextAccessor>()?.HttpContext?.Session;

                //Try to get access to the DbContext
                EfisPieShopDbContext context = services.GetService<EfisPieShopDbContext>() ?? throw new Exception("Error initializing");

                //We check based on the session if there is a CardId for the incoming user - if not we create new GUID
                string cartId = session?.GetString("CartId") ?? Guid.NewGuid().ToString();

                //We set the value of CartId
                session?.SetString("CartId", cartId);

                //We return the ShoppingCart paasing in the DBContext as the CartId
                return new ShoppingCart(context) { ShoppingCartId = cartId };
            }

            public void AddToCart(Pie pie)
            {
                //First we check if there is a PieId in shoppingCartItem
                var shoppingCartItem =
                        _efisPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                            s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

                if (shoppingCartItem == null)
                {
                    shoppingCartItem = new ShoppingCartItem
                    {
                        ShoppingCartId = ShoppingCartId,
                        Pie = pie,
                        Amount = 1
                    };

                    _efisPieShopDbContext.ShoppingCartItems.Add(shoppingCartItem);
                }
                else
                {
                    shoppingCartItem.Amount++;
                }
                _efisPieShopDbContext.SaveChanges();
            }

            public int RemoveFromCart(Pie pie)
            {
                var shoppingCartItem =
                        _efisPieShopDbContext.ShoppingCartItems.SingleOrDefault(
                            s => s.Pie.PieId == pie.PieId && s.ShoppingCartId == ShoppingCartId);

                var localAmount = 0;

                if (shoppingCartItem != null)
                {
                    if (shoppingCartItem.Amount > 1)
                    {
                        shoppingCartItem.Amount--;
                        localAmount = shoppingCartItem.Amount;
                    }
                    else
                    {
                        _efisPieShopDbContext.ShoppingCartItems.Remove(shoppingCartItem);
                    }
                }

                _efisPieShopDbContext.SaveChanges();

                return localAmount;
            }

            public List<ShoppingCartItem> GetShoppingCartItems()
            {
                return ShoppingCartItems ??=
                           _efisPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                               .Include(s => s.Pie)
                               .ToList();
            }

            public void ClearCart()
            {
                var cartItems = _efisPieShopDbContext
                    .ShoppingCartItems
                    .Where(cart => cart.ShoppingCartId == ShoppingCartId);

                _efisPieShopDbContext.ShoppingCartItems.RemoveRange(cartItems);

                _efisPieShopDbContext.SaveChanges();
            }

            public decimal GetShoppingCartTotal()
            {
                var total = _efisPieShopDbContext.ShoppingCartItems.Where(c => c.ShoppingCartId == ShoppingCartId)
                    .Select(c => c.Pie.Price * c.Amount).Sum();
                return total;
            }
        }

}
