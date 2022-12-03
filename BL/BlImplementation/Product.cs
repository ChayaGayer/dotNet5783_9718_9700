using BlApi;
using BO;
using DO;
using System.Diagnostics;
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
                   ID = doProduct?.ID ?? throw new BO.BlMissingEntityException("Missing ID"),
                   ProductName = doProduct?.ProductName ?? throw new BO.BlEmptyStringException("missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWorngCategoryException("missing category"),
                   Price = doProduct?.Price ?? 0
               };
        

    }
    public IEnumerable<BO.ProductItem?> GetListedProductsForC()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem
               {
                   ID = doProduct?.ID ?? throw new BO.BlMissingEntityException("Missing ID"),
                   ProductName = doProduct?.ProductName ?? throw new BO.BlEmptyStringException("missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWorngCategoryException("missing category"),
                   Price = doProduct?.Price ?? 0,
                   IsStock= doProduct?.InStock>0?true:false
               };


    }

    public BO.Product RequestProductDetaForM(int productID)
    {
         if (productID < 100000 || productID > 999999)
        {
            throw new BO.BlInCorrectException("Worng ID");
        }

        DO.Product doProduct = dal.Product.GetById(productID);
        try
        {
            doProduct = dal.Product.GetById(productID);

        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing product", ex);
        }

        return new BO.Product()
    {
        ID = doProduct.ID,
        Category = (BO.Category) doProduct.Category,
        Price = doProduct.Price,
        ProductName = doProduct.ProductName,
        InStock = doProduct.InStock,
    };

}
    public BO.ProductItem RequestProductDetaForC(int productID, BO.Cart cart)
    {
        
            if (productID < 100000 || productID > 999999)
                throw new BO.BlInCorrectException("Worng ID");
        
        DO.Product product;

            try
            {
                product = dal.Product.GetById(productID);
            }
            catch (DO.DalMissingIdException ex)
             {
                 throw new BO.BlMissingEntityException("Missing product", ex);
              }

        return new BO.ProductItem()///builiding a productItem
            {
                ID = product.ID,
                ProductName = product.ProductName,
                IsStock = product.InStock > 0 ? true : false,
                Category = (BO.Category)product.Category,
                Price = product.Price,
                AmountInCart = cart.Items is null ? 0 : cart.Items.Where(x => x.ItemId == productID).Sum(x => x.Amount)

            };
            //DO.Product doProduct; = dal.Product.GetById(productID);
            //    return new BO.ProductItem()
            //    {
            //        ID = doProduct.ID,
            //        Category = (BO.Category)doProduct.Category,
            //        Price = doProduct.Price,
            //       ProductName= doProduct.ProductName,

            //    };
        }
    
    public void AddProduct(BO.Product product)
    {
        
             
            if (product.ID < 100000 || product.ID > 999999||product.ProductName.Length == 0 || product.Price < 0 || product.InStock < 0)
            
                throw new BO.BlNullPropertyException("Missing detail in property");
        
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
        try
        {
            DO.OrderItem? Items = dal.OrderItem.GetAll().FirstOrDefault(item => item.Value.ID == productID);
            if (Items == null)
                throw new DO.DalMissingIdException(productID, "product");
            dal.Product.Delete(productID);
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.ToString());
        }
        


    }
    public void UpdateProductData(BO.Product product)
    {
        
            if (product.ID < 100000 || product.ID > 999999 || product.ProductName.Length == 0 || product.Price < 0 || product.InStock < 0)

                throw new BO.BlNullPropertyException("Missing detail in property");
      
        dal.Product.Update(new DO.Product()
        {
            ID = product.ID,
            ProductName = product.ProductName,
            Price = product.Price,
            InStock = product.InStock,
            Category = (DO.Category)product.Category,

        });
        return;
    }
}
