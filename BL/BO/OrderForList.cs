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
    public int ID { get; set; }
    public string CustomerName { get; set; }
    public OrderStatus Status { get; set; }
    public int Amount { get; set; }
    public double TotelPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
