
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
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


        BO.OrderItem? orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);

        if (orderItem != null)//if the product already in the cart
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
                    ItemId= product.ID,
                    Price = product.Price,
                    Amount = 1,
                    TotalPrice = product.Price,
                };

                cart.Items= cart.Items.Append(newOrderItem);
                cart.TotalPrice += newOrderItem.TotalPrice;//update
                return cart;
            }
            else
            {
                throw new BO.BlMissingEntityException("THE PRODUCT IS NOT IN THE CART");
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
        DO.Product product = new DO.Product();
        product = dal.Product.GetById(productId);//get the product

        var orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);
        if (orderItem != null)//there is such product
        {
            if (product.InStock < amount)//if the amount in stock smaller then the new amount->update
            {
                orderItem.Amount += amount;
                orderItem.TotalPrice = product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }

            if (product.InStock > amount) //if the amount in stock bigger then the new amount->update
            {
                orderItem.Amount = amount;
                orderItem.TotalPrice -= (amount - product.InStock) * product.Price;
                cart.TotalPrice -= (amount - product.InStock) * product.Price;
                return cart;
            }
            if (amount == 0)
            {

                cart.Items = cart.Items.Where(x => x.ItemId != product.ID);//delete
                return cart;

            }
            return cart;
        }
        else
        {
            throw new BO.BlMissingEntityException("THE PRODUCT IS NOT IN THE CART");
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
        try { 
            productIsExist = from item in cart.Items
                             select dal.Product.GetById(item.ID);
        }
        catch (DO.DalMissingIdException ex)//if not exist-throw
        {
            throw new BO.BlMissingEntityException("Product not exist", ex);
        }


        //check if the amount is not negetive
        var isNegetive = cart.Items.Where(x => x.Amount < 1);
        if (isNegetive.Any())
        {
            throw new BO.BlNagtiveNumberException("negative amount in order items");
        }
        int index;
        try
        {
             index = cart.Items.ToList().FindIndex(x => x.Amount > productIsExist.First(y => y.ID == x.ItemId).InStock);
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("the product doesnt exist",ex);
        }
        if(index!=-1)
        {
            throw new BO.BlMissingEntityException("there is not enough from the product");
        }
       
        //check if the name is empty
        if (cart.CustomerName == "")
            throw new BO.BlEmptyStringException("empty customer name");
        if (!GetEmail(cart.CustomerEmail))
            throw new BO.BlEmptyStringException("empty customer email");


        DO.Order order = new DO.Order();//create a new order

        order.CustomerName = cart.CustomerName;
        order.CustomerAddress = cart.CustomerAdress;
        order.CustomerEmail = cart.CustomerEmail;
        order.OrderDate = DateTime.Now;
        order.ShipDate = null;
        order.DeliveryDate = null;

        int orderId = dal.Order.Add(order);//try to add

        var addOrderItem = from BO.OrderItem item in cart.Items//build order items
                           select new DO.OrderItem()
                           {
                               ID = item.ID,
                               OrderId = orderId,
                               Price = item.Price,
                               Amount = item.Amount,
                           };
        addOrderItem.ToList().ForEach(x => dal.OrderItem.Add(x));//add
        addOrderItem.ToList().ForEach(x => { DO.Product p = dal.Product.GetById(x.ItemId); p.InStock -= x.Amount; dal.Product.Update(p); });//update


    }
    bool GetEmail(string email)//help func to check the email
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}
