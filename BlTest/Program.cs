using BO;
using System.Security.Cryptography.X509Certificates;
using BlApi;
using BlImplementation;
using System.Linq.Expressions;

namespace BITest
{
    public enum Options { PRODUCT = 1, ORDER, CART, EXIT };
    public enum ProductActions { Product_List = 1, ProductDetails, Add, Delete, Update, Catalog, Product_in_Catalog, Exit }
    public enum CartActions { Add = 1, Update, Create, Exit }
    public enum OrderActions { Get_Order = 1, Order_List, Update_Ship, Update_Delivery, Order_Tracking, Update_Order, Exit }
    internal class Program
    {
        static IBl bl = Factory.Get();

        static Cart newCart = new Cart() { CustomerAdress = "", CustomerEmail = "", CustomerName = "", Items = new List<OrderItem>(), TotalPrice = 0 };



        public static void ProductOptions()
        {
            string x;
            ProductActions choice;
            Console.WriteLine(@"insert your choice:
1:list of products,
2:details of product,
3:add product,
4:delete product,
5:update product,
6:show catalog
7:delails of product from catalog
8:for back");

            if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != ProductActions.Exit)
            {
                try {
                    switch (choice)
                    {
                        case ProductActions.Product_List:
                            var lst = bl.Product.GetListedProducts();
                            foreach (var item in lst)
                                Console.WriteLine(item);
                            break;
                        case ProductActions.ProductDetails:
                            int id;
                            Console.WriteLine("enter the id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            Console.WriteLine(bl.Product.RequestProductDetaForM(id));
                            break;
                        case ProductActions.Add:
                            Product addProduct = new Product();
                            double price;
                            int category;
                            int stock;
                            Console.WriteLine("enter id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            addProduct.ID = id;
                            Console.WriteLine("enter name of product:");
                            addProduct.ProductName = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                            addProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            
                            if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                            addProduct.Category = (Category)category;
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                            addProduct.InStock = stock;
                            bl.Product.AddProduct(addProduct);
                            break;
                        case ProductActions.Delete:
                            Console.WriteLine("enter id for delete a product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            bl.Product.DeleteProduct(id);
                            break;
                        case ProductActions.Update:
                            Product updateProduct = new Product();
                            Console.WriteLine("enter the id of product:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            updateProduct.ID = id;
                            Console.WriteLine("enter name of product:");
                            updateProduct.ProductName = Console.ReadLine();
                            Console.WriteLine("enter price of product:");
                            if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                            updateProduct.Price = price;
                            Console.WriteLine("enter category of product:");
                            if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                            updateProduct.Category = (Category)category;
                            Console.WriteLine("enter amount in stock of product:");
                            if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                            updateProduct.InStock = stock;
                            bl.Product.UpdateProductData(updateProduct);
                            break;
                        case ProductActions.Catalog:
                            foreach (var item in bl.Product.GetListedProductsForC())
                                Console.WriteLine(item);
                            break;
                        case ProductActions.Product_in_Catalog:
                            Console.WriteLine("enter the id of product :");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                            Console.WriteLine(bl.Product.RequestProductDetaForC(id, newCart));
                            break;
                        case ProductActions.Exit:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine("");
                    Console.WriteLine(@"insert your choice 1: list of products,2:details of product,3:add product,4:delete product,5:update product,6:show catalog,7:delails of product item");
                    if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
                }
                catch(BO.BlMissingEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlEmptyStringException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlIncorrectDatesException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlAlreadyExistEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlInCorrectException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlNagtiveNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlNullPropertyException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(BO.BlWorngCategoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                }
            

           
}
        public static void OrderOptions()
        {
            int id;
            Console.WriteLine(@"insert your choice 
1:order details,
2:list of orders,
3:update ship date,
4:update delivery date,
5:order tracking,
6:update order
7:for back");
            OrderActions ch = OrderActions.Exit;

            do
            {
                try
                {
                    if (!OrderActions.TryParse(Console.ReadLine(), out ch)) throw new Exception("wrong input type");
                    switch (ch)
                    {
                        case OrderActions.Get_Order:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.RequestOrderDeta(id));
                            break;
                        case OrderActions.Order_List:
                            Console.WriteLine(String.Join(" ", bl.Order.GetListedOrders()));
                            break;
                        case OrderActions.Update_Ship:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.UpdateSendOrder(id));
                            break;
                        case OrderActions.Update_Delivery:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.UpdateSupplyOrder(id));
                            break;
                        case OrderActions.Order_Tracking:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.OrderTracking(id));
                            break;
                        case OrderActions.Update_Order: 
                         break;
                        case OrderActions.Exit:
                            break;
                        default:
                            break;
                    }
                }
                catch (BO.BlMissingEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlEmptyStringException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlIncorrectDatesException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlAlreadyExistEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlInCorrectException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlNagtiveNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlNullPropertyException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlWorngCategoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                
            } while (ch != OrderActions.Exit);

        }
        public static void CartOptions()
        {
            CartActions choice;
            Console.WriteLine(@"insert your choice 
1:add product to cart,
2:update amount of product in cart,
3:create a new order-OrderConfirmation:
4:for back");
            if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            while (choice != CartActions.Exit)
            {
                try
                {
                    switch (choice)
                    {
                        case CartActions.Add:
                            int id, amount;
                            Console.WriteLine("insert name:");
                            newCart.CustomerName = Console.ReadLine();
                            Console.WriteLine("insert address:");
                            newCart.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("insert email address:");
                            newCart.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.AddProductForCart(newCart, id));
                            break;
                        case CartActions.Update:
                            Console.WriteLine("insert name:");
                            newCart.CustomerName = Console.ReadLine();
                            Console.WriteLine("insert address:");
                            newCart.CustomerAdress = Console.ReadLine();
                            Console.WriteLine("insert email address:");
                            newCart.CustomerEmail = Console.ReadLine();
                            Console.WriteLine("enter id of product to add to cart:");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine("enter new amount of product:");
                            if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Cart.UpdateAmountOfProduct(newCart, id, amount));
                            break;
                        case CartActions.Create:
                            bl.Cart.OrderConfirmation(newCart);
                            //newCart = new Cart();
                            break;
                        case CartActions.Exit:
                            break;
                        default:
                            break;
                    }
                    Console.WriteLine(@"insert your choice:1:add product to cart,2:update amount of product in cart,3:create a new order:");
                    if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");

                }
                catch (BO.BlMissingEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlEmptyStringException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlIncorrectDatesException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlAlreadyExistEntityException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlInCorrectException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlNagtiveNumberException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlNullPropertyException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (BO.BlWorngCategoryException ex)
                {
                    Console.WriteLine(ex.Message);
                    break;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    break;

                }
            }

           
}
        static void Main(string[] args)
        {
            Console.WriteLine(@"insert your choice:
1: Products,
2:Orders,
3:Carts,
0:Exit");
            Options choice;
            if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            while (choice != Options.EXIT)
            {
                switch (choice)
                {
                    case Options.PRODUCT:
                        ProductOptions();
                        break;
                    case Options.ORDER:
                        OrderOptions();
                        break;
                    case Options.CART:
                        CartOptions();
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"insert your choice:
1: Products,
2:Orders,
3:Carts,
0:Exit");
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("This option not exist!");
            }
        }
    }
}
