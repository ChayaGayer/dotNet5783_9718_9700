using BO;
using System.Collections.Generic;

namespace BlImplementation;

internal class Product : BlApi.IProduct
{

    
    DalApi.IDal dal = DalApi.Factory.Get();

    public IEnumerable<BO.ProductForList?> GetListedProducts(Func<BO.ProductForList?, bool>? filter = null)
    {
        IEnumerable<BO.ProductForList?> list;
        list = from DO.Product doProduct in dal.Product.GetAll()
               select new BO.ProductForList
               {
                   ID = doProduct.ID, //?? throw new BO.BlMissingEntityException("Missing ID"),
                   ProductName = doProduct.ProductName!,// ?? throw new BO.BlEmptyStringException("missing name"),
                   Category = (BO.Category)doProduct.Category,// ?? throw new BO.BlWorngCategoryException("missing category"),
                   Price = doProduct.Price, //?? 0
                   ImageRelativeName= @"\Pics\"+doProduct.ID+".png"
               };
        return filter is null ? list : list.Where(filter);
        

    }
   
    /// <summary>
    /// return the list of product for the customer
    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlEmptyStringException"></exception>
    /// <exception cref="BO.BlWorngCategoryException"></exception>
    public IEnumerable<BO.ProductItem?> GetListedProductsForC(Func<BO.ProductItem?, bool>? filter = null)
    {
        IEnumerable<BO.ProductItem?> list;

        list= from DO.Product? doProduct in dal.Product.GetAll()
              select new BO.ProductItem
              {
                   ID = doProduct?.ID ?? throw new BO.BlMissingEntityException("Missing ID"),
                   ProductName = doProduct?.ProductName ?? throw new BO.BlEmptyStringException("missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWorngCategoryException("missing category"),
                   Price = doProduct?.Price ?? 0,
                   IsStock = doProduct?.InStock > 0 ? true : false,
                   ImageRelativeName = @"\Pics\" + doProduct?.ID + ".png"

               };
        return filter is null ? list : list.Where(filter);

    }
    private bool checkIfstock(int inStock)
    {
        if (inStock <= 0)
            return false;
        return true;
    }
    private int AmountCart(BO.Cart cart, int id)
    {
        if (cart == null)
            return 0;
        var productItem = cart.Items?.FirstOrDefault(x => x?.ItemId == id);
        return productItem!=null ? productItem.Amount:0;
    }
    public IEnumerable <BO.ProductItem?> MostPopular()
    {
        var productL = from item in dal.OrderItem.GetAll()
                       group item by item?.ItemId into gPopular
                       select new { id = gPopular.Key, Items = gPopular };
        productL=productL.OrderByDescending(x=>x.Items.Count()).Take(10);
        try
        {
            return from item in productL
                   let p = dal.Product.GetById(item?.id ?? throw new BlMissingEntityException("Prouduct doesnt exist"))
                   select new BO.ProductItem
                   {
                       ID = p.ID,
                       ProductName = p.ProductName,
                       Category = (BO.Category)p.Category,
                       //AmountInCart =AmountCart(cart,p.ID),
                       IsStock = checkIfstock(p.InStock),
                       ImageRelativeName = @"\Pics\" + p.ID + ".png",
                       Price = p.Price
                   };
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException(ex.Message);
        }
    }
    /// <summary>
    /// get the product id and return this product for maneger
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public BO.Product RequestProductDetaForM(int productID)
    {
        if (productID < 100000 || productID > 999999)//if the id correct
        {
            throw new BO.BlInCorrectException("Worng ID");
        }

        DO.Product doProduct = dal.Product.GetById(productID);
        try
        {
            doProduct = dal.Product.GetById(productID);//if exist

        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing product", ex);
        }

        return new BO.Product()//build and return the product
        {
            ID = doProduct.ID,
            Category = (BO.Category)doProduct.Category,
            Price = doProduct.Price,
            ProductName = doProduct.ProductName,
            InStock = doProduct.InStock,
            ImageRelativeName = @"\Pics\" + doProduct.ID + ".png"
        };

    }
    /// <summary>
    /// get the product id and return this product for costumer
    /// </summary>
    /// <param name="productID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlInCorrectException"></exception>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public BO.ProductItem RequestProductDetaForC(int productID, BO.Cart cart)
    {

        if (productID < 100000 || productID > 999999)//if the id correct
            throw new BO.BlInCorrectException("Worng ID");

        DO.Product product;

        try
        {
            product = dal.Product.GetById(productID);//if exist
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
            AmountInCart = cart.Items is null ? 0 : cart.Items.Where(x => x.ItemId == productID).Sum(x => x.Amount),
            ImageRelativeName = @"\Pics\" + product.ID + ".png"

        };

    }
    /// <summary>
    /// add a product gets a product and add it to the dal
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public void AddProduct(BO.Product product)
    {
        try
        {
            if (product.ID < 100000 || product.ID > 999999 ) //check if the details in property

               
                throw new BO.BlNullPropertyException("Wrong ID");
            if (product.ProductName.Length == 0)
                throw new BO.BlEmptyStringException("Missing Name");
            if (product.Price < 0)
                throw new BO.BlNagtiveNumberException("Negative Number");
                if(product.InStock < 0)
                throw new BO.BlNagtiveNumberException("Negative Number");
            dal?.Product.Add(new DO.Product()//adding to the dal
            {
                ID = product.ID,
                ProductName = product.ProductName,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Category)product.Category,


            });
        }
        catch (DO.DalAlreadyExistIdException e)
        {
            throw new BO.BlAlreadyExistEntityException("ID allready Exist", e);
        }
    }
  
        /// <summary>
        /// getting product id and delete this product
        /// </summary>
        /// <param name="productID"></param>
        /// <exception cref="BO.BlMissingEntityException"></exception>
        public void DeleteProduct(int productID)

        {
            try
            {
                DO.OrderItem? Items = dal.OrderItem.GetAll().FirstOrDefault(item => item?.ID == productID);
                if (Items == null)//not exist 
                    throw new DO.DalMissingIdException(productID, "product");
                dal.Product.Delete(productID);
            }
            catch (DO.DalMissingIdException ex)
            {
                throw new BO.BlMissingEntityException(ex.ToString());
            }



        }
    /// <summary>
    /// get a product to update 
    /// </summary>
    /// <param name="product"></param>
    /// <exception cref="BO.BlNullPropertyException"></exception>
    public void UpdateProductData(BO.Product product)
    {
        try {
             if (product.ID < 100000 || product.ID > 999999 ) //check if the details in property

               
                throw new BO.BlNullPropertyException("Wrong ID");
            if (product.ProductName.Length == 0)
                throw new BO.BlEmptyStringException("Missing Name");
            if (product.Price < 0)
                throw new BO.BlNagtiveNumberException("Negative Number");
                if(product.InStock < 0)
                throw new BO.BlNagtiveNumberException("Negative Number");
            dal.Product.Update(new DO.Product()//update
            {
                ID = product.ID,
                ProductName = product.ProductName,
                Price = product.Price,
                InStock = product.InStock,
                Category = (DO.Category)product.Category,

            });
            return;
        }
        catch(DO.DalMissingIdException e)
        {
            throw new BO.BlMissingEntityException("Missing");
        }
       
        }
    }
