

namespace BO;
/// <summary>
/// the Order Tracking
/// </summary>
public class OrderTracking 
{
    public int ID { get; set; }
    /// <summary>
    ///the id of the order
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// the status of the order
    /// </summary>
    
    
    public List<Tuple<DateTime?,string>>? Tracking { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
