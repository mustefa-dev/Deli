
using System.Text.RegularExpressions;
using AutoMapper;
using Deli.Repository;
using Deli.Services;
using Deli.DATA.DTOs;
using Deli.DATA.DTOs.Item;
using Deli.Entities;
using Deli.Interface;

namespace Deli.Services;


public interface IItemServices
{
Task<(Item? item, string? error)> Create(ItemForm itemForm, string language);
Task<(ItemDto? item, string? error)> GetById(Guid userId,Guid id, string language);
Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(Guid userId,ItemFilter filter, string language);
Task<(ItemDto? item, string? error)> Update(Guid id , ItemUpdate itemUpdate, string language);
Task<(Item? item, string? error)> Delete(Guid id, string language);
Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId,Guid userId, string language);
Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId, string language);
Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId, string language);
Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId, string language);
Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate, string language);
Task<(Sale? sale, string? error)> EndSale(Guid itemid,string language);
Task<(Sale? sale, string? error)> DeleteScheduledSale(Guid saleId, string language);

Task<(List<ItemDto> items, string? error)> GetAllSoldItems(OrderStatisticsFilter filter, string language);
Task<(PriceRangeDto priceRangeDto, string? error)> GetPriceRange(string language);
}

public class ItemServices : IItemServices
{
private readonly IMapper _mapper;
private readonly IRepositoryWrapper _repositoryWrapper;

public ItemServices(
    IMapper mapper ,
    IRepositoryWrapper repositoryWrapper
    )
{
    _mapper = mapper;
    _repositoryWrapper = repositoryWrapper;
}
   
   
public async Task<(Item? item, string? error)> Create(ItemForm itemForm , string language)
{
    var item = _mapper.Map<Item>(itemForm);
    var hashtags = Regex.Matches(itemForm.Description, @"(?<=#)\w+")
        .Select(m => m.Value)
        .Distinct()
        .ToList();
    foreach (var hashtag in hashtags)
    {
        var tag = await _repositoryWrapper.Tag.Get(t => t.Name == hashtag);

        if (tag == null)
        {
            var newTag = new Tag { Name = hashtag };
            await _repositoryWrapper.Tag.Add(newTag);
            tag = newTag;
        }

        var itemTag = new ItemTag { ItemId = item.Id, TagId = tag.Id };
        item.ItemTags.Add(itemTag);
    }

    var category = await _repositoryWrapper.Category.Get(c => c.Id == itemForm.CategoryId);
    if (category == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Category not found", "لم يتم العثور على الفئة", language));
    var inventory = await _repositoryWrapper.Inventory.Get(i => i.Id == itemForm.InventoryId);
    if (inventory == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Inventory not found", "لم يتم العثور على المخزن", language));
    var result = await _repositoryWrapper.Item.Add(item);
    if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in Creating a Item", "خطأ في انشاء العنصر", language));
    return (result, null);
}
public async Task<(ItemDto? item, string? error)> GetById(Guid userId,Guid id, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(id);
        if (item == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        var date=DateTime.Now;
        var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == item.Id && date >= s.StartDate && date <= s.EndDate);
        if (sale != null)
        {
            item.SalePrice = sale.SalePrice;
            item.SalePercintage = sale.SalePercintage;
            item.SaleStartDate = sale.StartDate;
            item.SaleEndDate = sale.EndDate;
        }
        else
        {
            item.SalePrice = null;
            item.SalePercintage = null;
            item.SaleStartDate = null;
            item.SaleEndDate = null;
        }
       
        var itemDto = _mapper.Map<ItemDto>(item);
        if(itemDto.SalePrice!=null) itemDto.IsSale=true;
        else itemDto.IsSale=false;
        var wishlistedItem = await _repositoryWrapper.Wishlist.Get(l => l.UserId == userId && l.ItemsIds.Contains(itemDto.Id));
        if(userId==null) itemDto.IsWishlist = null;
        else itemDto.IsWishlist = wishlistedItem != null;
        var reviews = await _repositoryWrapper.Review.GetAll(r => r.ItemId == itemDto.Id);
        if (reviews.data != null && reviews.data.Count > 0)
        {
            itemDto.AvgRating = (float)reviews.data.Average(r => r.Rating);
        }
        else
        {
            itemDto.AvgRating = 0;
        }
        itemDto.ReviewsCount = reviews.data.Count;
        itemDto.Name = ErrorResponseException.GenerateLocalizedResponse(item.Name, item.ArName, language);
        itemDto.MainDetails = ErrorResponseException.GenerateLocalizedResponse(item.MainDetails, item.ArMainDetails, language);
        itemDto.Description = ErrorResponseException.GenerateLocalizedResponse(item.Description, item.ArDescription, language);
        var category = await _repositoryWrapper.Category.Get(c => c.Id == item.CategoryId);
        itemDto.CategoryName = ErrorResponseException.GenerateLocalizedResponse(category.Name, category.ArName, language);
        var inventory = await _repositoryWrapper.Inventory.Get(i => i.Id == item.InventoryId);
        itemDto.InventoryName = ErrorResponseException.GenerateLocalizedResponse(inventory.Name, inventory.ArName, language);
        var governorate = await _repositoryWrapper.Governorate.Get(g => g.Id == inventory.GovernorateId);
        itemDto.GovernorateName = ErrorResponseException.GenerateLocalizedResponse(governorate.Name, governorate.ArName, language);
        itemDto.IsAddedToCart= await _repositoryWrapper.Cart.Get(c => c.UserId == userId && c.ItemOrders.Any(i => i.ItemId == itemDto.Id)) != null;
        if (itemDto.IsAddedToCart == true)
        {
            var cart = await _repositoryWrapper.Cart.Get(c => c.UserId == userId);
                
            var itemOrder = await _repositoryWrapper.ItemOrder.Get(i => i.CartId == cart.Id && i.ItemId == itemDto.Id);
            itemDto.QuantityAddedToCart = itemOrder.Quantity;
        }

    
        return (itemDto, null);
        
    }
public async Task<(List<ItemDto> items, int? totalCount, string? error)> GetAll(Guid userId,ItemFilter filter, string language)
{
    var (item, totalCount) = await _repositoryWrapper.Item.GetAll<ItemDto>(
        x => (string.IsNullOrEmpty(filter.Name) || x.Name.Contains(filter.Name)) &&
             (string.IsNullOrEmpty(filter.ArName) || x.ArName.Contains(filter.ArName)) &&
             (string.IsNullOrEmpty(filter.TagName) || x.ItemTags.Any(t => t.Tag.Name == filter.TagName)) &&
             (filter.RefNumber == null || x.RefNumber == filter.RefNumber) &&
             (filter.CategoryId == null || x.CategoryId == filter.CategoryId) &&
             (filter.InventoryId == null || x.InventoryId == filter.InventoryId) &&
             (filter.Quantity == null || x.Quantity >= filter.Quantity));
        
        var result = new List<ItemDto>();
        foreach (var itemDto in item)
        {
            var tempPrice = itemDto.Price;
            var reviews = await _repositoryWrapper.Review.GetAll(r => r.ItemId == itemDto.Id);
            if (reviews.data != null && reviews.data.Count > 0)
            {
                itemDto.AvgRating = (float)reviews.data.Average(r => r.Rating);
            }
            else
            {
                itemDto.AvgRating = 0;
            }
            
            if(itemDto.AvgRating<filter.AvgRating)
            {
                continue;
            }
            var date = DateTime.Now;
            var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == itemDto.Id && date >= s.StartDate && date<= s.EndDate);
            if (sale != null)
            {
                itemDto.SalePrice = sale.SalePrice;
                tempPrice= sale.SalePrice;
                itemDto.SalePercintage = sale.SalePercintage;
                itemDto.SaleStartDate = sale.StartDate;
                itemDto.SaleEndDate = sale.EndDate;
                itemDto.IsSale = true;
                
            }
            else
            {
                itemDto.SalePrice = null;
                itemDto.SalePercintage = null;
                itemDto.SaleStartDate = null;
                itemDto.SaleEndDate = null;
                itemDto.IsSale = false;
            }
            if (filter.IsSale==true && itemDto.SalePrice==null)
            {
                continue;
         
            }
            if(filter.EndPrice!=null && (tempPrice>filter.EndPrice || tempPrice<filter.StartPrice))
            {
                continue;
            }
            
            var wishlistedItem = await _repositoryWrapper.Wishlist.Get(l => l.UserId == userId && l.ItemsIds.Contains(itemDto.Id));
            if(userId==null) itemDto.IsWishlist = null;
            else itemDto.IsWishlist = wishlistedItem != null;
          
            itemDto.ReviewsCount = reviews.data.Count;
            var originalItem = await _repositoryWrapper.Item.Get(x => x.Id == itemDto.Id);
            var category = await _repositoryWrapper.Category.Get(c => c.Id == originalItem.CategoryId);
            itemDto.CategoryName = ErrorResponseException.GenerateLocalizedResponse(category.Name, category.ArName, language);
            var inventory = await _repositoryWrapper.Inventory.Get(i => i.Id == originalItem.InventoryId);
            itemDto.InventoryName = ErrorResponseException.GenerateLocalizedResponse(inventory.Name, inventory.ArName, language);
            var governorate = await _repositoryWrapper.Governorate.Get(g => g.Id == inventory.GovernorateId);
            itemDto.GovernorateName = ErrorResponseException.GenerateLocalizedResponse(governorate.Name, governorate.ArName, language);
            itemDto.Name = ErrorResponseException.GenerateLocalizedResponse(originalItem.Name, originalItem.ArName, language);
            itemDto.MainDetails = ErrorResponseException.GenerateLocalizedResponse(originalItem.MainDetails, originalItem.ArMainDetails, language);
            itemDto.Description = ErrorResponseException.GenerateLocalizedResponse(originalItem.Description, originalItem.ArDescription, language);
            itemDto.IsAddedToCart= await _repositoryWrapper.Cart.Get(c => c.UserId == userId && c.ItemOrders.Any(i => i.ItemId == itemDto.Id)) != null;
            if (itemDto.IsAddedToCart == true)
            {
                var cart = await _repositoryWrapper.Cart.Get(c => c.UserId == userId);
                
                var itemOrder = await _repositoryWrapper.ItemOrder.Get(i => i.CartId == cart.Id && i.ItemId == itemDto.Id);
                itemDto.QuantityAddedToCart = itemOrder.Quantity;
            }
            
            result.Add(itemDto);
           
          
        }
        var query= result.AsQueryable();
        var (data,totalcount)=  _repositoryWrapper.Item.ExecuteItemDtoQuery(query, filter.PageNumber,filter.PageSize);
        return (data, totalcount, null);
        
    }

public async Task<(ItemDto? item, string? error)> Update(Guid id ,ItemUpdate itemUpdate, string language)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        _mapper.Map(itemUpdate, item);
        var category = await _repositoryWrapper.Category.Get(c => c.Id == itemUpdate.CategoryId);
        item.Category = category;
        var result = await _repositoryWrapper.Item.Update(item);
        if (result == null) return (null, error: ErrorResponseException.GenerateLocalizedResponse("Error in updating item", "خطأ في تحديث العنصر", language));
        return (_mapper.Map<ItemDto>(result), null);
   
    }

public async Task<(Item? item, string? error)> Delete(Guid id, string language)
    {
        var item = await _repositoryWrapper.Item.Get(x => x.Id == id);
        if (item == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        var result = await _repositoryWrapper.Item.SoftDelete(id);
        if (result == null) return (null, ErrorResponseException.GenerateLocalizedResponse("Error in deleting item", "خطأ في حذف العنصر", language));
        return (result, null);
    }
    public async Task<(Wishlist? wishlist, string? error)> AddOrRemoveItemToWishlist(Guid itemId, Guid userId, string language)
    {
        var item= await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var wishlist = await _repositoryWrapper.Wishlist.Get(w => w.UserId == userId);
        if (wishlist == null)
        {
            wishlist = new Wishlist
            {
                UserId = userId,
            };
            await _repositoryWrapper.Wishlist.Add(wishlist);
        }

        if (wishlist.ItemsIds.Contains(itemId))
        {
            wishlist.ItemsIds.Remove(itemId);
        }
        else
        {
            wishlist.ItemsIds.Add(itemId);
        }
        await _repositoryWrapper.Wishlist.Update(wishlist);
        return (wishlist, null);

    }

    public async Task<(ICollection<ItemDto>? items,int? count, string? error)> GetMyWishlist(Guid userId, string language)
    {
        var wishlist = await _repositoryWrapper.Wishlist.Get(w => w.UserId == userId);
        if (wishlist == null)
        {
            return (null,0, ErrorResponseException.GenerateLocalizedResponse("Wishlist is empty", "القائمة المفضلة فارغة", language));
        }
        var items = new List<ItemDto>();
        foreach (var itemId in wishlist.ItemsIds)
        {
            var item = await _repositoryWrapper.Item.GetById(itemId);
            var itemDto = _mapper.Map<ItemDto>(item);
            items.Add(itemDto);
        }
        var count = items.Count;
        
        return (items,count, null);
        
    }  
    public async Task<(Liked? liked, string? error)> AddOrRemoveItemToLiked(Guid itemId, Guid userId, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var liked = await _repositoryWrapper.Liked.Get(l => l.UserId == userId);
        if (liked == null)
        {
            liked = new Liked
            {
                UserId = userId,
            };
            await _repositoryWrapper.Liked.Add(liked);
        }

        if (liked.ItemsIds.Contains(itemId))
        {
            liked.ItemsIds.Remove(itemId);
        }
        else
        {
            liked.ItemsIds.Add(itemId);
        }
        await _repositoryWrapper.Liked.Update(liked);
        return (liked, null);
    }

    public async Task<(ICollection<ItemDto>? items, int? count, string? error)> GetMyLikedItems(Guid userId, string language)
    {
        var liked = await _repositoryWrapper.Liked.Get(l => l.UserId == userId);
        if (liked == null)
        {
            return (null, 0, ErrorResponseException.GenerateLocalizedResponse("Liked list is empty", "قائمة الإعجاب فارغة", language));
        }
        var items = new List<ItemDto>();
        foreach (var itemId in liked.ItemsIds)
        {
            var item = await _repositoryWrapper.Item.GetById(itemId);
            var itemDto = _mapper.Map<ItemDto>(item);
            items.Add(itemDto);
        }
        var count = items.Count;

        return (items, count, null);
    }
    public async Task<(Sale? sale, string? error)> AddSaleToItem(Guid itemId, double salePrice, DateTime startDate, DateTime endDate, string language)
    {
        var item = await _repositoryWrapper.Item.GetById(itemId);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var saleExist = await _repositoryWrapper.Sale.Get(s => s.ItemId == itemId && 
                                                               ((s.StartDate <= startDate && s.EndDate >= startDate) || 
                                                                (s.StartDate <= endDate && s.EndDate >= endDate) || 
                                                                (s.StartDate >= startDate && s.EndDate <= endDate)));
        if (saleExist != null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item already has a sale scheduled during this period", "العنصر لديه تخفيض مجدول خلال هذه الفترة", language));
        }


        var sale = new Sale
        {
            ItemId = itemId,
            SalePrice = salePrice,
            StartDate = startDate,
            EndDate = endDate,
            SalePercintage = (item.Price.GetValueOrDefault() - salePrice) / item.Price.GetValueOrDefault() * 100,
        };

        await _repositoryWrapper.Sale.Add(sale);
        return (sale, null);
    }

    public async Task<(Sale? sale, string? error)> EndSale(Guid itemid, string language)
    {    
        var item = await _repositoryWrapper.Item.GetById(itemid);
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        var date = DateTime.Now;
        var sale = await _repositoryWrapper.Sale.Get(s => s.ItemId == item.Id && date >= s.StartDate && date <= s.EndDate );
        if (sale == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Sale either doesn't exist or it is not active", "لم يتم العثور على التخفيض او التخفيض غير نشط", language));
        }
        item.SaleId = null;
        item.SalePrice = null;
        item.SaleStartDate = null;
        item.SaleEndDate = null;
        item.SalePercintage = null;
        await _repositoryWrapper.Item.Update(item);
        sale.EndDate = DateTime.Now;
        await _repositoryWrapper.Sale.Update(sale);
        return (sale, null);
    }
    public async Task<(Sale? sale, string? error)> DeleteScheduledSale(Guid saleId, string language)
    {
        var sale = await _repositoryWrapper.Sale.Get(s => s.Id == saleId);
        if (sale == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Sale not found", "لم يتم العثور على التخفيض", language));
        }
        var date = DateTime.Now;
        if(sale.EndDate <date)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Sale already ended", "انتهى التخفيض بالفعل", language));
        }
        if(sale.StartDate < date)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Sale already started", "بدأ التخفيض بالفعل", language));
        }
        
        var item = await _repositoryWrapper.Item.GetById(sale.ItemId.GetValueOrDefault());
        if (item == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Item not found", "لم يتم العثور على العنصر", language));
        }
        item.SaleId = null;
        item.SalePrice = null;
        item.SaleStartDate = null;
        item.SaleEndDate = null;
        item.SalePercintage = null;
        await _repositoryWrapper.Item.Update(item);
        var result = await _repositoryWrapper.Sale.SoftDelete(saleId);
        if (result == null)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("Error in deleting sale", "خطأ في حذف التخفيض", language));
        }
        return (result, null);
    }
    
   
    

    public async Task<(List<ItemDto> items, string? error)> GetAllSoldItems(OrderStatisticsFilter filter, string language)
{
    var orderItems = await _repositoryWrapper.OrderItem.GetAll();
    if (orderItems.data == null || orderItems.data.Count == 0)
    {
        return (null, ErrorResponseException.GenerateLocalizedResponse("No sold items found", "لم يتم العثور على عناصر مباعة", language));
    }

    var soldItems = new List<ItemDto>();
    foreach (var orderItem in orderItems.data)
    {
        var item = await _repositoryWrapper.Item.GetById(orderItem.ItemId);
        if (item != null &&
            (filter.InventoryId == null || item.InventoryId == filter.InventoryId) &&
            (filter.CategoryId == null || item.CategoryId == filter.CategoryId))
        {
            var itemDto = _mapper.Map<ItemDto>(item);
            soldItems.Add(itemDto);
        }
    }
    

    return (soldItems, null);
}
    public async Task<(PriceRangeDto priceRangeDto, string? error)> GetPriceRange(string language)
    {
        var items = await _repositoryWrapper.Item.GetAll();
        if (items.data == null || items.totalCount == 0)
        {
            return (null, ErrorResponseException.GenerateLocalizedResponse("No items found", "لم يتم العثور على عناصر", language));
        }

        var lowestPrice = items.data.Min(i => i.Price);
        var highestPrice = items.data.Max(i => i.Price);
        var pricerangedto = new PriceRangeDto
        {
            LowestPrice = lowestPrice,
            HighestPrice = highestPrice
        };

        return (pricerangedto,null);
    }
    
}