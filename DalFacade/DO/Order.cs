
namespace DO;

public struct Order
{/// <summary>
/// the order id
/// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the customer name
    /// </summary>
    public string? CustomerName { get; set; }
    /// <summary>
    /// the customer email 
    /// </summary>
    public string? CustomerEmail { get; set; }
    /// <summary>
    /// //the cusomer adress
    /// </summary>
    public string? CustomerAddress { get; set; }
    /// <summary>
    /// /the date of the order
    /// </summary>
    public DateTime? OrderDate { get; set; }
    /// <summary>
    /// the ship date
    /// </summary>
    public DateTime? ShipDate { get; set; }
    /// <summary>
    /// /the delivery date
    /// </summary>
    public DateTime? DeliveryDate { get; set; }
  public override string ToString()
    {
        return this.ToStringProperty();
    }

}
