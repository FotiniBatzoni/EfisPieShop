namespace EfisPieShop.Models
{
    public class OrderRepository
    {
        private readonly EfisPieShopDbContext _efisPieShopDbContext;
        private readonly IShoppingCart _shoppingCart;

        public OrderRepository(EfisPieShopDbContext efisPieShopDbContext, IShoppingCart shoppingCart)
        {
            _efisPieShopDbContext = efisPieShopDbContext;
            _shoppingCart = shoppingCart;
        }

        public void CreateOrder(Order order)
        {
            order.OrderPlaced = DateTime.Now;

            List<ShoppingCartItem>? shoppingCartItems = _shoppingCart.ShoppingCartItems;
            order.OrderTotal = _shoppingCart.GetShoppingCartTotal();

            order.OrderDetails = new List<OrderDetail>();

            foreach (ShoppingCartItem? shoppingCartItem in shoppingCartItems)
            {
                var orderDetail = new OrderDetail
                {
                    Amount = shoppingCartItem.Amount,
                    PieId = shoppingCartItem.Pie.PieId,
                    Price = shoppingCartItem.Pie.Price
                };

                order.OrderDetails.Add(orderDetail);
            }

            _efisPieShopDbContext.Orders.Add(order);

            _efisPieShopDbContext.SaveChanges();
        }
    }
}
