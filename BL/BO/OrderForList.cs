using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;
/// <summary>
/// order for list 
/// </summary>
public class OrderForList
{
    /// <summary>
    /// Id
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// Customer Name
    /// </summary>
    public string CustomerName { get; set; }
    /// <summary>
    /// the status of the order(deliverd/orderd/shiped)
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// Amount of the product in the order
    /// </summary>
    public int Amount { get; set; }
    /// <summary>
    /// the price of the order
    /// </summary>
    public double TotelPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
