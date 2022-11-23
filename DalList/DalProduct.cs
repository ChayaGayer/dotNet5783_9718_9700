using DalApi;
using DO;
namespace Dal;

internal class DalProduct:IProduct
{
    public int Add(Product product)
    {
        if (DataSource.ProducstList.Exists(x => x?.ID == product.ID))//checking if the product already here
            throw new Exception("Product is exist");
        else
            DataSource.ProducstList.Add(product);//if not add to the list
        return product.ID;
    }

    public Product GetById(int id)
    {
        Product product = new Product();//create a new product
        product = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new Exception("not found");//find the product with this id if not found throw exception
        return product;
    }
    public void Update(Product product)
    {
        Product productToUpdate = new Product();//create a new product
        productToUpdate = DataSource.ProducstList.Find(x => x?.ID == product.ID) ?? throw new Exception("not found");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToUpdate);//if found remove it
        DataSource.ProducstList.Add(product);//add the right one
    }
    public void Delete(int id)
    {
        Product productToDel = new Product();
        productToDel = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new Exception("not found");//find the product with this id if not found throw exception
        DataSource.ProducstList.Remove(productToDel);//if found remove

    }
    public IEnumerable<Product?> GetAll()
{
        List<Product?> listP = new List<Product?>();//create a new list
        for (int i = 0; i < DataSource.ProducstList.Count; i++)//go over the list
    {
            Product? newProduct = new Product();//creat new product
            newProduct = DataSource.ProducstList[i];//copy the product from the list to the new list
            listP.Add(newProduct);//add 

    }
        return listP;
    }
}