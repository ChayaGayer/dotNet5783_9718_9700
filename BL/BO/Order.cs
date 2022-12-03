using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// order in the BO
/// </summary>
public class Order
{
    public int ID { get; set; }
    /// <summary>
    /// the order id
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the customer name
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// the customer email
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// the customer Address
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
   /// the date of the order
     /// </summary>
    public OrderStatus Status{ get; set; }
    /// <summary>
    /// the order status
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// the ship date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
    /// <summary>
    /// //the delivery date
    /// </summary>
    public IEnumerable<OrderItem?> Items { get; set; }
    /// <summary>
    /// a list of all the order items
    /// </summary>
    public double TotalPrice { get; set; }
    /// <summary>
    /// a func that return the total price of the order
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
