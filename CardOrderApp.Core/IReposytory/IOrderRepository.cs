using CardOrderApp.Core.Dtos.OrderDto;

namespace CardOrderApp.DAL.OrderRepository
{
    public interface IOrderRepository
    {
        int CreateOrder(OrderDto order);
        OrderDto GetById(int orderId);
        List<OrderDto> GetOrderByCustomerId(int customerId);
        bool UpdateOrder(OrderDto order);
    }
}