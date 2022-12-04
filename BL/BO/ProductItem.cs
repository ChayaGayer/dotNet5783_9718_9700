
namespace BO;
/// <summary>
///  the product item
/// </summary>
public class ProductItem
{/// <summary>
/// theID 
/// </summary>
    public int ID { get; set; }
    /// <summary>
    ///the  Product Name
    /// </summary>
    public string ProductName { get; set; }
    /// <summary>
    /// the price
    /// </summary>
     public double Price { get; set; } 
    /// <summary>
    /// the catefory
    /// </summary>
    public Category Category { get; set; }
    /// <summary>
    /// a func that check if the product in stock
    /// </summary>
    public bool IsStock { get; set; }   
/// <summary>
/// return the amount of this product in cart
/// </summary>
     public int AmountInCart { get; set; }
    public override string ToString()
    {
        return this.ToStringProperty();
    }
}
