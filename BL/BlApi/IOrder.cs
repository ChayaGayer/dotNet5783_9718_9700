﻿using BO;

namespace BlApi;
/// <summary>
/// the order interface
/// </summary>
public interface IOrder
{
   
    IEnumerable<OrderForList?> GetListedOrders();
    BO.Order  RequestOrderDeta(int orderID);
    BO.Order UpdateSendOrder(int orderID);
    BO.Order UpdateSupplyOrder(int orderID);
    BO.OrderTracking OrderTracking(int orderID);
    BO.Order UpdateOrder(int orderID);
}
