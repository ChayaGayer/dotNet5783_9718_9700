
using Dal;
using DO;

namespace DalTest
{
    class Program
    {
       
        static void ForProduct (DalProduct product)
        {

            
            Console.WriteLine("Choose one of the following:enter f for stop");
            Console.WriteLine("a :for add");
            Console.WriteLine("b: for show product");
            Console.WriteLine("c: for show list");
            Console.WriteLine("d: for update");
            Console.WriteLine("e: for delete");
            int id;
            char x;

            do
            {
                char.TryParse(Console.ReadLine(), out x );
                switch (x)
                {

                    case 'a':
                        {
                            
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the product name");
                            string name;
                            name = Console.ReadLine();
                            double price;
                            Console.WriteLine("Enter the price of the product");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the category");
                            Category catagory;
                            Category.TryParse(Console.ReadLine(), out catagory);
                            Console.WriteLine("Enter the stock:");
                            int inStock;
                            int.TryParse(Console.ReadLine(), out inStock);
                            Product product2 = new Product();
                            product2.ID = id;
                            product2.ProductName = name;
                            product2.Price = price;
                            product2.InStock = inStock;
                            int d = product.Add(product2);
                            Console.WriteLine(d);
                        } break;
                    case 'b':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            Product newProduct= product.GetById(id);
                           Console.WriteLine(newProduct); } break;
                    case 'c':
                        {
                            foreach (var item in product.GetAll())
                            {
                                Console.WriteLine(item);//
                            }
                        }
                        break;
                    case 'd':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the product name");
                            string name;
                            name = Console.ReadLine();
                            double price;
                            Console.WriteLine("Enter the price of the product");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the category");
                            Category catagory;
                            Category.TryParse(Console.ReadLine(), out catagory);
                            Console.WriteLine("Enter the stock:");
                            int inStock;
                            int.TryParse(Console.ReadLine(), out inStock);
                            Product product2 = new Product();
                            product2.ID = id;
                            product2.ProductName = name;
                            product2.Price = price;
                            product2.InStock = inStock;
                            product.Update(product2); } break;
                    case 'e':
                        {
                            Console.WriteLine("Enter the product id");
                            int.TryParse(Console.ReadLine(), out id);
                            product.Delete(id); } break;
                    default: Console.WriteLine("finish"); break;

                }
            }
            while (x != 'f');
        }

         static void ForOrder(DalOrder order)
        {

            //DalOrder Order = new DalOrder();
            int id;
            char x;
           
            Console.WriteLine("Choose one of the following:,enter f for stop");
            Console.WriteLine("a :for add");
            Console.WriteLine("b: for show order");
            Console.WriteLine("c: for show list");
            Console.WriteLine("d: for update");
            Console.WriteLine("e: for delete");
           
            do
            {
                char.TryParse(Console.ReadLine(), out x);
                switch (x)
                {

                    case 'a':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the customer name");
                            string name;
                            name = Console.ReadLine();
                            string email;

                            Console.WriteLine("Enter the customer email");
                            email = Console.ReadLine();
                            string address;
                            Console.WriteLine("Enter the customer address:");
                            address = Console.ReadLine();
                            DateTime orderDate;
                            Console.WriteLine("Enter the day of the order");
                            DateTime.TryParse(Console.ReadLine(), out orderDate);
                            Order order1 = new Order();
                            order1.ID = id;
                            order1.CustomerName = name;
                            order1.CustomerEmail = email;
                            order1.CustomerAddress = address;
                            order.Add(order1);
                            Console.WriteLine(order1);
                        }; break;
                    case 'b':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id);
                            Order newOrder = order.GetById(id);
                            Console.WriteLine(newOrder);
                        }
                        break;
                    case 'c':
                        {

                            foreach (var item in order.GetAll())
                            {
                                Console.WriteLine(item);//
                            }

                        }
                        break;
                    case 'd': {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id);
                            Console.WriteLine("Enter the customer name");
                            string name;
                            name = Console.ReadLine();
                            string email;

                            Console.WriteLine("Enter the customer email");
                            email = Console.ReadLine();
                            string address;
                            Console.WriteLine("Enter the customer address:");
                            address = Console.ReadLine();
                            DateTime orderDate;
                            Console.WriteLine("Enter the day of the order");
                            DateTime.TryParse(Console.ReadLine(), out orderDate);
                            Order order1 = new Order();
                            order1.ID = id;
                            order1.CustomerName = name;
                            order1.CustomerEmail = email;
                            order1.CustomerAddress = address;
                            order.Update(order1);
                        } ; break;
                    case 'e':
                        {
                            Console.WriteLine("Enter the order id");
                            int.TryParse(Console.ReadLine(), out id);
                            order.Delete(id);
                        } break;
                    default: Console.WriteLine("finish"); break;

                }
            }
            while (x != 'f');
        }


        static void ForOrderItem(DalOrderItem orderItem)
        {
            
           int id;
            char x;
           
            Console.WriteLine("Choose one of the following:enter f for stop");
            Console.WriteLine("a :for add");
            Console.WriteLine("b: for show product");
            Console.WriteLine("c: for show list");
            Console.WriteLine("d: for update");
            Console.WriteLine("e: for delete");
            Console.WriteLine("g: for show order items");
            Console.WriteLine("h: for show the items");
            

            do
            {
                char.TryParse(Console.ReadLine(), out x);
                switch (x)
                {
                    case 'a': 
                        {
                           
                            Console.WriteLine("Enter the orderItem id");
                            int.TryParse(Console.ReadLine(), out id);
                            int orderId;
                            Console.WriteLine("Enter the orderId");
                            int.TryParse(Console.ReadLine(), out orderId);
                            int itemId;
                            Console.WriteLine("Enter the itemId");
                            int.TryParse(Console.ReadLine(), out itemId);
                            double price;
                            Console.WriteLine("Enter the price of the orderItem");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the amount");
                            int amount;
                            int.TryParse(Console.ReadLine(), out amount);
                            OrderItem orderItem1 = new OrderItem();
                            orderItem1.ID = id;
                            orderItem1.OrderId = orderId;
                            orderItem1.ItemId = itemId;
                            orderItem1.Price = price;
                            orderItem1.Amount = amount;
                            orderItem.Add(orderItem1);
                            Console.WriteLine(orderItem1.ID);
                           
                        }; break;
                    case 'b':
                        {
                            Console.WriteLine("Enter the orderItem id");
                            int.TryParse(Console.ReadLine(), out id);
                            OrderItem newOrderItem = orderItem.GetById(id);
                            Console.WriteLine(newOrderItem);
                        }
                        break;
                    case 'c':
                        {
                           foreach(var item in orderItem.GetAll())
                            {
                                Console.WriteLine(item);//
                            }
                           
                            } break;
                    case 'd': {
                            Console.WriteLine("Enter the orderItem id");
                            int.TryParse(Console.ReadLine(), out id);
                            int orderId;
                            Console.WriteLine("Enter the orderId");
                            int.TryParse(Console.ReadLine(), out orderId);
                            int itemId;
                            Console.WriteLine("Enter the itemId");
                            int.TryParse(Console.ReadLine(), out itemId);
                            double price;
                            Console.WriteLine("Enter the price of the orderItem");
                            double.TryParse(Console.ReadLine(), out price);
                            Console.WriteLine("Enter the amount");
                            int amount;
                            int.TryParse(Console.ReadLine(), out amount);
                            OrderItem orderItem1 = new OrderItem();
                            orderItem1.ID = id;
                            orderItem1.OrderId = orderId;
                            orderItem1.ItemId = itemId;
                            orderItem1.Price = price;
                            orderItem1.Amount = amount;
                            orderItem.Update(orderItem1); break;
                        }
                    case 'e':
                        {
                            Console.WriteLine("Enter the orderItem id");
                            int.TryParse(Console.ReadLine(), out id);
                            orderItem.Delete(id); break;
                        } 
                    case 'g': {
                            Console.WriteLine("Enter the orderItem id");
                            int.TryParse(Console.ReadLine(), out id);
                            foreach (var item in orderItem.GetOrderItems(id))
                            {
                                Console.WriteLine(item);//
                            }

                        }
                        break;
                    case 'h':
                        {
                            int itemId;
                            int orderId;
                            Console.WriteLine("Enter the orderId, and itemId");
                            int.TryParse(Console.ReadLine(), out orderId);
                            int.TryParse(Console.ReadLine(), out itemId);
                            Console.WriteLine(orderItem.GetTheItem(orderId, itemId));

                        } break;
                    default: Console.WriteLine("finish"); break;

                }
            }
            while (x != 'f');
        }


        static void Main(string[] args)
        {
            
            DalOrder order = new DalOrder();
            DalOrderItem orderItem = new DalOrderItem();
            DalProduct product = new DalProduct();

        Console.WriteLine("Choose one of the following:");
            Console.WriteLine("0 :for exist");
            Console.WriteLine("1: for product");
            Console.WriteLine("2: for order");
            Console.WriteLine("3: for order item");
            char ch;
            try
            {
                do
                {

                    char.TryParse(Console.ReadLine(), out ch);
                    switch (ch)
                    {

                        case '1': ForProduct(product); break;
                        case '2': ForOrder(order); break;
                        case '3': ForOrderItem(orderItem); break;

                        default:
                            Console.WriteLine("finish"); break;

                    }
                } while (ch != '0');
            }

            catch(Exception e) { Console.WriteLine(e.Message); }


        }

    }
}

    
    

