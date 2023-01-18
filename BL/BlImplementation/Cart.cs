
using BlApi;
using BO;
using DO;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = DalApi.Factory.Get();

    /// <summary>
    /// adding a product to the cart
    /// </summary>
    /// <param name="cart"></param>//get a cart from the logic 
    /// <param name="productId"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>//if this product not exist throw
    public BO.Cart AddProductForCart(BO.Cart cart, int productId)

    {
        DO.Product product = new DO.Product();
        try
        {
            product = dal.Product.GetById(productId);//check if exist
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing product", ex);
        }


        BO.OrderItem? orderItem = cart.Items.FirstOrDefault(x => x?.ItemId == productId);

        if (orderItem != null)//if the product alreadyin the cart
        {
            if (product.InStock > orderItem.Amount)//there is enough-update
            {
                orderItem.Amount += 1;
                orderItem.TotalPrice += product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }
            else//there isnt enough-throw
            {
                throw new BO.BlMissingEntityException("The amount of the product is lower then the amount in stock");
            }
        }

        else///if the item is not exist in the cart
        {
            if (cart.Items == null)//if there isnt such product
            {
                cart.Items = new List<BO.OrderItem>();//create
            }
            if (product.InStock > 0)//if in stock
            {
                BO.OrderItem? newOrderItem = new BO.OrderItem()//add to the cart
                {
                    ID = product.ID,
                    ItemName = product.ProductName,
                    //orderid
                    ItemId = product.ID,
                    Price = product.Price,
                    Amount = 1,
                    TotalPrice = product.Price,
                };

                cart.Items = cart.Items.Append(newOrderItem);
                cart.TotalPrice += newOrderItem.TotalPrice;//update
                return cart;
            }
            else
            {
                throw new BO.BlMissingEntityException("The product is out of stock");
            }

        }




    }
    /// <summary>
    /// update amount of product in cart
    /// </summary>
    /// <param name="cart"></param>//get a cart 
    /// <param name="productId"></param>//get the id of the product we want to update
    /// <param name="amount"></param>//the new amount
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount)
    {
        {
            try
            {
                if (productId < 0)// if id is inavalid
                {
                    throw new BO.BlInCorrectException("id is inavalid");
                }
                if (amount < 0)// if amount is inavalid
                {
                    throw new BO.BlNagtiveNumberException("Incorrect amount");
                }

                DO.Product? p = dal?.Product.GetById(productId);// getting the product
                BO.OrderItem? oi = (from item in cart.Items  // finding orsderItem
                                    where item.ItemId == productId
                                    select item).First();
                if (amount == 0)
                {
                    cart.Items = cart.Items.Where(x => x?.ItemId != oi.ID);//delete
                    cart.TotalPrice = cart.TotalPrice - oi.TotalPrice;


                    return cart;    //        return cart;

                   
                }
               
                else if (amount > oi.Amount)// increasing the amount
                {
                    if (p?.InStock >= amount)
                    {
                        cart.TotalPrice += oi.Price * (amount - oi.Amount);// updating the prices and the amount
                        oi.TotalPrice += oi.Price * (amount - oi.Amount);
                        oi.Amount = amount;
                    }
                    else// if wanting to increase the amount and there aren't enough products in stock
                    {
                        throw new BO.BlNagtiveNumberException("Out of Stock");
                    }
                }
                else if (amount < oi.Amount)// decreasing the amount
                {
                    cart.TotalPrice -= oi.Price * (oi.Amount - amount);// updating the prices and the amount
                    oi.TotalPrice -= oi.Price * (oi.Amount - amount);
                    oi.Amount = amount;
                }
                return cart;
            }
            catch (DO.DalMissingIdException exception)//if product doesn't exist
            {
                throw new BO.BlMissingEntityException("Product doesn't exist in cart", exception);
            }

        }
    
   
}
    /// <summary>
    /// confirmation of  order getting a cart and check propriety
    /// </summary>
    /// <param name="cart"></param>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlNagtiveNumberException"></exception>
    /// <exception cref="BO.BlEmptyStringException"></exception>
    public void OrderConfirmation(BO.Cart cart)
    {
        //check if items are exist
        IEnumerable<DO.Product> productIsExist;
        try
        {
            productIsExist = from item in cart.Items
                             select dal!.Product.GetById(item.ItemId);
        }
        catch (DO.DalMissingIdException ex)//if not exist-throw
        {
            throw new BO.BlMissingEntityException("Product not exist", ex);
        }


        //check if the amount is not negetive
        var isNegetive = cart.Items.Where(x => x?.Amount < 1);
        if (isNegetive.Any())
        {
            throw new BO.BlNagtiveNumberException("negative amount in order items");
        }
        int index;
        try
        {
            index = cart.Items.ToList().FindIndex(x => x?.Amount > productIsExist.First(y => y.ID == x?.ItemId).InStock);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("the product doesnt exist", ex);
        }
        if (index != -1)
        {
            throw new BO.BlMissingEntityException("there is not enough from the product");
        }

        //check if the name is empty
        if (cart.CustomerName == "")
            throw new BO.BlEmptyStringException("empty customer name");
        //if (!GetEmail(cart.CustomerEmail))
        //    throw new BO.BlEmptyStringException("empty customer email");


        DO.Order order = new DO.Order();//create a new order

        order.CustomerName = cart.CustomerName;
        order.CustomerAddress = cart.CustomerAdress;
        order.CustomerEmail = cart.CustomerEmail;
        order.OrderDate = DateTime.Now;
        order.ShipDate = null;
        order.DeliveryDate = null;

        int orderId = dal!.Order.Add(order);//try to add

        var addOrderItem = from BO.OrderItem item in cart.Items//build order items
                           select new DO.OrderItem()
                           {
                               ID = item.ID,
                               OrderId = orderId,
                               Price = item.Price,
                               Amount = item.Amount,
                           };
        addOrderItem.ToList().ForEach(x => dal.OrderItem.Add(x));//add
        IEnumerable<DO.Product> productUpdate = from item in cart.Items
                                                select new DO.Product
                                                {
                                                    ID = item.ID,
                                                    ProductName = item.ItemName,
                                                    Price = item.Price,
                                                    Category = dal.Product.GetById(item.ItemId).Category,
                                                    InStock = dal.Product.GetById(item.ItemId).InStock - item.Amount
                                                };



        productUpdate.ToList().ForEach(x => dal.Product.Update(x));

        //Checking the correctness of the values
        //if (cart.CustomerName == " ")
        //    throw new   BO.BlEmptyStringException("empty customer name");
        //if (cart.CustomerAdress== " ")
        //    throw new BO.BlEmptyStringException("empty customer adress");
        //if (!new EmailAddressAttribute().IsValid(cart.CustomerEmail))
        //    throw new BO.BlEmptyStringException("empty customer email");
        //if (cart.Items?.Count() == 0)
        //    throw new BO.BlNagtiveNumberException("There are no products in the cart");
        ////Create a new order
        //DO.Order newOrder = new DO.Order
        //{
        //    CustomerName = cart.CustomerName,
        //    CustomerAddress = cart.CustomerAdress,
        //    CustomerEmail = cart.CustomerEmail,
        //    OrderDate = DateTime.Now,
        //    ShipDate = null,
        //    DeliveryDate = null,
        //};
        //try
        //{
        //    //Adding the order to the list and receiving an ID number
        //    int orderID = dal.Order.Add(newOrder);

        //    //Adding the products in the cart to the order item list
        //    cart.Items.ForEach()(item =>
        //    dal?.OrderItem.Add(new DO.OrderItem
        //    {
        //        OrderID = orderID,
        //        ProductID = item.ProductID,
        //        Amount = item.Amount,
        //        Price = item.Price,
        //    }));

        //    //Temporary list of products after the new stock update
        //    var products = from BO.OrderItem item in cart.Items!
        //                   let updateProduct = dal.Product.GetById(item.ItemId)//?ID
        //                   select new DO.Product
        //                   {
        //                       ID = updateProduct.ID,
        //                       ProductName = updateProduct.ProductName,
        //                       Category = updateProduct.Category,
        //                       Price = updateProduct.Price,
        //                       InStock = updateProduct.InStock - item.Amount,

        //                   };

        //    //Updating the products in the product list
        //    products.ToList().ForEach(item => dal.Product.Update(item));
        //    //return orderID;
    }
        //catch (DO.DalMissingIdException ex)
        //{
        //    throw new BO.BlMissingEntityException(ex.Message);
        //}
    }
//    bool GetEmail(string email)//help func to check the email
//    {
//        return new EmailAddressAttribute().IsValid(email);
//    }
//}
//}
