

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
        internal const int s_startOrderNumber = 0;
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
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
        for (int i = 0; i < 9; i++)
        {

            ProducstList.Add(
               new Product
               {
                   ID = s_rand.Next(100000 + i, 999999),
                   Price = s_rand.Next(300, 1500),
                   ProductName = ProductsArray[i],
                   //Category
                   InStock = s_rand.Next(10),

               });

            ProducstList.Add(
               new Product
               {
                   ID = s_rand.Next(100000 + i, 999999),
                   Price = s_rand.Next(300, 1500),
                   ProductName = ProductsArray[9],
                   //Category
                   InStock = 0,

               });

        }

           
    }
    private static void CreateAndInitOrders()
    {
        string[] CustomersName = { "Shira Choen", "Sigal Levi", "Inbal Peri", "Sarit Shimon", "Chana Gross", " Chen David", "Yoval Leon", " Sari Waiss", "Ella Fridman", "Rcheli Gayer", "Shani Polski", "Reut Hominer", "Nurit Hact", "Sapir Grinboim", "Dasi Shain", "Tzipi Shwartz", "Noa Block", "Roni Berko", "Chaya Gal", "Bar Sason" };
        string[] CustomersAdress = { "Tel Aviv", "Petach Tikva", "Hod Hasharon", "Herzeliya", "Ramat Gan", "Azor", "Bney Brak", "Jerushlem", "Natania", "Zfat", "Ashdod", "Rechovot", "Kfar Saba", "Rosh Hayin", "Givat Shmouel", "Ranana", "Hadera", "Be'er Sheva", "Nariya", "Eilat" };
        for (int i = 0; i < 20; i++)
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


        }
    }
private static void CreateAndInitOrderItems()
    {

    }

}
