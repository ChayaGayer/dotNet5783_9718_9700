
using DO;

namespace Dal;

 public class DalOrder
{
    //create
    public int Add(Order order)
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrdersList.Add(order);
        return order.ID;
   }
    //Request
    public Order GetById(int id)
    {
        Order order=new Order();
        order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new Exception("not found");
       
            return order;

    }
    //Update
    public void Update(Order order)
    {
        Order orderToUpdate = new Order();
        orderToUpdate = DataSource.OrdersList.Find(x =>x?.ID == order.ID) ?? throw new Exception("not found");
        DataSource.OrdersList.Remove(orderToUpdate);
        DataSource.OrdersList.Add(order);
    }
    //Delete
    public void Delete(int id) 
    {
        Order orderToDel=new Order();
        orderToDel = DataSource.OrdersList.Find(x => x?.ID == id) ?? throw new Exception("not found");
        DataSource.OrdersList.Remove(orderToDel);

    }
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> list = new List<Order?>();
        for(int i = 0; i < DataSource.OrdersList.Count; i++)
{
            Order? newOrder  = new Order();
            newOrder = DataSource.OrdersList[i];
            list.Add(newOrder);

        }
        return list;
    }
}
