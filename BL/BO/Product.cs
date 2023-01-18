
namespace BO;

public class Product
{
    /// <summary>
    /// the id of the product
    /// </summary>
    public int ID { get; set; }
    /// <summary>
    /// the name of the product
    /// </summary>
    public string? ProductName { get; set; }
    /// <summary>
    /// the price of the product
    /// </summary>
    public double Price { get; set; }//
    /// <summary>
    /// the category of the product 
    /// </summary>
    public Category Category { get; set; }//
    /// <summary>
    /// the amount of the product in the store
    /// </summary>
    public int InStock { get; set; }
    /// <summary>
    /// the name for the picture
    /// </summary>
    public string? ImageRelativeName { get; set; }  
    public override string ToString()
    {
        return this.ToStringProperty();
    }

}
