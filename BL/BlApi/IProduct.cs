using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IProduct
{
    IEnumerable<ProductItem?> GetProducts();
    IEnumerable<ProductForList?> GetListedProducts();
    BO.Product RequestProductDetalistForM(int productID);
    BO.Product RequestProductDetalistForC(int productID, BO.Cart cart);
    void AddProduct(BO.Product product);
    void DeleteProduct(BO.Product product); 
    void UpdateProductData(BO.Product product);
}
