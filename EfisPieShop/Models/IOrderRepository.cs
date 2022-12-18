namespace EfisPieShop.Models
{
    public interface IOrderRepository
    {
        void CreateOrder(Order order);
    }
}
