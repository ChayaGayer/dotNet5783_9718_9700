
using BlApi;
using BO;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;
using System.Transactions;
namespace BlImplementation;

internal class Cart : ICart
{
    DalApi.IDal dal = new Dal.DalList();
    public BO.Cart AddProductForCart(BO.Cart cart, int productId)
    {
        DO.Product product = new DO.Product();
        try
        {
            product = dal.Product.GetById(productId);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing product", ex);
        }


        BO.OrderItem? orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);

        if (orderItem != null)
        {
            if (product.InStock > orderItem.Amount)
            {
                orderItem.Amount += 1;
                orderItem.TotalPrice += product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }
          else
            {
                throw new BO.BlMissingEntityException("The amount of the product is lower then the amount in stock");
            }
        }

        else///if the item is not exist in the cart
        {
            if(cart.Items==null)
            {
                cart.Items = new List<BO.OrderItem>();
            }
            if (product.InStock > 0)
            {
                BO.OrderItem? newOrderItem = new BO.OrderItem()
                {
                    ID = product.ID,
                    ItemName = product.ProductName,
                    Price = product.Price,
                    Amount = 1,
                    TotalPrice = product.Price,
                };

                cart.Items = cart.Items.Append(newOrderItem);
                cart.TotalPrice += newOrderItem.TotalPrice;
                return cart;
            }
            else 
            {
                throw new BO.BlMissingEntityException("THE PRODUCT IS NOT IN THE CART");
            }
            
        }




    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount)
    {
        DO.Product product = new DO.Product();
        product = dal.Product.GetById(productId);

        var orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);
        if (orderItem != null)
        {
            if (product.InStock <amount)
            {
                orderItem.Amount += amount;
                orderItem.TotalPrice = product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }

            if (product.InStock > amount)
            {
                orderItem.Amount = amount;
                orderItem.TotalPrice -= (amount - product.InStock) * product.Price;
                cart.TotalPrice -= (amount - product.InStock) * product.Price;
                return cart;
            }
            if (amount == 0)
            {
               
                cart.Items = cart.Items.Where(x => x.ItemId != product.ID);
                return cart;
               
            }
            return cart;
        }
        else
        {
            throw new BO.BlMissingEntityException("THE PRODUCT IS NOT IN THE CART");
        }
        
    }

    public void OrderConfirmation(BO.Cart cart)
    {
        //check if items are exist
        IEnumerable<DO.Product> productIsExist;
        try
        {
            productIsExist = from item in cart.Items
                             select dal.Product.GetById(item.ID);
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Product not exist", ex);
        }


        //check if items are not negetive
        var isNegetive = cart.Items.Where(x => x.Amount < 1);
        if(isNegetive.Any())
        {
           throw new BO.BlNagtiveNumberException("negative amount in order items");
        }

        //check if items are in stock
        var isInStock = from item in cart.Items
                        select dal.Product.GetById(item.ID).InStock < item.Amount;
        if (isInStock.Any())
        {
            throw new BO.BlNagtiveNumberException("negative amount in order items");
        }

       
        var isEmptyNamed = cart.CustomerName;
        if (isEmptyNamed.Any())
        {
            throw new BO.BlNagtiveNumberException("negative amount in order items");
        }

        if (cart.CustomerName == "")
            throw new BO.BlEmptyStringException("empty customer name");
        if (GetEmail(cart.CustomerEmail))
            throw new BO.BlEmptyStringException("empty customer email");

      
            DO.Order order = new DO.Order();

            order.CustomerName = cart.CustomerName;
            order.CustomerAddress = cart.CustomerAdress;
            order.CustomerEmail = cart.CustomerEmail;
            order.OrderDate = DateTime.Now;
            order.ShipDate = null;
            order.DeliveryDate = null;

     int orderId = dal.Order.Add(order);
       
        var addOrderItem = from item in cart.Items
                select dal.OrderItem.Add(new DO.OrderItem()
                {
                    ID = item.ID,
                    OrderId = orderId,
                    Price = item.Price,
                    Amount = item.Amount,
                });
        foreach (BO.OrderItem item in cart.Items)
        {
            DO.Product product = dal.Product.GetById(item.ID);
            product.InStock -= item.Amount;
            dal.Product.Update(product);
        }
        //var updateProduct = from item in cart.Items
        //select dal.Product.GetById(item.ID)
        //    product.InStock -= item.Amount;
        //    dal.Product.Update(product);
        //cart.Items.Where()



        //foreach (BO.OrderItem item in cart.Items)
        //    {
        //        dal.OrderItem.Add(new DO.OrderItem()
        //        {
        //            ID = item.ID,
        //            OrderId = orderId,
        //            Price = item.Price,
        //            Amount = item.Amount,
        //        });
        //        DO.Product product = dal.Product.GetById(item.ID);
        //        product.InStock -= item.Amount;
        //        dal.Product.Update(product);
        //    }

        }
       
    
    bool GetEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}
