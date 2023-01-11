using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrderItem : IOrderItem
{
    readonly string s_orderItems = "orderItems";
    public int Add(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        //orderItem.ID = DataSource.Config.NextOrderNumber;
        listOrdItem.Add(orderItem);
        XMLTools.SaveListToXMLSerializer(listOrdItem, s_orderItems);
        return orderItem.ID;
    }

    public void Delete(int id)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        OrderItem orderItemToDell = new OrderItem();
        orderItemToDell = listOrdItem.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "orderItem");///if found the orderItem with the same id if not throw exception
        listOrdItem.Remove(orderItemToDell);
        XMLTools.SaveListToXMLSerializer(listOrdItem, s_orderItems);
    }

    public IEnumerable<OrderItem?> GetAll(Func<OrderItem?, bool>? filtar = null)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        if (filtar != null)
            return listOrdItem.Where(x => filtar(x));
        return listOrdItem.Select(x => x);
    }

    public OrderItem GetById(int id)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        OrderItem newOrderItem = new OrderItem();//create a new  item
        newOrderItem = listOrdItem.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "orderItem");//if found the orderItem with the same id if not throw exception

        return newOrderItem;

    }

    public IEnumerable<OrderItem?> GetOrderItems(int id)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        var allTheOrder = listOrdItem.Where(x => x.Value.OrderId == id);
        return allTheOrder;
    }

    public OrderItem GetTheItem(int orderId, int itemId)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        OrderItem? theOrderItem = listOrdItem.Find(x => x?.ItemId == itemId && x?.OrderId == orderId) ?? throw new DO.DalMissingIdException(orderId, "orderItem");//throw exception;
        return (OrderItem)theOrderItem;

    }

    public void Update(OrderItem orderItem)
    {
        List<DO.OrderItem?> listOrdItem = XMLTools.LoadListFromXMLSerializer<DO.OrderItem>(s_orderItems);
        OrderItem orderItemToUpdate = new OrderItem();
        orderItemToUpdate = listOrdItem.Find(x => x?.ID == orderItem.ID) ?? throw new DO.DalMissingIdException(orderItem.ID, "orderItem");//if found the orderItem with the same id if not throw exception
        listOrdItem.Remove(orderItemToUpdate);//if found remove it
        listOrdItem.Add(orderItem);//add the right one
        XMLTools.SaveListToXMLSerializer(listOrdItem, s_orderItems);
    }
}
