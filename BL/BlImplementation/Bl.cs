using BlApi;
using BO;

namespace BlImplementation;

public class Bl: IBl
{
    public Bl() { }
    public IOrder Order { get; set; } = new BO.Order();
    public IProduct Product { get; set; }= new BO.Product();
    public ICart Cart = {get; set;}=new Cart();

}
