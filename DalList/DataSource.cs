

using DO;
using System.Data;
using System.Xml.Linq;

namespace Dal;

internal static class DataSource
{
    static DataSource()//the constructor
    {
        s_Initialize();
    }
    private static readonly Random s_rand = new();//the rand func
    internal static class Config//for the Continuous number
    {
        internal const int s_startOrderNumber = 100000;//for the order
        private static int s_nextOrderNumber = s_startOrderNumber;
        internal static int NextOrderNumber { get => s_nextOrderNumber++; }
        internal const int s_startOrderItemNumber = 100000;//for the number
        private static int s_nextOrderItemNumber = s_startOrderItemNumber;
        internal static int NextOrderItemNumber { get => s_nextOrderItemNumber++; }

    }
    internal static List<Product?> ProducstList { get; } = new List<Product?>();//the list for the product
    internal static List<Order?> OrdersList { get; } = new List<Order?>();//the list for the order
    internal static List<OrderItem?> OrderItemsList { get; } = new List<OrderItem?>(); //the list for the order items
    internal static List<User?> UsersList { get; } = new List<User?>();
    private static void s_Initialize()
    {

        CreateAndInitProducts();
        CreateAndInitOrders();
        CreateAndInitOrderItems();
        CreateAndInitUsers();
        //    XMLTools.SaveListToXMLSerializer(ProducstList, "products");
        //    XMLTools.SaveListToXMLSerializer(OrdersList, "orders");
        //    XMLTools.SaveListToXMLSerializer(OrderItemsList, "orderItems");
        //    XMLTools.SaveListToXMLSerializer(UsersList, "users");
        //}
    }
    /// <summary>
    /// For the users List
    /// </summary>
    private static void CreateAndInitUsers()
    {
        string[] UsersNames = { "Efrat Amar", "Shira Levinzon", "Chaya Gayer","Shira Gayer", "Yeudit Avitan","Inbal Peri"};
        {
            for(int i = 0; i < 3; i++)
            {
                UsersList.Add(
                    new User
                    {
                        Name = UsersNames[i],
                        Email = (UsersNames[i] + "@gmail.com").Replace(' ', '_'),
                        LogIn = UserLogIn.Maneger,
                        Password=123
                        
                    }

                    ) ;
            }
            for(int j = 3; j < 6; j++)
            {
                UsersList.Add(
                   new User
                   {

                       Name = UsersNames[j],
                       Email = (UsersNames[j] + "@gmail.com").Replace(' ', '_'),
                       LogIn = UserLogIn.Coustomer,
                       Password=456
                   }
                   );

            }
        }
       
    }
    /// <summary>
    /// The function that initializes the list of products
    /// </summary>
    private static void CreateAndInitProducts()
    {
        string[] ProductsArray = { " stud earrings", " Tennis bracelets", " Halo rings",  " Neckless", " Watch "," Bands rings" };
        string[] ProductsColorArray = { "Gold", "Silver", "RoseGold" };
        int m = 0;
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 3; j++)
            {
               

                ProducstList.Add(
                   new Product
                   {
                       ID = 100000 + m,
                       Price = s_rand.Next(300, 1500),
                       ProductName = ProductsColorArray[j] + ProductsArray[i],
                       Category = (Category)i,
                       InStock = s_rand.Next(10),

                   });
                m++;
            }
        }

        
        ProducstList.Add(
           new Product
           {
               ID = 100000 + 15,
               Price = s_rand.Next(300, 1500),
               ProductName = ProductsColorArray[2] + ProductsArray[5],
               Category = (Category)2,
               InStock = 0,

           });

    }



    private static void CreateAndInitOrders()//The function that initializes the list of orders
    {

        string[] CustomersNameArray = { "Shira Choen", "Sigal Levi", "Inbal Peri", "Sarit Shimon", "Chana Gross", " Chen David", "Yoval Leon", " Sari Waiss", "Ella Fridman", "Rcheli Gayer", "Shani Polski", "Reut Hominer", "Nurit Hact", "Sapir Grinboim", "Dasi Shain", "Tzipi Shwartz", "Noa Block", "Roni Berko", "Chaya Gal", "Bar Sason" };
        string[] CustomersAdressArray = { "Tel Aviv", "Petach Tikva", "Hod Hasharon", "Herzeliya", "Ramat Gan", "Azor", "Bney Brak", "Jerushlem", "Natania", "Zfat", "Ashdod", "Rechovot", "Kfar Saba", "Rosh Hayin", "Givat Shmouel", "Ranana", "Hadera", "Be'er Sheva", "Nariya", "Eilat" };
        for (int i = 0; i < 10; i++)
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan time1 = new TimeSpan(s_rand.Next(0, 7), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 58));
            TimeSpan time2 = new TimeSpan(s_rand.Next(0, 7), s_rand.Next(0, 24), s_rand.Next(0, 58), s_rand.Next(0, 60));
            Order x = new Order();
            x.ID = Config.NextOrderNumber;
            x.CustomerName = CustomersNameArray[i];
            x.CustomerEmail = (CustomersNameArray[i] + "@gmail.com").Replace(' ', '_');
            x.CustomerAddress = CustomersAdressArray[i];
            x.OrderDate = date;
            x.ShipDate = date + time1;
            x.DeliveryDate = date + time1 + time2;
            OrdersList.Add(x);//add the object for the order list
        }
        for (int i = 0; i < 4; i++)
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan time = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            Order x = new Order();
            x.ID = Config.NextOrderNumber;
            x.CustomerName = CustomersNameArray[i];
            x.CustomerEmail = (CustomersNameArray[i] + "@gmail.com").Replace(' ', '_');
            x.CustomerAddress = CustomersAdressArray[i];
            x.OrderDate = date;
            x.ShipDate = null;//date + time;
            x.DeliveryDate = null;
            OrdersList.Add(x);//add the object for the order list
        }

        for (int i = 0; i < 2; i++)
        {
            DateTime date = new DateTime(s_rand.Next(2021, 2023), s_rand.Next(1, 11), s_rand.Next(1, 30), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            TimeSpan time = new TimeSpan(s_rand.Next(0, 8), s_rand.Next(0, 24), s_rand.Next(0, 60), s_rand.Next(0, 60));
            Order x = new Order();
            x.ID = Config.NextOrderNumber;
            x.CustomerName = CustomersNameArray[i];
            x.CustomerEmail = (CustomersNameArray[i] + "@gmail.com").Replace(' ', '_');
            x.CustomerAddress = CustomersAdressArray[i];
            x.OrderDate = date;
            x.ShipDate =date + time;
            x.DeliveryDate = null;
            OrdersList.Add(x);//add the object for the order list
        }

    }

    /// <summary>
    /// The function that initializes the list of the order items
    /// </summary>
    private static void CreateAndInitOrderItems()
   {
        int x1 = s_rand.Next(15);

        int y = 100000;

        for (int i = 0; i < 20; i++)//for each order
        {
         
            OrderItemsList.Add(
                new OrderItem
                {
                    ID = Config.NextOrderItemNumber,
                    OrderId = 100000 + i,
                    ItemId = 100000 + x1,
                    Price = ProducstList.Find(x => (x?.ID == x1 + y)).Value.Price,
                    Amount = s_rand.Next(1, 10),
                });
            int x2 = s_rand.Next(15);
            while (x2 == x1)
            {
                x2 = s_rand.Next(15);
            }
            OrderItemsList.Add(
               new OrderItem
               {
                   ID = Config.NextOrderItemNumber,
                   OrderId = 10000 + i,
                   ItemId = 100000 + x2,
                   Price = ProducstList.Find(x => (x?.ID == y + x2)).Value.Price,
                   Amount = s_rand.Next(1, 10),

               });
    }
    }
}

    
   
   

