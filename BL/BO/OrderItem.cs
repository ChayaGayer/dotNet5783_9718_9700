using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO;

public class OrderItem
{
    public int ID { get; set; }//the id of the order item
    public int OrderId { get; set; }//the order id
    public int ItemId { get; set; }//the item id
    public string ItemName { get; set; }
    public double Price { get; set; }
    public int Amount { get; set; }//the amount of the items

    public double TotalPrice { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
