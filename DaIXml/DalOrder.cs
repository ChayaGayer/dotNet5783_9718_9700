﻿using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal;

internal class DalOrder : IOrder
{
    readonly string s_orders = "orders";
    public int Add(Order order)
    {
        List<DO.Order?> listOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
       // order.ID = DataSource.Config.NextOrderNumber;

        listOrd.Add(order);//add to the list
        XMLTools.SaveListToXMLSerializer(listOrd, s_orders);
        return order.ID;

    }

    public void Delete(int id)
    {
        List<DO.Order?> listOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);

        Order orderToDel = new Order();
        orderToDel = listOrd.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "order");//find the order with this id if not found throw exception
        listOrd.Remove(orderToDel);//delete rhe found order.if found
        XMLTools.SaveListToXMLSerializer(listOrd, s_orders);
    }

    public IEnumerable<Order?> GetAll(Func<Order?, bool>? filtar = null)
    {
        List<DO.Order?> listOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        if (filtar != null)
            return listOrd.Where(x => filtar(x));
        return listOrd.Select(x => x);

    }

    public Order GetById(int id)
    {
        List<DO.Order?> listOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Order order = new Order();//create a new order
        order = listOrd.Find(x => x?.ID == id) ?? throw new DO.DalMissingIdException(id, "order");//find the order with this id if not found throw exception

        return order;

    }

    public void Update(Order order)
    {
        List<DO.Order?> listOrd = XMLTools.LoadListFromXMLSerializer<DO.Order>(s_orders);
        Order orderToUpdate = new Order();//create a new order
        orderToUpdate = listOrd.Find(x => x?.ID == order.ID) ?? throw new DO.DalMissingIdException(order.ID, "order");//find the order with this id if not found throw exception
        listOrd.Remove(orderToUpdate);//if found remove it
        listOrd.Add(order);//add the new one
        XMLTools.SaveListToXMLSerializer(listOrd, s_orders);
    }

}
