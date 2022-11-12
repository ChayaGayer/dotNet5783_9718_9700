
namespace DO;

public struct Product
{
    public int ID { get; set; }
    public string? ProductName{ get; set; }
    public double Price { get; set; }
    public Category Category { get; set; }
    public int InStock { get; set; }

    public override string ToString() => $@"
 ID={ID}, 
ProductName ={ProductName},
 Price={Price},
Category={Category},
InStock={InStock}

";
}
