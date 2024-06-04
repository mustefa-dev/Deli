using Deli.DATA.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Deli.DATA.DTOs.Item;
using Deli.Interface;

namespace Deli.Services
{
    public interface IFinancialReportService
    {
        Task<FinancialReport> GenerateFinancialReportAsync();
        Task<string> GenerateFinancialReportPdfAsync();
    }

    public class FinancialReportService : IFinancialReportService
    {
        private readonly IRepositoryWrapper _repositoryWrapper;
        private readonly IMapper _mapper;

        public FinancialReportService(IRepositoryWrapper repositoryWrapper, IMapper mapper)
        {
            _repositoryWrapper = repositoryWrapper;
            _mapper = mapper;
        }

        public async Task<FinancialReport> GenerateFinancialReportAsync()
        {
            // Step 1: Gather Necessary Data
            var allItems = (await _repositoryWrapper.Item.GetAll()).data.ToList();
            var allOrderItems = (await _repositoryWrapper.OrderItem.GetAll()).data.ToList();

            // Fetch Category, Inventory, and Governorate from the repository
            var allCategories = (await _repositoryWrapper.Category.GetAll()).data.ToList();
            var allInventories = (await _repositoryWrapper.Inventory.GetAll()).data.ToList();
            var allGovernorates = (await _repositoryWrapper.Governorate.GetAll()).data.ToList();

            // Step 2: Organize the Data
            var salesTransactions = allOrderItems.GroupBy(orderItem => orderItem.ItemId)
                                                  .ToDictionary(group => group.Key, group => group.ToList());

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

        public async Task<string> GenerateFinancialReportPdfAsync()
        {
            var financialReport = await GenerateFinancialReportAsync();
            var pdfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FinancialReport.pdf");

            var document = new FinancialReportDocument(financialReport);
            document.SaveToPdf(pdfPath);

            return pdfPath;
        }

        





    }
}
