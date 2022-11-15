
namespace DO;

public struct OrderItem
{

    public int ID { get; set; }//the id of the order item
    public int OrderId { get; set; }//the order id
    public int ItemId { get; set; }//the item id
    public double Price { get; set; }
    public int Amount { get; set; }//the amount of the items
    
    public override string ToString() => $@"
 ID={ID}, 
OrderId ={OrderId},
ItemId={ItemId},
 Price={Price},
Amount={Amount},

";
}
