using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal sealed class DalXml : IDal
{
    private DalXml() { }
    public IOrder Order => new DalOrder();
    public IProduct Product { get; } = new DalProduct();
    public IOrderItem OrderItem { get; } = new DalOrderItem();
    public IUser User { get; } = new DalUser();
    public static IDal Instance { get; } = new DalXml();
}
