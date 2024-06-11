using AutoMapper;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Helpers.OneSignal;
using Deli.Interface;
using Microsoft.EntityFrameworkCore;

namespace Deli.Services;


public interface IOrderServices
{
    Task<(Order? order, string? error)> Create(OrderForm orderForm , Guid UserId, string language);
    Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter, string language);
    Task<(OrderDto? order, string? error)> GetById(Guid id, string language);
    Task<(Order? order, string? error)> Update(Guid id , OrderUpdate orderUpdate, string language);
    Task<(string? done, string? error)> Approve(Guid id, Guid userId, string language);
    Task<(string? done, string? error)> Delivered(Guid id, Guid userId, string language);
    Task<(string? done, string? error)> Cancel(Guid id, Guid userId, string language);
    Task<(string? done, string? error)> Reject(Guid id, Guid userId, string language);
    Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm, string language);

    Task<(Order? order, string? error)> Delete(Guid id, string language);
    Task<(List<OrderDto>order ,int? totalCount, string? error)> GetMyOrders(Guid userId, string language);
    Task<OrderStatisticsDto> GetOrderStatistics(OrderStatisticsFilter filter, string language);
    Task<FinancialReport> CreateFinancialReport();

}

public class OrderServices : IOrderServices
{
    private readonly IMapper _mapper;
    private readonly IRepositoryWrapper _repositoryWrapper;

    public OrderServices(
        IMapper mapper,
        IRepositoryWrapper repositoryWrapper
    )
    {
        _mapper = mapper;
        _repositoryWrapper = repositoryWrapper;
    }


    public async Task<(Order? order, string? error)> Create(OrderForm orderForm, Guid userId, string language)
    {
        var order = _mapper.Map<Order>(orderForm);
        order.UserId = userId;
        order.DateOfAccepted = null;

        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null)
            return (null,ErrorResponseException.GenerateErrorResponse("User not found", "المستخدم غير موجود", language));
        order.OrderStatus = OrderStatus.Pending;
        order.AddressId = user.AddressId;

        var addedOrder = await _repositoryWrapper.Order.Add(order);
        if (addedOrder == null)
            return (null,ErrorResponseException.GenerateErrorResponse("Error in Creating a Order", "خطأ في انشاء الطلب", language));

        decimal totalPrice = 0;

        foreach (var orderItemForm in orderForm.ItemId)
        {
            var item = await _repositoryWrapper.Item.Get(x => x.Id == orderItemForm.ItemId);
            var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == item.Id && orderForm.OrderDate >= s.StartDate && orderForm.OrderDate <= s.EndDate);
            if (sale != null)
            {
                item.Price = sale.SalePrice;
            }
            if (item == null)
                return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "العنصر غير موجود", language));

            var orderItem = new OrderItem
            {
                OrderId = addedOrder.Id,
                ItemId = orderItemForm.ItemId,
                Quantity = orderItemForm.Quantity 
            }; 
            orderItem.ItemPrice =  (double)(item.Price ?? 0.0);

            totalPrice += orderItem.Quantity * (decimal)(item.Price ?? 0.0);
            await _repositoryWrapper.OrderItem.Add(orderItem);
        }

        order.TotalPrice = totalPrice;

        var orderItems = await _repositoryWrapper.OrderItem.GetAll(x => x.OrderId == addedOrder.Id);
        foreach (var orderItem in orderItems.data)
        {
            var item = await _repositoryWrapper.Item.Get(x => x.Id == orderItem.ItemId);
            if (item == null)
                return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "العنصر غير موجود", language));
            item.Quantity -= orderItem.Quantity;
            await _repositoryWrapper.Item.Update(item);
        }

        return (order, null);
    }


    public async Task<(List<OrderDto> orders, int? totalCount, string? error)> GetAll(OrderFilter filter, string language)
    {
        var orders = await _repositoryWrapper.Order.GetAll<OrderDto>(
            x =>
            (
                (filter.OrderDate == null || x.OrderDate == filter.OrderDate) &&
                (filter.OrderStatus == null || x.OrderStatus == filter.OrderStatus) &&
                (filter.Note == null || x.Note.Contains(filter.Note)) &&
                (filter.UserId == filter.UserId) &&
                (filter.Rating == null || x.Rating == filter.Rating) &&
                (filter.DateOfAccepted == null || x.DateOfAccepted == filter.DateOfAccepted) &&
                (filter.DateOfCanceled == null || x.DateOfCanceled == filter.DateOfCanceled) &&
                (filter.DateOfDelivered == null || x.DateOfDelivered == filter.DateOfDelivered) &&
                (filter.AddressId == null || x.AddressId == filter.AddressId)
            ),
            filter.PageNumber, filter.PageSize);
        return (orders.data, orders.totalCount, null);
    }

    public async Task<(OrderDto? order, string? error)> GetById(Guid id, string language)
    {
        var order = await _repositoryWrapper.Order.GetAll<OrderDto>(x => x.Id == id);
        if (order.data == null)
            return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        return (order.data.FirstOrDefault(), null);
    }

    public async Task<(Order? order, string? error)> Update(Guid id, OrderUpdate orderUpdate, string language)
    {
        throw new NotImplementedException();

    }

    public async Task<(string? done, string? error)> Approve(Guid id, Guid userId, string language)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id,
            i => i.Include(s => s.Address));

        if (order == null) return (null, "الطلب غير موجود");
        if (order.OrderStatus != OrderStatus.Pending) return (null, ErrorResponseException.GenerateErrorResponse("Order is not pending", "الطلب ليس معلق", language));


        if (order.OrderItem != null)
        {
            var notificationForm = new Notification
            {
                Title = "تمت الموافقة على الطلب",
                Description = "تمت الموافقة على الطلب",
                UserId = order.UserId,
                NotifyFor = order.UserId.ToString(),
                Date = DateTime.UtcNow,

            };
            await _repositoryWrapper.Notification.Add(notificationForm);
        }

        order.OrderStatus = OrderStatus.Accepted;
        order.DateOfAccepted = DateTime.UtcNow;
        

        var orderCars = await _repositoryWrapper.OrderItem.GetAll(x => x.OrderId == id);
        
        foreach (var orderCar in orderCars.data)
        {
            var item = await _repositoryWrapper.Item.Get(x => x.Id == orderCar.ItemId);
            if (item == null) return (null, ErrorResponseException.GenerateErrorResponse("Item not found", "العنصر غير موجود", language));
            await _repositoryWrapper.Item.Update(item);
        }


        var update = await _repositoryWrapper.Order.Update(order);
        if (update == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating order", "خطأ في تحديث الطلب", language));

        var oneSignalNotification = new Notification
        {
            Title = "تمت الموافقة على الطلب",
            Description = "تمت الموافقة على الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        return ("تمت الموافقة على الطلب", null);
    }

    public async Task<(string? done, string? error)> Delivered(Guid id, Guid userId, string language)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id,
            i => i.Include(a => a.Address));
        if (order == null) return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        order.OrderStatus = OrderStatus.Delivered;
        order.DateOfDelivered = DateTime.UtcNow;
        var update = await _repositoryWrapper.Order.Update(order);
        var notificationForm = new Notification
        {
            Title = "تم تسليم الطلب",
            Description = "تم تسليم الطلب",
            UserId = order.UserId,
        };
        await _repositoryWrapper.Notification.Add(notificationForm);
        OneSignal.SendNoitications(notificationForm, userId.ToString());
        var oneSignalNotification = new Notification
        {
            Title = "تم تسليم الطلب",
            Description = "تم تسليم الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        if (update == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating order", "خطأ في تحديث الطلب", language));
        return ("تم تسليم الطلب", null);
    }

    public async Task<(string? done, string? error)> Cancel(Guid id, Guid userId, string language)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        order.OrderStatus = OrderStatus.Canceled;
        order.DateOfCanceled = DateTime.UtcNow;
        var orderCars = await _repositoryWrapper.OrderItem.GetAll(x => x.OrderId == id);
        foreach (var orderCar in orderCars.data)
        {
            
            if (order.UserId != userId)
                return (null, ErrorResponseException.GenerateErrorResponse("You are not authorized to cancel this order", "غير مصرح لك بالغاء هذا الطلب", language));
            var car = await _repositoryWrapper.Item.Get(x => x.Id == orderCar.ItemId);
            await _repositoryWrapper.Item.Update(car);
        }

        var update = await _repositoryWrapper.Order.Update(order);
        var notificationForm = new Notification
        {
            Title = "تم الغاء الطلب",
            Description = "تم الغاء الطلب",
            UserId = order.UserId,
        };
        await _repositoryWrapper.Notification.Add(notificationForm);
        OneSignal.SendNoitications(notificationForm, userId.ToString());
        var oneSignalNotification = new Notification
        {
            Title = "تم الغاء الطلب",
            Description = "تم الغاء الطلب",
        };
        OneSignal.SendNoitications(oneSignalNotification, userId.ToString());

        if (update == null) return (null,ErrorResponseException.GenerateErrorResponse("Error in updating order", "خطأ في تحديث الطلب", language));
        return ("تم الغاء الطلب", null);

    }

    public async Task<(string? done, string? error)> Reject(Guid id, Guid userId, string language)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        order.OrderStatus = OrderStatus.Rejected;
        var orderCars = await _repositoryWrapper.OrderItem.GetAll(x => x.OrderId == id);

        var update = await _repositoryWrapper.Order.Update(order);
            if (update == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating order", "خطأ في تحديث الطلب", language));
            return ("تم رفض الطلب", null);

        }

public async Task<(string? done, string? error)> Rating(Guid id, Guid userId, RatingOrderForm ratingOrderForm, string language)
    {
        var order =await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        order.Rating = ratingOrderForm.Rating;
        var update = await _repositoryWrapper.Order.Update(order);
        if (update == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in updating order", "خطأ في تحديث الطلب", language));
        return ("تم تقييم الطلب", null);
        
    }

    public async Task<(Order? order, string? error)> Delete(Guid id, string language)
    {
        var order = await _repositoryWrapper.Order.Get(x => x.Id == id);
        if (order == null) return (null, ErrorResponseException.GenerateErrorResponse("Order not found", "الطلب غير موجود", language));
        var delete = await _repositoryWrapper.Order.SoftDelete(id);
        if (delete == null) return (null, ErrorResponseException.GenerateErrorResponse("Error in deleting order", "خطأ في حذف الطلب", language));
        return (order, null);
    }
    public async Task<(List<OrderDto> order,int? totalCount,string?error)>GetMyOrders(Guid userId, string language)
    {
        var user = await _repositoryWrapper.User.Get(x => x.Id == userId);
        if (user == null)
            return (null, null, ErrorResponseException.GenerateErrorResponse("User not found", "المستخدم غير موجود", language));
        var orders = await _repositoryWrapper.Order.GetAll<OrderDto>(x => x.UserId == userId);
        return (orders.data, orders.totalCount, null);
    }
    public async Task<OrderStatisticsDto> GetOrderStatistics(OrderStatisticsFilter filter, string language)
    {
        var allOrdersResult = await _repositoryWrapper.Order.GetAll();
        var allOrders = allOrdersResult.data;

        if (filter.InventoryId != null)
        {
            allOrders = allOrders.Where(o => o.OrderItem.Any(oi => oi.Item.InventoryId == filter.InventoryId)).ToList();
        }

        if (filter.CategoryId != null)
        {
            allOrders = allOrders.Where(o => o.OrderItem.Any(oi => oi.Item.CategoryId == filter.CategoryId)).ToList();
        }

        if (filter.OrderStatus != null)
        {
            allOrders = allOrders.Where(o => o.OrderStatus == filter.OrderStatus).ToList();
        }

        if (filter.StartDate != null)
        {
            allOrders = allOrders.Where(o => o.OrderDate >= filter.StartDate).ToList();
        }

        if (filter.EndDate != null)
        {
            allOrders = allOrders.Where(o => o.OrderDate <= filter.EndDate).ToList();
        }

        var totalOrders = allOrders.Count;
        var acceptedOrders = allOrders.Count(o => o.OrderStatus == OrderStatus.Accepted);
        var rejectedOrders = allOrders.Count(o => o.OrderStatus == OrderStatus.Rejected);
        var pendingOrders = allOrders.Count(o => o.OrderStatus == OrderStatus.Pending);

        return new OrderStatisticsDto
        {
            TotalOrders = totalOrders,
            AcceptedOrders = acceptedOrders,
            RejectedOrders = rejectedOrders,
            PendingOrders = pendingOrders,
        };
    }
    public async Task<FinancialReport> CreateFinancialReport()
{
    // Step 1: Gather Necessary Data
    var allItems = (await _repositoryWrapper.Item.GetAll()).data.ToList();
    var allOrderItems = (await _repositoryWrapper.OrderItem.GetAll()).data.ToList();

    // Fetch Category, Inventory, and Governorate from the repository
    var allCategories = (await _repositoryWrapper.Category.GetAll()).data.ToList();
    var allInventories = (await _repositoryWrapper.Inventory.GetAll()).data.ToList();
    var allGovernorates = (await _repositoryWrapper.Governorate.GetAll()).data.ToList();

    // Step 2: Organize the Data
    var salesTransactions = allOrderItems.GroupBy(orderItem => orderItem.ItemId).ToDictionary(group => group.Key, group => group.ToList());

    // Step 3: Calculate Key Metrics
    decimal totalSales = 0;
    decimal totalProfit = 0;
    int totalItemsSold = 0;

    // Initialize variables to store best selling items for each category, inventory, and government
    ItemDto bestSellingItemByCategory = null;
    ItemDto bestSellingItemByInventory = null;
    ItemDto bestSellingItemByGovernorate = null;

    foreach (var item in allItems)
    {
        int itemSales = salesTransactions.TryGetValue(item.Id, out var transactions) ? transactions.Sum(t => t.Quantity) : 0;
        totalItemsSold += itemSales;

        // Update best selling item for the item's category
        if (item.Category != null && (bestSellingItemByCategory == null || itemSales >= bestSellingItemByCategory.Quantity))
        {
            bestSellingItemByCategory = _mapper.Map<ItemDto>(item);
            bestSellingItemByCategory.CategoryName = item.Category.Name;
        }

        // Update best selling item for the item's inventory
        if (item.Inventory != null && (bestSellingItemByInventory == null || itemSales >= bestSellingItemByInventory.Quantity))
        {
            bestSellingItemByInventory = _mapper.Map<ItemDto>(item);
            bestSellingItemByInventory.InventoryName = item.Inventory.Name;
        }

        // Update best selling item for the item's government
        if (item.Inventory != null && item.Inventory.Governorate != null && (bestSellingItemByGovernorate == null || itemSales >= bestSellingItemByGovernorate.Quantity))
        {
            bestSellingItemByGovernorate = _mapper.Map<ItemDto>(item);
            bestSellingItemByGovernorate.GovernorateName = item.Inventory.Governorate.Name;
        }

        if (transactions != null)
        {
            foreach (var transaction in transactions)
            {
                totalSales += transaction.Quantity * (decimal)item.Price.GetValueOrDefault();
                totalProfit += transaction.Quantity * ((decimal)item.Price.GetValueOrDefault() - (decimal)item.Price.GetValueOrDefault());
            }
        }
    }

    // Step 4: Structure the Report
    var report = new FinancialReport
    {
        TotalSales = totalSales,
        TotalProfit = totalProfit,
        TotalItemsSold = totalItemsSold,
        BestCategorySellingItem = bestSellingItemByCategory!,
        BestInventorySellingItem = bestSellingItemByInventory!,
        BestGovernorateSellingItem = bestSellingItemByGovernorate!,
    };

    return report;
}
 

}