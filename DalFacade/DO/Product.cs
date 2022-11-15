﻿
namespace DO;

public struct Product
{
    public int ID { get; set; }//the id of the product
    public string? ProductName{ get; set; }//the name of the product
    public double Price { get; set; }//the price of the product
    public Category Category { get; set; }//the category of the product
    public int InStock { get; set; }//the amount of the product in the store 

    public override string ToString() => $@"
 ID={ID}, 
ProductName ={ProductName},
 Price={Price},
Category={Category},
InStock={InStock}

";
}
