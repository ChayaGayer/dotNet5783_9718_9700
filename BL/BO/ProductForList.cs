

namespace BO;
/// <summary>
///  product for list
/// </summary>
public class ProductForList
{
    public int ID { get; set; }
    public string ProductName { get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
