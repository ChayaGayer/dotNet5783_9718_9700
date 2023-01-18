namespace BlApi;
/// <summary>
/// the cart interface
/// </summary>
public interface ICart
{
    /// <summary>
    /// get a logic cart and product id and add it to the cart than return the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <returns></returns>
    BO.Cart AddProductForCart(BO.Cart cart,int productId);
    /// <summary>
    ///  get a logic cart ,product id and  amount of product and update it than return the cart
    /// </summary>
    /// <param name="cart"></param>
    /// <param name="productId"></param>
    /// <param name="amount"></param>
    /// <returns></returns>
    BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount);
    /// <summary>
    /// get a cart and confirm the order
    /// </summary>
    /// <param name="cart"></param>

    void OrderConfirmation(BO.Cart cart);
   
}
