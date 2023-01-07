using DO;

namespace DalApi;

public interface IDal
{
    IOrder Order { get; }
    IProduct Product { get; }

    IOrderItem OrderItem { get; }
    IUser User { get; }

}
