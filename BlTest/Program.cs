﻿/*using BlApi;
using BlImplementation;
using BO;

namespace BlTest;
    
internal class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, World!");
        IBl bl=new Bl();
        IEnumerable<ProductForList> productFL = bl.Product.GetListedProducts();
    }
}
*/
using BO;
using BlApi;
using BlImplementation;


namespace BITest
{
    public enum Options { PRODUCT = 1, ORDER, CART, EXIT };
    public enum ProductActions { Product_List = 1, ProductDetails, Add, Delete, Update, Catalog, Product_in_Catalog, Exit }
    public enum CartActions { Add = 1, Update, Create, Exit }
    public enum OrderActions { Get_Order = 1, Order_List, Update_Ship, Update_Delivery, Order_Tracking, Update_Order, Exit }
    internal class Program
    {
        static IBl bl = new Bl();

        static Cart newCart = new Cart() { CustomerAddress = "", CustomerEmail = "", CustomerName = "", Items = null, TotalPrice = 0 };



        public static void ProductOptions()
        {

            ProductActions choice;
            Console.WriteLine(@"insert your choice:1: list of the products,2:details of product,3:add a product,4:delete a product,5:update product,6:for catalog,7:delails of product from catalog");

            if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception(" not exist!");
            while (choice != ProductActions.Exit)
            {
                switch (choice)
                {
                    case ProductActions.Product_List:
                        var lst = bl.Product.GetListedProducts();//getproductlist
                        foreach (var item in lst)
                            Console.WriteLine(item);
                        break;
                    case ProductActions.ProductDetails:
                        int id;
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(bl.Product.RequestProductDetaForC);//for c?m
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
                        addProduct.ProductName= Console.ReadLine();
                        Console.WriteLine("enter price of product:");
                        if (!double.TryParse(Console.ReadLine(), out price)) throw new Exception("wrong input type");
                        addProduct.Price = price;
                        Console.WriteLine("enter category of product:");
                        if (!int.TryParse(Console.ReadLine(), out category)) throw new Exception("wrong input type");
                        addProduct.Category = (Category)(category);
                        Console.WriteLine("enter amount in stock of product:");
                        if (!int.TryParse(Console.ReadLine(), out stock)) throw new Exception("wrong input type");
                        addProduct.InStock = stock;
                        bl.Product.AddProduct(addProduct);
                        break;
                    case ProductActions.Delete:
                        Console.WriteLine("enter id to delete product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        bl.Product.DeleteProduct(id);
                        break;
                    case ProductActions.Update:
                        Product updateProduct = new Product();
                        Console.WriteLine("enter id of product:");
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
                        foreach (var item in bl.Product.GetListedProductsForC())//catalog?
                            Console.WriteLine(item);
                        break;
                    case ProductActions.Product_in_Catalog:
                        Console.WriteLine("enter id of product:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type");
                        Console.WriteLine(bl.Product.RequestProductDetaForC( id));//getproduct by id?
                        break;
                    case ProductActions.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine("");
                Console.WriteLine(@"insert your choice:1: list of products,2:details of product,3:add product,4:delete product,5:update product,6:show catalog 7:delails of product item");
                if (!ProductActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("not exist!");
            }

    
}
        public static void OrderOptions()
        {
            int id;
            Console.WriteLine(@"insert your choice:1:oder details,2:list of orders,3:update ship date,4:update delivery date,5:order tracking 6:update order");
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
                            Console.WriteLine(bl.Order.RequestOrderDeta(id));//getorderbyid?
                            break;
                        case OrderActions.Order_List:
                            Console.WriteLine(String.Join(" ", bl.Order.GetListedOrders()));//func
                            break;
                        case OrderActions.Update_Ship:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.(id));//ship
                            break;
                        case OrderActions.Update_Delivery:
                            Console.WriteLine("please insert order Id");
                            if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                            Console.WriteLine(bl.Order.(id));//delivary
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
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            } while (ch != OrderActions.Exit);

        }
        public static void CartOptions()
        {
            CartActions choice;
            Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:");
            if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");
            while (choice != CartActions.Exit)
            {
                switch (choice)
                {
                    case CartActions.Add:
                        int id, amount;
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter id of product to add to cart:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                        Console.WriteLine(bl.Cart.AddToCart(newCart, id));
                        break;
                    case CartActions.Update:
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        Console.WriteLine("enter id of product to add to cart:");
                        if (!int.TryParse(Console.ReadLine(), out id)) throw new Exception("wrong input type ");
                        Console.WriteLine("enter new amount of product:");
                        if (!int.TryParse(Console.ReadLine(), out amount)) throw new Exception("wrong input type ");
                        Console.WriteLine(bl.Cart.UpdateProductInCart(newCart, amount, id));
                        break;
                    case CartActions.Create:
                        Console.WriteLine("please insert name:");
                        newCart.CustomerName = Console.ReadLine();
                        Console.WriteLine("please insert address:");
                        newCart.CustomerAddress = Console.ReadLine();
                        Console.WriteLine("please insert email address:");
                        newCart.CustomerEmail = Console.ReadLine();
                        bl.Cart.OrderCreate(newCart, newCart.CustomerName, newCart.CustomerEmail, newCart.CustomerAddress);
                        break;
                    case CartActions.Exit:
                        break;
                    default:
                        break;
                }
                Console.WriteLine(@"Choose one of the following options:
1:add product to cart
2:update amount of product in cart
3:create a new order:");
                if (!CartActions.TryParse(Console.ReadLine(), out choice)) throw new Exception("wrong input type");


            }

         
}
        static void Main(string[] args)
        {
            Console.WriteLine(@"insert your choice:1: Products,2:Orders,3:Carts, 0:Exit");
            Options choice;
            if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception("not exist option!");
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
                Console.WriteLine(@"insert your choice:1: Products,2:Orders,3:Carts, 0:Exit");
                if (!Options.TryParse(Console.ReadLine(), out choice)) throw new Exception(" not exist option!");
            }
        }
    }
}




