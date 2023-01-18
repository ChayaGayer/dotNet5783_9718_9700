using DalApi;
using DO;
using System;

namespace Dal
{
    internal class DalOrderItem:IOrderItem
    {
        
       /// <summary>
       /// Add
       /// </summary>
       /// <param name="orderItem"></param>
       /// <returns></returns>
        public int Add(OrderItem orderItem)
        {
            orderItem.ID = DataSource.Config.NextOrderNumber;
            DataSource.OrderItemsList.Add(orderItem);
            return orderItem.ID;
        }
        /// <summary>
        /// Request order item by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <exception cref="DO.DalMissingIdException"></exception>
        public OrderItem GetById(int id)
        {
            OrderItem newOrderItem = new OrderItem();//create a new  item
            newOrderItem = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "orderItem");//if found the orderItem with the same id if not throw exception

            return newOrderItem;

        }
        /// <summary>
        /// update
        /// </summary>
        /// <param name="orderItem"></param>
        /// <exception cref="DO.DalMissingIdException"></exception>
        public void Update(OrderItem orderItem)
        {
            OrderItem orderItemToUpdate = new OrderItem();
            orderItemToUpdate = DataSource.OrderItemsList.Find(x => x?.ID == orderItem.ID) ?? throw new DO.DalMissingIdException(orderItem.ID, "orderItem");//if found the orderItem with the same id if not throw exception
            DataSource.OrderItemsList.Remove(orderItemToUpdate);//if found remove it
            DataSource.OrderItemsList.Add(orderItem);//add the right one
        }
        /// <summary>
        /// delet if found
        /// </summary>
        /// <param name="id"></param>
        /// <exception cref="DO.DalMissingIdException"></exception>
        public void Delete(int id)
        {
            OrderItem orderItemToDell = new OrderItem();
            orderItemToDell = DataSource.OrderItemsList.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "orderItem");///if found the orderItem with the same id if not throw exception
            DataSource.OrderItemsList.Remove(orderItemToDell);

        } 
        /// <summary>
        /// Get all the orderitem
        /// </summary>
        /// <param name="filtar"></param>
        /// <returns></returns>
        public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filtar = null)
        {
            if (filtar != null)
                return DataSource.OrderItemsList.Where(x => filtar(x));
            return DataSource.OrderItemsList.Select(x => x);
        }
        /// <summary>
        /// get the id and return add the items of this order
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<OrderItem?> GetOrderItems(int id)
        {

            var allTheOrder = DataSource.OrderItemsList.Where(x => x.Value.OrderId == id);
            return allTheOrder;
        }
        /// <summary>
        /// get the item by the order id and the product id
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="itemId"></param>
        /// <returns></returns>
        /// <exception cref="DO.DalMissingIdException"></exception>
        public OrderItem GetTheItem(int orderId, int itemId)
        {
            OrderItem? theOrderItem = DataSource.OrderItemsList.Find(x=>x?.ItemId == itemId && x?.OrderId == orderId)?? throw new DO.DalMissingIdException(orderId, "orderItem");//throw exception;
            return (OrderItem)theOrderItem;
            
        }

    }
   

}
