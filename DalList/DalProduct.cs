using DO;
namespace Dal;

public class DalProduct
{
    public int Add(Product product)
    {
        if (DataSource.ProducstList.Exists(x => x?.ID == product.ID))
            throw new Exception("Product is exist");
        else
            DataSource.ProducstList.Add(product);
        return product.ID;
    }

    public Product GetById(int id)
    {
        Product product = new Product();
        product = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        return product;
    }
    public void Update(Product product)
    {
        Product productToUpdate = new Product();
        productToUpdate = DataSource.ProducstList.Find(x => x?.ID == product.ID) ?? throw new Exception("not found");
        DataSource.ProducstList.Remove(productToUpdate);
        DataSource.ProducstList.Add(product);
    }
    public void Delete(int id)
    {
        Product productToDel = new Product();
        productToDel = DataSource.ProducstList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        DataSource.ProducstList.Remove(productToDel);

    }
    public IEnumerable<Product?> GetAll()
{
        List<Product?> listP = new List<Product?>();
        for (int i = 0; i < DataSource.ProducstList.Count; i++)
    {
            Product? newProduct = new Product();
            newProduct = DataSource.ProducstList[i];
            listP.Add(newProduct);

    }
        return listP;
    }
}