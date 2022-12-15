namespace BlImplementation;

internal class Product : BlApi.IProduct
{

    /// <summary>
    /// Product list request
    ///Request a list of products from the data layer
    /// Build a list of products of the ProductForList type(logical entity) based on the data
    ////Return the list that was built

    /// </summary>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlEmptyStringException"></exception>
    /// <exception cref="BO.BlWorngCategoryException"></exception>
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
    public IEnumerable<BO.ProductItem?> GetListedProductsForC()
    {
        return from DO.Product? doProduct in dal.Product.GetAll()
               select new BO.ProductItem
               {
                   ID = doProduct?.ID ?? throw new BO.BlMissingEntityException("Missing ID"),
                   ProductName = doProduct?.ProductName ?? throw new BO.BlEmptyStringException("missing name"),
                   Category = (BO.Category?)doProduct?.Category ?? throw new BO.BlWorngCategoryException("missing category"),
                   Price = doProduct?.Price ?? 0,
                   IsStock = doProduct?.InStock > 0 ? true : false
               };


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
            AmountInCart = cart.Items is null ? 0 : cart.Items.Where(x => x.ItemId == productID).Sum(x => x.Amount)

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
            if (product.ID < 100000 || product.ID > 999999 || product.ProductName.Length == 0 || product.Price < 0 || product.InStock < 0)//check if the details in property


                throw new BO.BlNullPropertyException("Missing detail in property");

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
            if (product.ID < 100000 || product.ID > 999999 || product.ProductName.Length == 0 || product.Price < 0 || product.InStock < 0)//check if the details in property

                throw new BO.BlNullPropertyException("Missing detail in property");

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
