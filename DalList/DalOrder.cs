
using DalApi;
using DO;
using System;

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
        order = DataSource.OrdersList.Find(x => x?.ID == id)?? throw new DO.DalMissingIdException(id,"order");//find the order with this id if not found throw exception
       
            return order;

    }
    /// <summary>
    /// Update
    /// </summary>
    /// <param name="order"></param>
    /// <exception cref="Exception"></exception>
    public void Update(Order order)
    {
        Order orderToUpdate = new Order();//create a new order
        orderToUpdate = DataSource.OrdersList.Find(x =>x?.ID == order.ID) ?? throw new DO.DalMissingIdException(order.ID,"order");//find the order with this id if not found throw exception
        DataSource.OrdersList.Remove(orderToUpdate);//if found remove it
        DataSource.OrdersList.Add(order);//add the new one
    }
    /// <summary>
    /// Delete
    /// </summary>
    /// <param name="id"></param>
    /// <exception cref="DO.DalMissingIdException"></exception>
    public void Delete(int id) 
    {
        Order orderToDel=new Order();
        orderToDel = DataSource.OrdersList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id,"order");//find the order with this id if not found throw exception
        DataSource.OrdersList.Remove(orderToDel);//delete rhe found order.if found

    }
    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filtar = null)
    {
        if(filtar != null)
            return DataSource.OrdersList.Where(x => filtar(x));
        return DataSource.OrdersList.Select(x => x);
       

    }
}
