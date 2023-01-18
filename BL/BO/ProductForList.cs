

namespace BO;
/// <summary>
///  product for list
/// </summary>
public class ProductForList
{
    /// <summary>
    /// id
    /// </summary>
    public int ID { get; set; } 
    /// <summary>
    /// product name
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    /// price of the product
    /// </summary>
    public double Price { get; set; }
    /// <summary>
    /// category
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// the name for the picture
    /// </summary>
    public string? ImageRelativeName { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
