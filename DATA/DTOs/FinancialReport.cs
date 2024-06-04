using Deli.DATA.DTOs.Item;

namespace Deli.DATA.DTOs;

public class FinancialReport
{
    public decimal TotalSales { get; set; }
    public decimal TotalProfit { get; set; }
    public int TotalItemsSold { get; set; }
    public ItemDto BestCategorySellingItem { get; set; }
    public ItemDto BestInventorySellingItem { get; set; }
    public ItemDto BestGovernorateSellingItem { get; set; }
}