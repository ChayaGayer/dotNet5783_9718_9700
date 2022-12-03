
namespace BlApi;
/// <summary>
/// interface that contain :order,produt,cart
/// </summary>
public interface IBl
{
    public IOrder Order { get; internal set; }
    public IProduct Product { get; internal set; }
    public ICart Cart { get; internal set; }
}
