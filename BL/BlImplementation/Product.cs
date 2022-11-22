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
}
