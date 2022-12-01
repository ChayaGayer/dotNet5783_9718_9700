
using BlApi;
using System.ComponentModel.DataAnnotations;
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
        catch (DO.NotExist ex)
        {
            throw new BO.NotExist(ex);
        }

        var orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);

        if (orderItem != null)
        {
            if (product.InStock > orderItem.Amount)
            {
                orderItem.Amount += 1;
                orderItem.TotalPrice = product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }
          else
            { throw new BO.NagtiveNumberException(""); }
        }

        else///if the item is not exist in the cart
        {

            if (product.InStock > orderItem.Amount)
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
                return cart;
            }
            return cart;
        }




    }

    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount)
    {
        DO.Product product = new DO.Product();
        product = dal.Product.GetById(productId);

        var orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);
        if (orderItem != null)
        {
            if (product.InStock > amount)
            {
                orderItem.Amount += amount;
                orderItem.TotalPrice = product.Price;
                cart.TotalPrice += product.Price;
                return cart;
            }

            if (product.InStock < amount)
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
            throw new BO.NotExist("THE PRODUCT IS NOT IN THE CART");
        }
        
    }

    public void OrderConfirmation(BO.Cart cart/*, string name, string email, string address*/)
    {
        if (cart.Items.Any())
        {
            foreach (BO.OrderItem item in cart.Items)
            {
                if (item.Amount < 1)
                {
                    throw new BO.NagtiveNumberException("negative amount in order items");
                }
                DO.Product produc1;
                try
                {
                    produc1 = dal.Product.GetById(item.ID);
                }
                catch (DO.NotExist ex)
                {
                    throw new BO.NotExist(ex);
                }
                if (produc1.InStock < item.Amount)
                {
                    throw new BO.NagtiveNumberException("there is not enogh amount in stock");
                }
                if (cart.CustomerName == "")
                    throw new BO.EmptyString("empty customer name");
                if (GetEmail(cart.CustomerEmail))
                    throw new BO.EmptyString("empty customer email");

            };
            DO.Order order = new DO.Order();

            order.CustomerName = cart.CustomerName;
            order.CustomerAddress = cart.CustomerAdress;
            order.CustomerEmail = cart.CustomerEmail;
            order.OrderDate = DateTime.Now;
            order.ShipDate = null;
            order.DeliveryDate = null;
            int orderId = dal.Order.Add(order);
            foreach (BO.OrderItem item in cart.Items)
            {
                dal.OrderItem.Add(new DO.OrderItem()
                {
                    ID = item.ID,
                    OrderId = orderId,
                    Price = item.Price,
                    Amount = item.Amount,
                });
                DO.Product product = dal.Product.GetById(item.ID);
                product.InStock -= item.Amount;
                dal.Product.Update(product);
            }

        }
       
    }
    bool GetEmail(string email)
    {
        return new EmailAddressAttribute().IsValid(email);
    }
}
