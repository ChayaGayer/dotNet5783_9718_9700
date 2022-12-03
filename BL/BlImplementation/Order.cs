using BlApi;


namespace BlImplementation;

internal class Order : IOrder
{
    DalApi.IDal dal = new Dal.DalList();
    private BO.OrderStatus Status(DO.Order order)
    {
        return order.DeliveryDate != null ? BO.OrderStatus.Delivered : order.ShipDate != null ? BO.OrderStatus.Shipped : BO.OrderStatus.Ordered;


    }
    public IEnumerable<BO.OrderForList?> GetListedOrders()
    {

        IEnumerable<DO.Order?> orders = dal.Order.GetAll();
        IEnumerable<DO.OrderItem?> orderItem = dal.OrderItem.GetAll();
        return from DO.Order item in orders
               let items=orderItem.Where(orderItem=>orderItem.Value.OrderId==item.ID)
               select new BO.OrderForList()
               {
                   ID = item.ID,
                   CustomerName = item.CustomerName,
                   Status = Status(item),
                   Amount= items.Count(),
                   TotelPrice = items.Sum(items => items.Value.Price * items.Value.Amount)
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
    public BO.Order RequestOrderDeta(int orderID)
    {
        if (orderID < 100000||orderID>999999)
        {
            throw new BO.BlInCorrectException("Worng ID");
        }
        DO.Order order = dal.Order.GetById(orderID);
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
            Items = GetList(dal.OrderItem.GetAll().Where(x => x.Value.OrderId == order.ID)),
            TotalPrice = GetList(dal.OrderItem.GetAll().Where(x => x.Value.OrderId == order.ID)).Sum(x => x.TotalPrice)//לבדוק את הסכום פה


        };
    }



    public BO.Order UpdateSendOrder(int orderID)
    {
        DO.Order order = new DO.Order();

        try
        {
            order = dal.Order.GetById(orderID);
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing order", ex);
        }

        if (order.ShipDate != null)
            throw new BO.BlIncorrectDatesException("The order already shipped");

        order.ShipDate = DateTime.Now;
        dal.Order.Update(order);

        return RequestOrderDeta(orderID);
    }


    public BO.Order UpdateSupplyOrder(int orderID)
    {
        DO.Order order = new DO.Order();

        try
        {
            order = dal.Order.GetById(orderID);
        }
        catch(DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing order", ex);
        }

        if (order.DeliveryDate != null)
            throw new BO.BlIncorrectDatesException("The order already deliverd");

        order.DeliveryDate = DateTime.Now;

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


    public BO.OrderTracking OrderTracking(int orderID)
    {
        
        
            DO.Order order = new DO.Order();
        try
        {
            order = dal.Order.GetById(orderID);
        }
        catch (DO.DalMissingIdException ex)
        {
            throw new BO.BlMissingEntityException("Missing order", ex);
        }

        return new BO.OrderTracking()
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
