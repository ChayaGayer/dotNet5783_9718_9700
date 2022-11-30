using BO;namespace BlApi;

public interface ICart
{
    BO.Cart AddProductForCart(BO.Cart cart,int productId);
    BO.Cart UpdateAmountOfProduct(BO.Cart cart, int productId, int amount);

    void OrderConfirmation(BO.Cart cart,string name,string email,string address);
   
}
