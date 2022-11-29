using BlApi;
using BO;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace BlImplementation;

internal class Product:IProduct
{
    DalApi.IDal dal=new Dal.DalList();
    
    public IEnumerable<BO.ProductForList?> GetListedProducts()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("missing id"),
                   ProductName = doProduct?.ProductName ?? throw new NullReferenceException("missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("missing category"),
                   Price = doProduct?.Price ?? 0
               };


    }
    public IEnumerable<BO.ProductItem?> GetListedProductsForC()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem
               {
                   ID = doProduct?.ID ?? throw new NullReferenceException("missing id"),
                   ProductName = doProduct?.ProductName ?? throw new NullReferenceException("missing Name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new NullReferenceException("missing category"),
                   Price = doProduct?.Price ?? 0,
                   IsStock= doProduct?.InStock>0?true:false
               };


    }

    public BO.Product RequestProductDetaForM(int productID)
    {

        DO.Product doProduct = dal.Product.GetById(productID);
        return new BO.Product()
        {
            ID = doProduct.ID,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            ProductName = doProduct.ProductName,
            InStock = doProduct.InStock,
        };

    }
    public BO.ProductItem RequestProductDetaForC(int productID)
    {

        DO.Product doProduct = dal.Product.GetById(productID);
        return new BO.ProductItem()
        {
            ID = doProduct.ID,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
           ProductName= doProduct.ProductName,
          
        };
    }
    public void AddProduct(BO.Product product)
    {
        try
        {
            if(product.ID<0||product.ProductName.Length==0||product.Price<0||product.InStock<0)
            {
                throw new NotImplementedException();
            }
        }
        catch { throw new NotImplementedException(); }
        dal.Product.Add(new DO.Product()
        {
ID=product.ID,
ProductName=product.ProductName,
Price=product.Price,
InStock=product.InStock,
Category=(DO.Category)product.Category,

        });
    }
    public void DeleteProduct(int productID)
    {
        IEnumerable<DO.Order?> orders = dal.Order.GetAll();
        var order = from item in orders
                    where dal.OrderItem.GetById(item.Value.ID).ItemId == productID 
                    select item;
        if(orders.Any())
        {
            
                dal.Product.Delete(productID);
            
        }
       



    }
    public void UpdateProductData(BO.Product product)
    {
        try
        {
            if (product.ID < 0 || product.ProductName.Length == 0 || product.Price < 0 || product.InStock < 0)
            {
                throw new NotImplementedException();
            }
        }
        catch { throw new NotImplementedException(); }
        dal.Product.Update(new DO.Product()
        {
            ID = product.ID,
            ProductName = product.ProductName,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category,

        });
    }
}
