using Deli.DATA.DTOs;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using QuestPDF.Helpers;

namespace Deli.Services
{
    public class FinancialReportDocument : IDocument
    {
        private FinancialReport Report { get; }

        public FinancialReportDocument(FinancialReport report)
        {
            Report = report;
        }

        public DocumentMetadata GetMetadata()
        {
            return new DocumentMetadata
            {
                Title = "Financial Report",
                Author = "Deli.Services",
                Subject = "Financial Analysis Report"
            };
        }

        public void Compose(IDocumentContainer container)
        {
            container
                .Page(page =>
                {
                    page.Size(PageSizes.A4); // Set the page size to A4
                    page.Margin(20);
                    page.Header().Element(ComposeHeader);
                    page.Content().Element(ComposeContent);
                    page.Footer().Element(ComposeFooter);
                });
        }

        private void ComposeHeader(IContainer container)
        {
            container
                .Background(Color.FromARGB(88, 255, 11, 41))
                .Padding(10)
                .Stack(stack =>
                {
                    stack.Spacing(5);
                    stack.Item().Row(row =>
                    {
                        row.ConstantColumn(20);
                        row.RelativeColumn().AlignRight().Text("\"////////////////////\"", TextStyle.Default.Size(20).Bold());
                    });
                    stack.Item().AlignRight().Text("القسم:\"////////////////////\"", TextStyle.Default.Size(14));
                    stack.Item().AlignRight().Text("agency_bas@scmt.gov.iq", TextStyle.Default.Size(12));
                });
        }

        private void ComposeContent(IContainer container)
        {
            container
                .PaddingVertical(10)
                .Decoration(decoration =>
                {
                    decoration.Content().Background(Colors.White).Stack(stack =>
                    {
                        stack.Spacing(10);

                        // Report Information
                        stack.Item().AlignRight().Text("معلومات التقرير", TextStyle.Default.Size(16).Bold());
                        stack.Item().Table(table =>
                        {
                            table.ColumnsDefinition(columns =>
                            {
                                columns.RelativeColumn();
                                columns.RelativeColumn();
                            });

                            table.Cell().Element(CellStyle).Text("إجمالي المبيعات");
                            table.Cell().Element(CellStyle).Text($"{Report.TotalSales:C}");
                            table.Cell().Element(CellStyle).Text("إجمالي الربح");
                            table.Cell().Element(CellStyle).Text($"{Report.TotalProfit:C}");
                            table.Cell().Element(CellStyle).Text("إجمالي العناصر المباعة");
                            table.Cell().Element(CellStyle).Text($"{Report.TotalItemsSold:N0}");
                        });

                        // Best Selling Items
                        if (Report.BestCategorySellingItem != null)
                            stack.Item().AlignRight().Text($"أفضل عنصر مبيعًا في الفئة: {Report.BestCategorySellingItem.Name} - {Report.BestCategorySellingItem.CategoryName}", TextStyle.Default.Size(14));

                        if (Report.BestInventorySellingItem != null)
                            stack.Item().AlignRight().Text($"أفضل عنصر مبيعًا في المخزون: {Report.BestInventorySellingItem.Name} - {Report.BestInventorySellingItem.InventoryName}", TextStyle.Default.Size(14));

                        if (Report.BestGovernorateSellingItem != null)
                            stack.Item().AlignRight().Text($"أفضل عنصر مبيعًا في المحافظة: {Report.BestGovernorateSellingItem.Name} - {Report.BestGovernorateSellingItem.GovernorateName}", TextStyle.Default.Size(14));
                    });
                });
        }

        private void ComposeFooter(IContainer container)
        {
            container
                .Background(Color.FromARGB(88, 255, 11, 41))
                .Padding(10)
                .Stack(stack =>
                {
                    stack.Spacing(5);
                    stack.Item().Row(row =>
                    {
                        row.RelativeColumn().AlignLeft().Text(text =>
                        {
                            text.CurrentPageNumber();
                            text.Span(" / ");
                            text.TotalPages();
                        });
                        row.ConstantColumn(20);
                        row.RelativeColumn().AlignRight().Text("////////////////////", TextStyle.Default.Size(10));
                    });
                });
        }

        public void SaveToPdf(string path)
        {
            Document.Create(container => Compose(container))
                .GeneratePdf(path);
        }

        private IContainer CellStyle(IContainer container)
        {
            return container.Padding(5);
        }
    }
}
