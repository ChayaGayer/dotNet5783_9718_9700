

namespace BO;

public class OrderTracking
{
    public int ID { get; set; }
    /// <summary>
    ///
    /// </summary>
    public OrderStatus Status { get; set; }
    /// <summary>
    /// 
    /// </summary>
    public List<Tuple<DateTime?,string>>? Tracking { get; set; }

    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
