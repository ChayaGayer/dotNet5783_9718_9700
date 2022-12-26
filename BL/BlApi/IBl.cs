
namespace BlApi;
/// <summary>
/// interface that contain :order,produt,cart
/// </summary>
public interface IBl
{
    public IOrder Order { get;}
    public IProduct Product { get;  }
    public ICart Cart { get;  }
}
