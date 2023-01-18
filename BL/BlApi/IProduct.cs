
using BO;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;
/// <summary>
/// the product interface
/// </summary>
public interface IProduct
{

    IEnumerable<BO.ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);
    
    IEnumerable<BO.ProductItem?> GetListedProductsForC(Func<BO.ProductItem?, bool>? filter = null);
    IEnumerable<ProductItem?> MostPopular();
    
    BO.Product RequestProductDetaForM(int productID);
    BO.ProductItem RequestProductDetaForC(int productID, BO.Cart cart);
    void AddProduct(BO.Product product);
    void DeleteProduct(int  productID); 
    void UpdateProductData(BO.Product product);
    
}
