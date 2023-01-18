
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
    /// <summary>
    /// get listed product,func with a filter
    /// </summary>
    /// <param name="filter"></param> return the list  by filter
    /// <returns></returns>
    IEnumerable<BO.ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null);
    /// <summary>
    /// get the product list for the customer
    /// </summary>
    /// <returns></returns>
    IEnumerable<BO.ProductItem?> GetListedProductsForC();
    /// <summary>
    /// return the most popular products
    /// </summary>
    /// <returns></returns>
    IEnumerable<ProductItem?> MostPopular();
    /// <summary>
    /// returm the product data by id for maneger
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    
    BO.Product RequestProductDetaForM(int productID);
    /// <summary>
    /// returm the product data by id for customer 
    /// </summary>
    /// <param name="productID"></param>
    /// <param name="cart"></param>
    /// <returns></returns>
    BO.ProductItem RequestProductDetaForC(int productID, BO.Cart cart);
    /// <summary>
    /// get a product and add it to the list
    /// </summary>
    /// <param name="product"></param>
    void AddProduct(BO.Product product);
    /// <summary>
    /// get id of product and delete it from the list
    /// </summary>
    /// <param name="productID"></param>
    void DeleteProduct(int  productID); 
    /// <summary>
    /// get a product and update the data
    /// </summary>
    /// <param name="product"></param>
    void UpdateProductData(BO.Product product);
    
}
