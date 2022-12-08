
namespace BO;
/// <summary>
/// the status of the order
/// </summary>
public enum OrderStatus
{
    Initiated,
    Ordered,
    Paid, 
    Shipped,
    Delivered
}
/// <summary>
/// the categories
/// </summary>
 public enum Category
{
    Earrings,
    Braclet,
    Ring,
    Neckless,
    Watch,
    None//צריך לבדוק האם זה לא דופק את שלב שתיים
}
