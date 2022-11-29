
namespace DO;

public struct Order
{
    public int ID { get; set; }//the order id
    public string? CustomerName { get; set; }//the customer name
    public string? CustomerEmail { get; set; }//the customer email
    public string? CustomerAddress { get; set; }//the cusomer adress
    public DateTime? OrderDate { get; set; }//the date of the order
    public DateTime? ShipDate { get; set; }//the ship date
    public DateTime? DeliveryDate { get; set; }//the delivery date
  public override string ToString()
    {
        return this.ToStringProperty();
    }

}
