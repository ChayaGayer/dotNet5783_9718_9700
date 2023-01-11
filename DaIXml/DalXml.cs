
using DalApi;
using System.Diagnostics;

namespace Dal;

sealed internal class DalXml: DalApi.IDal
{
    private DalXml() { }
    public IOrder Order => new DalOrder();
    public IProduct Product { get; } = new DalProduct();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
    public IUser User { get; } = new DalUser();
    public static IDal Instance { get; } = new DalXml();
}