﻿using BO;
using static BlImplementation.Order;

namespace BlApi;
/// <summary>
/// the order interface
/// </summary>
public interface IOrder
{
   
    IEnumerable<OrderForList?> GetListedOrders();
    //IEnumerable<BO.OrderItem?> GetList(IEnumerable<DO.OrderItem?> listOrderItem);
   //IEnumerable<StatisticsOrders> GetStatisticsOrders();
    
     Order  RequestOrderDeta(int orderID);
    Order UpdateSendOrder(int orderID);
    Order UpdateSupplyOrder(int orderID);
   OrderTracking OrderTracking(int orderID);
    Order UpdateOrder(int orderID);
}
