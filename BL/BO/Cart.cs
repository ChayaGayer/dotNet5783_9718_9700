
namespace BO;

public class Cart
{
    public string CustomerName { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAdress { get; set; }
    public IEnumerable<OrderItem?> Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
