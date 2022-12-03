

using DO;
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

    private static void s_Initialize()
    {

        CreateAndInitProducts();
        CreateAndInitOrders();
        CreateAndInitOrderItems();

    }
    private static void CreateAndInitProducts()//The function that initializes the list of products
    {
        string[] ProductsArray = { " stud earrings", " Watch ", " Neckless", " Tennis bracelets", " Halo rings", " Bands rings" };
        string[] ProductsColorArray = { "Gold", "Silver", "RoseGold" };
        int m = 0;
        for (int i = 0; i < 5; i++)
        {

            for (int j = 0; j < 3; j++)
            {
                int x = s_rand.Next(5);

                ProducstList.Add(
                   new Product
                   {
                       ID = 100000 + m,
                       Price = s_rand.Next(300, 1500),
                       ProductName = ProductsColorArray[j] + ProductsArray[i],
                       Category = (Category)x,
                       InStock = s_rand.Next(10),

                   });
                m++;
            }
        }

        int y = s_rand.Next(5);
        ProducstList.Add(
           new Product
           {
               ID = 100000 + 6 + 3 + m,
               Price = s_rand.Next(300, 1500),
               ProductName = ProductsColorArray[2] + ProductsArray[5],
               Category = (Category)y,
               InStock = 0,

           });

    }



    private static void CreateAndInitOrders()//The function that initializes the list of orders
    {

        string[] CustomersNameArray = { "Shira Choen", "Sigal Levi", "Inbal Peri", "Sarit Shimon", "Chana Gross", " Chen David", "Yoval Leon", " Sari Waiss", "Ella Fridman", "Rcheli Gayer", "Shani Polski", "Reut Hominer", "Nurit Hact", "Sapir Grinboim", "Dasi Shain", "Tzipi Shwartz", "Noa Block", "Roni Berko", "Chaya Gal", "Bar Sason" };
        string[] CustomersAdressArray = { "Tel Aviv", "Petach Tikva", "Hod Hasharon", "Herzeliya", "Ramat Gan", "Azor", "Bney Brak", "Jerushlem", "Natania", "Zfat", "Ashdod", "Rechovot", "Kfar Saba", "Rosh Hayin", "Givat Shmouel", "Ranana", "Hadera", "Be'er Sheva", "Nariya", "Eilat" };
        for (int i = 0; i < 20; i++)//make 20orders
        {

            Order x = new Order();
            x.ID = Config.NextOrderNumber;
            x.CustomerName = CustomersNameArray[i];
            x.CustomerEmail = (CustomersNameArray[i] + "@gmail.com").Replace(' ', '_');
            x.CustomerAddress = CustomersAdressArray[i];
            int month = -s_rand.Next(1, 3);
            x.OrderDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month + month, s_rand.Next(1, DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month + month)));
            if (i < 0.8 * 20)//80% from the orders
            {
                x.ShipDate = x.OrderDate + TimeSpan.FromDays((double)s_rand.Next(4));
                if (i < 0.6 * 20)
                {
                    x.DeliveryDate = x.ShipDate + TimeSpan.FromDays((double)s_rand.Next(4));
                }
                else
                {
                    x.DeliveryDate = DateTime.MinValue;
                }
            }
            else
            {
                x.ShipDate = null;//or null?
                x.DeliveryDate = null;//or null?
            }

            OrdersList.Add(x);//add the object for the order list

        }

    }



    private static void CreateAndInitOrderItems()//The function that initializes the list of the order items
   {
        ////    for(int i = 0; i < 40; i++)
        ////    {
        //        //Product? product=new Product();
        //        //OrderItem item=new OrderItem();
        //        //product = ProducstList[s_rand.Next(0, ProducstList.Count())];
        //        //item.ID = Config.NextOrderItemNumber;
        //        //item.ItemId = product.Value.ID;
        //        //item.Amount = s_rand.Next(1, 3);
        //        //item.OrderId = OrdersList[s_rand.Next(0, OrdersList.Count())].Value.ID;
        //        //item.Price = product.Value.Price ;

        //    for (int i = 0; i < 40;)
        //    {
        //        OrderItem item = new OrderItem();
        //        Product product = new Product();
        //        if (i < 20)
        //            item.OrderId = OrdersList[i].Value.ID;
        //        else
        //            item.OrderId = OrdersList[s_rand.Next(0, OrdersList.Count())].Value.ID;
        //        int x = s_rand.Next(1, 4);
        //        for (int j = 0; j < x; j++)
        //        {
        //            item.ID = Config.NextOrderItemNumber;
        //            product = ProducstList[s_rand.Next(0, ProducstList.Count())].Value;
        //            item.OrderId = 10000 + i;
        //            item.ItemId = product.ID;
        //            item.Amount = s_rand.Next(0, 2);
        //            item.Price = product.Price * item.Amount;
        //            OrderItemsList.Add(item);
        //        }
        //    }
        for (int i = 0; i < 20; i++)//for each order
        {


            for (int j = 0; j <= s_rand.Next(1, 4); j++)//every order need 1-4 product
                                                        //    {
                OrderItemsList.Add(
                new OrderItem
                {
                    ID = Config.NextOrderItemNumber,
                    OrderId = 10000 + i,
                    ItemId = 100000 + s_rand.Next(0, 10),
                    Price = s_rand.Next(300, 1500),
                    Amount = s_rand.Next(1, 4)
                });

        }
        //for (int i = 0; i < 20; i++)//for each order
        //{


        //    for (int j = 0; j <= s_rand.Next(1, 4); j++)//every order need 1-4 product
        //                                                //    {
        //        OrderItemsList.Add(
        //        new OrderItem
        //        {
        //            ID = Config.NextOrderItemNumber,

        //             OrderId = 10000 + i,
        //            ItemId = 100000 + s_rand.Next(0, 10),
        //            Price = s_rand.Next(300, 1500),
        //            Amount = s_rand.Next(0, 2)
        //        });

        //}
    }
    }

    
   
   

