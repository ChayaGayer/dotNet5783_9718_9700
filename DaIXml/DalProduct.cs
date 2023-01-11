using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalProduct : IProduct
{
    readonly string s_products = "products";
    public int Add(Product product)
    {
        List <DO.Product?> listprod=XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (listprod. Exists(x => x?.ID == product.ID))//checking if the product already here
            throw new DO.DalAlreadyExistIdException(product.ID, "product");
        else
            listprod.Add(product);//if not, add to the list
        XMLTools.SaveListToXMLSerializer(listprod, s_products); 
        return product.ID;
    }

    public void Delete(int id)
    {
        List<DO.Product?> listprod = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product productToDel = new Product();
        productToDel = listprod.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        listprod.Remove(productToDel);//if found remove
        XMLTools.SaveListToXMLSerializer(listprod, s_products);
    }

    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filtar = null)
    {
        List<DO.Product?> listprod = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        if (filtar != null)
            return listprod.Where(x => filtar(x));
        return listprod.Select(x => x);
    }

    public Product GetById(int id)
    {
        List<DO.Product?> listprod = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product product = new Product();//create a new product
        product = listprod.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        return product;
    }

    public void Update(Product product)
    {
        List<DO.Product?> listprod = XMLTools.LoadListFromXMLSerializer<DO.Product>(s_products);
        Product productToUpdate = new Product();//create a new product
        productToUpdate = listprod.Find(x => x?.ID == product.ID) ?? throw new DO.DalMissingIdException(product.ID, "product");//find the product with this id if not found throw exception
        listprod.Remove(productToUpdate);//if found remove it
        listprod.Add(product);//add the right one
        XMLTools.SaveListToXMLSerializer(listprod, s_products);
    }
}
