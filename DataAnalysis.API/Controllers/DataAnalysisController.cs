using DataAnalysis.Application;
using FinancialAnalysis.API.Logic;
using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Net;

namespace DataAnalysis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataAnalysisController : ControllerBase
    {

        private readonly ILogger<DataAnalysisController> _logger;
        private readonly IStockAnalysisLogic _stockAnalysisLogic;
        public DataAnalysisController(ILogger<DataAnalysisController> logger, IStockAnalysisLogic stockAnalysisLogic)
        {
            _stockAnalysisLogic = stockAnalysisLogic;
            _logger = logger;
        }

        [HttpGet()]
        public async Task<IActionResult> Geat()
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
        [HttpGet("returns")]
        public IActionResult CalculateReturns()
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                _stockAnalysisLogic.CalculateReturns(stocks);
                return Ok(stocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("volatility")]
        public IActionResult CalculateVolatility()
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                Dictionary<string, double> volatilityData = _stockAnalysisLogic.CalculateVolatility(stocks);
                return Ok(volatilityData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("correlations")]
        public IActionResult CalculateCorrelations()
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                Dictionary<string, Dictionary<string, double>> correlationData = _stockAnalysisLogic.CalculateCorrelations(stocks);
                return Ok(correlationData);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("filter")]
        public IActionResult FilterByTimePeriod(DateTime startDate, DateTime endDate)
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                List<Stock> filteredStocks = _stockAnalysisLogic.FilterByTimePeriod(stocks, startDate, endDate);
                return Ok(filteredStocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("filter/{ticker}")]
        public IActionResult FilterByAsset(string ticker)
        {
            try
            {
                List<Stock> stocks = _stockAnalysisLogic.GetStockData();
                List<Stock> filteredStocks = _stockAnalysisLogic.FilterByAsset(stocks, ticker);
                return Ok(filteredStocks);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}

