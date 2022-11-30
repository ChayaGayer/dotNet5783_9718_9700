
using BlApi;
using System.ComponentModel.DataAnnotations;
using System.Transactions;
namespace BlImplementation;

internal class Cart: ICart
{
    DalApi.IDal dal = new Dal.DalList();
    public BO.Cart AddProductForCart(BO.Cart cart, int productId)
    {
        DO.Product product = new DO.Product();
        try
        {
            product = dal.Product.GetById(productId);
        }
        catch(DO.)
            var orderItem = cart.Items.FirstOrDefault(x => x.ItemId == productId);
            if (orderItem != null)
            {
                if (product.InStock > 0)
                {
                    orderItem.Amount += 1;
                    orderItem.TotalPrice = product.Price;
                    cart.TotalPrice += product.Price;
                    return cart;
                }
            }

            else///if the item is not exist in the cart
            {

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
                    return cart;
                }
            }
        }





        catch (Exception ex)
        {
            throw new Exception("MISSING");
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
                orderItem.TotalPrice -= (amount - product.InStock)*product.Price;
                cart.TotalPrice -= (amount-product.InStock)*product.Price;
                return cart;
            }
            if ( amount == 0)
            {
                cart.Items = cart.Items.Where(x => x.ItemId != product.ID);
                return cart;
;               
            }
            return cart;
        }

    }

    public void OrderConfirmation(BO.Cart cart, string name, string email, string address)
    {
        throw new NotImplementedException();
    }
}
