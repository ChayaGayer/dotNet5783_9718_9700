using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class Order
{
    public int ID { get; set; }
    public string? CustomerName { get; set; }
    public string? CustomerEmail { get; set; }//the customer email
    public string? CustomerAddress { get; set; }//the cusomer adress
    public DateTime? OrderDate { get; set; }//the date of the order
    public  OrderStatus Status{ get; set; }
    public DateTime? ShipDate { get; set; }//the ship date
    public DateTime? DeliveryDate { get; set; }//the delivery date
    public IEnumerable<OrderItem?> Items { get; set; }
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
