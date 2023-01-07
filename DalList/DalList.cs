using DalApi;
namespace Dal;
internal sealed class DalList : IDal
{
    private DalList() { }
    public IOrder Order => new DalOrder();
    public IProduct Product { get; }= new DalProduct();
    public IOrderItem OrderItem { get; } =new DalOrderItem();
    public IUser User { get; } = new DalUser();
    public static IDal Instance { get; } = new DalList();
}