using BlApi;

namespace BlImplementation;

internal class Bl : IBl
{
    public Bl() { }
    public IOrder Order { get;  } = new Order();
    public IProduct Product { get;  }= new Product();
    public ICart Cart { get; } = new Cart();

}
