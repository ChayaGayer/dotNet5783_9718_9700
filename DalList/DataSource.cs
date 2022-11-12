

using DO;
using System.Runtime.InteropServices;

namespace Dal;

internal static class DataSource
{
    static DataSource()
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();
    internal static class Config
    {
        internal const int s_startOrderNumber = 100000;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 100000;
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    }
    internal static List<Product?> ProducstList { get; } = new List<Product?>();
    internal static List<Order?> OrdersList { get; } = new List<Order?>();
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>();
    public static void s_Initialize()
    {
        CreateAndInitProducts();
        CreateAndInitOrders();
        CreateAndInitOrderItems();

    }
    private static void CreateAndInitProducts()
    {
        string[] ProductsArray = { "stud earrings", "Drop earrings", "Watch ", "Neckless", "Pendants neckless", "chokers", "Chain bracelets", "Tennis bracelets", "Halo rings", "Bands rings" };
        string[] ProductsColorArray = { "Gold", "Silver", "RoseGold" };
        for (int i = 0; i <= 9; i++)
        {
            for (int j = 0; j <= 3; j++)
            {
                int x = s_rand.Next(5);
                ProducstList.Add(
                   new Product
                   {
                       ID = 100000 + i,
                       Price = s_rand.Next(300, 1500),
                       ProductName = ProductsColorArray[j] + ProductsArray[i],
                       Category = (Category)x,
                       InStock = s_rand.Next(10),

                   });
            }
        }
              int y = s_rand.Next(5);
                ProducstList.Add(
                   new Product 
                   {
                       ID = s_rand.Next(100000 + 9, 999999),
                       Price = s_rand.Next(300, 1500),
                       ProductName = ProductsColorArray[3]+ ProductsArray[9],
                       Category = (Category)y,
                       InStock = 0,

                   });

        }

   
    }
    private static void CreateAndInitOrders()
    {
        string[] CustomersNameArray = { "Shira Choen", "Sigal Levi", "Inbal Peri", "Sarit Shimon", "Chana Gross", " Chen David", "Yoval Leon", " Sari Waiss", "Ella Fridman", "Rcheli Gayer", "Shani Polski", "Reut Hominer", "Nurit Hact", "Sapir Grinboim", "Dasi Shain", "Tzipi Shwartz", "Noa Block", "Roni Berko", "Chaya Gal", "Bar Sason" };
        string[] CustomersAdressArray = { "Tel Aviv", "Petach Tikva", "Hod Hasharon", "Herzeliya", "Ramat Gan", "Azor", "Bney Brak", "Jerushlem", "Natania", "Zfat", "Ashdod", "Rechovot", "Kfar Saba", "Rosh Hayin", "Givat Shmouel", "Ranana", "Hadera", "Be'er Sheva", "Nariya", "Eilat" };
        for (int i = 0; i <= 20; i++)
        {
            OrdersList.Add(
            new Order
            {
                ID = Config.NextOrderNumber,
                CustomerName = CustomersName[i],
                CustomerEmail = CustomersName[i] + "@gmail.com",
                CustomerAddress = CustomersAdress[i],
                OrderDate = DateTime.Now,
                ShipDate = ,
                DeliveryDate = null
            }) ;

            Order x = new Order();
            x.ID = Config.NextOrderNumber;
            x.CustomerName = CustomersNameArray[i];
            x.CustomerEmail = CustomersNameArray[i] + "@gmail.com";
            x.CustomerAddress = CustomersAdressArray[i];
            x.OrderDate = DateTime.Now;
            if (i < 0.8 * 20)
            {
                x.ShipDate = x.OrderDate + TimeSpan.FromDays((double)s_rand.Next(4));
                if (i < 0.6 * 20)
                {
                    x.DeliveryDate = x.ShipDate + TimeSpan.FromDays((double)s_rand.Next(4));
                }
            }
            else
            {
                x.ShipDate = null;
                x.DeliveryDate = null;
            }

            OrdersList.Add(x);
        }

    }

    private static void CreateAndInitOrderItems()
    {
        for (int i = Config.s_startOrderNumber; i <= Config.NextOrderNumber; i++)//for each order
        {
            for (int j = 0; j <= s_rand.Next(1, 4); j++)//every order need 1-4 product
            {
                OrderItemsList.Add(
                new OrderItem
                {
                    ID = Config.NextOrderItemNumber,
                    OrderId = 10000 + i,
                    ItemId = 100000+s_rand.Next(0 ,10),//
                    Price = s_rand.Next(300, 1500),
                    Amount = s_rand.Next(0, 2)
                });

            }
        }
    }
}    
   
   

