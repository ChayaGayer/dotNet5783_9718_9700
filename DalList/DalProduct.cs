using DalApi;
using DO;
using System;

namespace Dal;

internal class DalProduct: IProduct
{
    public int Add(Product product)
    {
        if (DataSource.ProducstList.Exists(x => x?.ID == product.ID))//checking if the product already here
            throw new DO.DalAlreadyExistIdException(product.ID,"product");
        else
            DataSource.ProducstList.Add(product);//if not add to the list
        return product.ID;
    } 

    public Product GetById(int id)
    {
        Product product = new Product();//create a new product
        product = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "product");//find the product with this id if not found throw exception
        return product;
    }
    public void Update(Product product)
    {
        Product productToUpdate = new Product();//create a new product
        productToUpdate = DataSource.ProducstList.Find(x => x?.ID == product.ID) ?? throw new DO.DalMissingIdException(product.ID, "product");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToUpdate);//if found remove it
        DataSource.ProducstList.Add(product);//add the right one
    }
    public void Delete(int id)
    {
        Product productToDel = new Product();
        productToDel = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id,"product");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToDel);//if found remove

    }
    public IEnumerable<Product?> GetAll(Func<Product?, bool>? filtar = null)
    {
        if (filtar != null)
            return DataSource.ProducstList.Where(x => filtar(x));
        return DataSource.ProducstList.Select(x => x);
    }
}