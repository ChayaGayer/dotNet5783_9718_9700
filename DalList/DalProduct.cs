using DalApi;
using DO;
using System;

namespace Dal;

internal class DalProduct: IProduct
{
    /// <summary>
    /// Add
    /// </summary>
    /// <param name="product"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalAlreadyExistIdException"></exception>
    public int Add(Product product)
    {
        if (DataSource.ProducstList.Exists(x => x?.ID == product.ID))//checking if the product already here
            throw new DO.DalAlreadyExistIdException(product.ID,"product");
        else
            DataSource.ProducstList.Add(product);//if not, add to the list
        return product.ID;
    } 
    /// <summary>
    /// Get product by id
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public Product GetById(int id)
    {
        Product product = new Product();//create a new product
        product = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        return product;
    }
    /// <summary>
    /// update
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public void Update(Product product)
    {
        Product productToUpdate = new Product();//create a new product
        productToUpdate = DataSource.ProducstList.Find(x => x?.ID == product.ID) ?? throw new DO.DalMissingIdException(product.ID, "product");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToUpdate);//if found remove it
        DataSource.ProducstList.Add(product);//add the right one
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public void Delete(int id)
    {
        Product productToDel = new Product();
        productToDel = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id,"product");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToDel);//if found remove

    }
    /// <summary>
    /// Get all the 
    /// </summary>
    /// <param name="filtar"></param>
    /// <returns></returns>
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filtar = null)
    {
        if (filtar != null)
            return DataSource.ProducstList.Where(x => filtar(x));
        return DataSource.ProducstList.Select(x => x);
    }
}