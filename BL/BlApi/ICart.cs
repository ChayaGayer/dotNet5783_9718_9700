using BO;namespace BlApi;
/// <summary>
/// the cart interface
/// </summary>
public interface ICart
{
    BO.Cart AddProductForCart(BO.Cart cart,int productId);
    BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount);

    void OrderConfirmation(BO.Cart cart);
   
}
