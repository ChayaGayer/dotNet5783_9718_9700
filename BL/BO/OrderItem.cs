using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    /// <summary>
    /// the id of the order item 
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the order id
    /// </summary>
    public int OrderId { get; set; }
    /// <summary>
    /// the item id
    /// </summary>
    public int ItemId { get; set; }
    /// <summary>
    /// the item name
    /// </summary>
    public string ItemName { get; set; }
    /// <summary>
    /// the price of the item
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of the items
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// Total price
    /// </summary>
    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
