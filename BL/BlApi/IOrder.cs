using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlApi;

public interface IOrder
{
   // IEnumerable<Order?> GetOrder();
    IEnumerable<OrderForList?> GetListedOrders();
    BO.Order  RequestOrderDeta(int orderID);
    BO.Order UpdateSendOrder(int orderID);
    BO.Order UpdateSupplyOrder(int orderID);
    BO.OrderTracking OrderTracking(int orderID);
    BO.Order UpdateOrder(int orderID);
}
