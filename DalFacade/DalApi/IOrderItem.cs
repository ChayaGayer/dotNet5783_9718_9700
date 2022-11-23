using DO;

namespace DalApi;

public interface IOrderItem : ICrud<OrderItem> 
{
    IEnumerable<OrderItem?> GetOrderItems(int id);
   OrderItem GetTheItem(int orderId,int itemId);
}
