using BlApi;
using BO;
namespace BlImplementation;

internal class Product:IProduct
{
    public IEnumerable<ProductForList> GetListedProducts()
    {
        throw new NotImplementedException();
    }
    public IEnumerable<ProductItem>GetProducts()
    {
        throw new NotImplementedException();
    }

    public BO.Product RequestProductDetalistForM(int productID)
    {
        throw new NotImplementedException();
    }
    public BO.Product RequestProductDetalistForC(int productID, BO.Cart cart)
    {
        throw new NotImplementedException();
    }
    public void AddProduct(BO.Product product)
    {
        throw new NotImplementedException();
    }
    public void DeleteProduct(BO.Product product)
    {
        throw new NotImplementedException();
    }
    public void UpdateProductData(BO.Product product)
    {
        throw new NotImplementedException();
    }
}
