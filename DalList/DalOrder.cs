
using DalApi;
using DO;

namespace Dal;

 internal class DalOrder : IOrder
{
    //create
    public int Add(Order order)
    {
        order.ID = DataSource.Config.NextOrderNumber;
        DataSource.OrdersList.Add(order);//add to the list
        return order.ID;
   }
    //Request
    public Order GetById(int id)
    {
        Order order=new Order();//create a new order
        order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new Exception("not found");//find the order with this id if not found throw exception
       
            return order;

    }
    //Update
    public void Update(Order order)
    {
        Order orderToUpdate = new Order();//create a new order
        orderToUpdate = DataSource.OrdersList.Find(x =>x?.ID == order.ID) ?? throw new Exception("not found");//find the order with this id if not found throw exception
        DataSource.OrdersList.Remove(orderToUpdate);//if found remove it
        DataSource.OrdersList.Add(order);//add the new one
    }
    //Delete
    public void Delete(int id) 
    {
        Order orderToDel=new Order();
        orderToDel = DataSource.OrdersList.Find(x => x?.ID == id) ?? throw new Exception("not found");//find the order with this id if not found throw exception
        DataSource.OrdersList.Remove(orderToDel);//delete rhe found order.if found

    }
    public IEnumerable<Order?> GetAll()
    {
        List<Order?> list = new List<Order?>();//create a new list
        for(int i = 0; i < DataSource.OrdersList.Count; i++)//go over the list
{
            Order? newOrder  = new Order();//create new order
            newOrder = DataSource.OrdersList[i];//copy from the list to the new list
            list.Add(newOrder);//add

        }
        return list;
    }
}
