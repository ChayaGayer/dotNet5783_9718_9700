using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
   
    IEnumerable<ProductForList?> GetListedProducts();
    IEnumerable<ProductItem?> GetListedProductsForC();
    
    BO.Product RequestProductDetaForM(int productID);
    BO.ProductItem RequestProductDetaForC(int productID, BO.Cart cart);
    void AddProduct(BO.Product product);
    void DeleteProduct(int  productID); 
    void UpdateProductData(BO.Product product);
}
