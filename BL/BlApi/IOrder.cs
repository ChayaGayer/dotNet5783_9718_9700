using BO;
using static BlImplementation.Order;

namespace BlApi;
/// <summary>
/// the order interface
/// </summary>
public interface IOrder
{
   /// <summary>
   /// return the list of the orders
   /// </summary>
   /// <returns></returns>
    IEnumerable<OrderForList?> GetListedOrders();
    /// <summary>
    /// get order id and return it data
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
     Order  RequestOrderDeta(int orderID);
    /// <summary>
    /// get id and update status from orderd to shiped
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    Order UpdateSendOrder(int orderID);
    /// <summary>
    /// get id and update status from shiped to deliverd
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    Order UpdateSupplyOrder(int orderID);
    /// <summary>
    /// get order id and return order tracking
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
   OrderTracking OrderTracking(int orderID);
    
}
