using DO;

namespace Dal
{
    public class DalOrderItem
    {
        
        //create
        public int Add(OrderItem orderItem)
        {
            orderItem.ID = DataSource.Config.NextOrderNumber;
            DataSource.OrderItemsList.Add(orderItem);
            return orderItem.ID;
        }
        //Request
        public OrderItem GetById(int id)
        {
            OrderItem newOrderItem = new OrderItem();//create a new  item
            newOrderItem = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("not found");//if found the orderItem with the same id if not throw exception

            return newOrderItem;

        }
        //Update
        public void Update(OrderItem orderItem)
        {
            OrderItem orderItemToUpdate = new OrderItem();
            orderItemToUpdate = DataSource.OrderItemsList.Find(x => x?.ID == orderItem.ID) ?? throw new Exception("not found");//if found the orderItem with the same id if not throw exception
            DataSource.OrderItemsList.Remove(orderItemToUpdate);//if found remove it
            DataSource.OrderItemsList.Add(orderItem);//add the right one
        }
        //Delete
        public void Delete(int id)
        {
            OrderItem orderItemToDell = new OrderItem();
            orderItemToDell = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("not found");///if found the orderItem with the same id if not throw exception
            DataSource.OrderItemsList.Remove(orderItemToDell);//delet if found

        }
        public IEnumerable<OrderItem?> GetAll()
        {
            List<OrderItem?> listO = new List<OrderItem?>();//create a new list
            for (int i = 0; i < DataSource.OrderItemsList.Count; i++)//go over the list
            {


                listO.Add(DataSource.OrderItemsList[i]);//add to the list the copy one

            }
            return listO;
        }
        public IEnumerable<OrderItem?> GetOrderItems(int id)//get the id and return akk the items of this order
        {
            List<OrderItem?> allTheOrder = new List<OrderItem?>();
            for (int i = 0; i < DataSource.OrderItemsList.Count; i++)//go over the new list
            {
                if (DataSource.OrderItemsList[i]?.OrderId == id)//if its the same id
                    allTheOrder.Add(DataSource.OrderItemsList[i]);//add it to the list
            }
            return allTheOrder;
        }

        public OrderItem GetTheItem(int orderId, int itemId)//get the item by the order id and the product id
        {
            OrderItem? theOrderItem = new OrderItem();
            for (int i = 0; i < DataSource.OrderItemsList.Count; i++)//go over the list
            {
                if (DataSource.OrderItemsList[i]?.OrderId == orderId && DataSource.OrderItemsList[i]?.ItemId == itemId)//if it is the product
                {
                    theOrderItem = DataSource.OrderItemsList[i];//the product
                }

            }
            if (theOrderItem == null)//if not found
                throw new Exception("not exist");//throw exception
            return (OrderItem)theOrderItem;
        }

    }
   

}
