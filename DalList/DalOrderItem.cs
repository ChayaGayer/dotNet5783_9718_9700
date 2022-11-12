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
            OrderItem newOrderItem = new OrderItem();
            newOrderItem = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("not found");

            return newOrderItem;

        }
        //Update
        public void Update(OrderItem orderItem)
        {
            OrderItem orderItemToUpdate = new OrderItem();
            orderItemToUpdate = DataSource.OrderItemsList.Find(x => x?.ID == orderItem.ID) ?? throw new Exception("not found");
            DataSource.OrderItemsList.Remove(orderItemToUpdate);
            DataSource.OrderItemsList.Add(orderItem);
        }
        //Delete
        public void Delete(int id)
        {
            OrderItem orderItemToDell = new OrderItem();
            orderItemToDell = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new Exception("not found");
            DataSource.OrderItemsList.Remove(orderItemToDell);

        }
        public IEnumerable<OrderItem?> GetAll()
{
            List<OrderItem?> listO = new List<OrderItem?>();
            for (int i = 0; i < DataSource.OrderItemsList.Count; i++)
    {
               OrderItem? newOrderItem = new OrderItem();
               newOrderItem = DataSource.OrderItemsList[i];
                listO.Add(newOrderItem);

            }
            return listO;
        }
    }
}
