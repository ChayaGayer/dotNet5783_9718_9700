
namespace DO;

public struct OrderItem
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
    /// the price of the orderItem
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// the amount of the items
    /// </summary>
    public int Amount { get; set; }
     
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
