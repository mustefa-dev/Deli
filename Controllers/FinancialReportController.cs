using Deli.DATA.DTOs;
using Deli.Services;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using QuestPDF.Fluent;

namespace Deli.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FinancialReportController : ControllerBase
    {
        private readonly IFinancialReportService _financialReportService;

        public FinancialReportController(IFinancialReportService financialReportService)
        {
            _financialReportService = financialReportService;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<IActionResult> GenerateFinancialReport()
        {
            try
            {
                var pdfPath = await _financialReportService.GenerateFinancialReportPdfAsync();
                return Ok(pdfPath);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }

}