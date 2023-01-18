using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal;


internal class DalProduct : IProduct
{
   readonly string s_products = "products";
   /// <summary>
   /// Add product
   /// </summary>
   /// <param name="product"></param>
   /// <returns></returns>
   /// <exception cref="DO.DalAlreadyExistIdException"></exception>
    public int Add(Product product)
    {
        List<DO.Product?> listProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listProd.Exists(x => x?.ID == product.ID))//checking if the product already here
            throw new DO.DalAlreadyExistIdException(product.ID, "product");
        else
            listProd.Add(product);//if not, add to the list
        XMLTools.SaveListToXMLSerializer(listProd, s_products);
        return product.ID;
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public void Delete(int id)
    {
        List<DO.Product?> listProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product productToDel = new Product();
        productToDel = listProd.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        listProd.Remove(productToDel);//if found remove
        XMLTools.SaveListToXMLSerializer(listProd, s_products);
    }
    /// <summary>
    /// Get all the product with option to filter
    /// </summary>
    /// <param name="filtar"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filtar = null)
    {
        List<DO.Product?> listProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (filtar != null)
            return listProd.Where(x => filtar(x));
        return listProd.Select(x => x);
    }
    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public Product GetById(int id)
    {
        List<DO.Product?> listProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product product = new Product();//create a new product
        product = listProd.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        return product;
    }
    /// <summary>
    /// update a product details
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public void Update(Product product)
    {
        List<DO.Product?> listProd = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product productToUpdate = new Product();//create a new product
        productToUpdate = listProd.Find(x => x?.ID == product.ID) ?? throw new DO.DalMissingIdException(product.ID, "product");//find the product with this id if not found throw exception
        listProd.Remove(productToUpdate);//if found remove it
        listProd.Add(product);//add the right one
        XMLTools.SaveListToXMLSerializer(listProd, s_products);
    }
   
}
