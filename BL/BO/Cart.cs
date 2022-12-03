
namespace BO;
/// <summary> 
/// a cart of shopping
/// </summary>
public class Cart
{
    public string CustomerName { get; set; }
    /// <summary>
    /// the coustomer name
    /// </summary>
    public string CustomerEmail { get; set; }
    /// <summary>
    /// the customer email
    /// </summary>
    public string CustomerAdress { get; set; }
    /// <summary>
    /// Customer Adress
    /// </summary>
    public IEnumerable<OrderItem?> Items { get; set; }
    /// <summary>
    /// a list of all the order items in the cart
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// a func that return the total price of the cart
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
