using BlApi;
using BO;
namespace BlImplementation;

internal class Cart:ICart
{
   public BO.Cart AddProductForCart(BO.Cart cart, int productId)
    {
        throw new NotImplementedException();
    }
    public BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount)
    {
        throw new NotImplementedException();
    }

    public void OrderConfirmation(BO.Cart cart, string name, string email, string address)
    {
        throw new NotImplementedException();
    }
}
