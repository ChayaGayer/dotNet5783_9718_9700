using BlApi;
using System.Reflection.Emit;


namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = DalApi.Factory.Get();

    /// <summary>
    /// return the status of the order
    /// </summary>
    /// <param name="order"></param>
    /// <returns></returns>
    private BO.OrderStatus Status(DO.Order order)
    {
        return order.DeliveryDate != null ? BO.OrderStatus.Delivered : order.ShipDate != null ? BO.OrderStatus.Shipped : BO.OrderStatus.Ordered;


    }
    /// <summary>
    /// return the list of the orders 
    /// </summary>
    /// <returns></returns>
    public IEnumerable<BO.OrderForList?> GetListedOrders()
    {

        IEnumerable<DO.Order?> orders = dal.Order.GetAll();
        IEnumerable<DO.OrderItem?> orderItem = dal.OrderItem.GetAll();
        return from DO.Order item in orders
               let items=orderItem.Where(x=>x?.OrderId==item.ID)
               select new BO.OrderForList()
               {
                   ID = item.ID,
                   CustomerName = item.CustomerName,
                   Status = Status(item),
                   //Amount= items.Count(),
                   Amount=dal.OrderItem.GetAll(x=>x.Value.OrderId==item.ID).Sum(x=>x.Value.Amount),
                   TotelPrice = items.Sum(orderItem => orderItem.Value.Price * orderItem.Value.Amount)
               };


    }
    private IEnumerable<BO.OrderItem?> GetList(IEnumerable<DO.OrderItem?> listOrderItem)
    {
        return from DO.OrderItem item in listOrderItem
               select new BO.OrderItem()
               {
                   ID = item.ID,
                   ItemName = dal.Product.GetById(item.ItemId).ProductName,
                   Price = item.Price,
                   Amount = item.Amount,
                   TotalPrice = item.Price * item.Amount,

               };
    }
    /// <summary>
     /// Order details request
     /// </summary>
     /// <param name="orderID"></param>
     /// <returns></returns>
     /// <exception cref="BO.BlInCorrectException"></exception>
    public BO.Order RequestOrderDeta(int orderID)
    {
        if (orderID < 100000||orderID>999999)//if the id incorrect-throw
        {
            throw new BO.BlInCorrectException("Worng ID");
        }
        DO.Order order = dal.Order.GetById(orderID);//from the do to the bo build the order
        return new BO.Order()
        {
            ID = order.ID,
            CustomerName = order.CustomerName,
            CustomerAddress = order.CustomerAddress,
            CustomerEmail = order.CustomerEmail,
            Status = Status(order),
            DeliveryDate = order.DeliveryDate,
            OrderDate = order.OrderDate,
            ShipDate = order.ShipDate,
            Items = GetList(dal.OrderItem.GetAll().Where(x => x?.OrderId == order.ID)),
            TotalPrice = GetList(dal.OrderItem.GetAll().Where(x => x?.OrderId == order.ID)).Sum(x => x!.TotalPrice)//לבדוק את הסכום פה


        };
    }


    /// <summary>
    /// Order shipping update gets a id of order and return order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlIncorrectDatesException"></exception>
    public BO.Order UpdateSendOrder(int orderID)
    {
        DO.Order order = new DO.Order();

        try
        {
            order = dal.Order.GetById(orderID);//if exist
        }
        catch(DO.DalMissingIdException ex)//missing
        {
            throw new BO.BlMissingEntityException("Missing order", ex);
        }

        if (order.ShipDate != null)//check if the order already shipped
            throw new BO.BlIncorrectDatesException("The order already shipped");

        order.ShipDate = DateTime.Now;//update
        dal.Order.Update(order);

        return RequestOrderDeta(orderID);
    }

    /// <summary>
    /// Order delivery update gets the id of the order and return a order
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    /// <exception cref="BO.BlIncorrectDatesException"></exception>
    public BO.Order UpdateSupplyOrder(int orderID)
    {
        DO.Order order = new DO.Order();

        try
        {
            order = dal.Order.GetById(orderID);//if exist
        }
        catch(DO.DalMissingIdException ex)//missing
        {
            throw new BO.BlMissingEntityException("Missing order", ex);
        }

        if (order.DeliveryDate != null)//check if the order already deliverd
            throw new BO.BlIncorrectDatesException("The order already deliverd");

        order.DeliveryDate = DateTime.Now;//update

        try
        {
            dal.Order.Update(order);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Cant update order", ex);
        }

        return RequestOrderDeta(orderID);

    }

    /// <summary>
    /// OrderTracking receive an order id
   /// Check if an order exists(in the data layer)
///Return an instance of order tracking
    /// </summary>
    /// <param name="orderID"></param>
    /// <returns></returns>
    /// <exception cref="BO.BlMissingEntityException"></exception>
    public BO.OrderTracking OrderTracking(int orderID)
    {
        
        
            DO.Order order = new DO.Order();
        try
        {
            order = dal.Order.GetById(orderID);//if exist
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing order", ex);//throw if missing
        }

        return new BO.OrderTracking()//build and return order tracking
            {
                ID = order.ID,
                Status = Status(order),
                Tracking = new List<Tuple<DateTime?, string>>() { new Tuple<DateTime?, string>( order.OrderDate, "order date "),
                    new Tuple<DateTime?, string>( order.ShipDate, "ship date "),
                    new Tuple<DateTime?, string>( order.DeliveryDate, "delivery date ") }
            };
        }

        public BO.Order UpdateOrder(int orderID) { throw new NotImplementedException(); }//bonus

}
