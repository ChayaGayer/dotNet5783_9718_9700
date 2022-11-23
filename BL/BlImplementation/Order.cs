using BlApi;
using BO;
namespace BlImplementation;

internal class Order
{
    IEnumerable<OrderForList?> GetListedOrders() { throw new NotImplementedException(); }
    BO.Order RequestOrderDeta(int orderID) { throw new NotImplementedException(); }
    BO.Order UpdateSendOrder(int orderID) { throw new NotImplementedException(); }
    BO.Order UpdateSupplyOrder(int orderID) { throw new NotImplementedException(); }
    BO.OrderTracking OrderTracking(int orderID) { throw new NotImplementedException(); }
    BO.Order UpdateOrder(int orderID) { throw new NotImplementedException(); }
}
