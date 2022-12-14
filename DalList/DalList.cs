using DalApi;
namespace Dal;
 internal sealed class DalList: IDal
{
    private DalList() { }
    public IOrder Order => new DalOrder();
    public IProduct Product => new DalProduct();
    public IOrderItem OrderItem => new DalOrderItem();
    public static IDal Instance { get; } = new DalList();
}