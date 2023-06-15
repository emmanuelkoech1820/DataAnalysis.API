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

        private const string DataUrl = "https://example.com/financial-data";
        private readonly ILogger<DataAnalysisController> _logger;
        //private string filePath = Path.Combine(AppContext.BaseDirectory, "stock_prices_latest.xlsx");
        string filePath = @"C:\Users\manu\source\repos\DataAnalysis.API\DataAnalysis.API\stock_prices_latest.xlsx";

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
            //Dictionary<string, List<List<string>>> sheetColumns = new Dictionary<string, List<List<string>>>();

            //// Set the LicenseContext
            //ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Assuming you are using EPPlus in a non-commercial project

            //FileInfo fileInfo = new FileInfo(filePath);

            //using (ExcelPackage package = new ExcelPackage(fileInfo))
            //{
            //    ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Fetch the first worksheet

            //    List<List<string>> columnValues = new List<List<string>>();

            //    int rowCount = worksheet.Dimension.Rows;
            //    int columnCount = worksheet.Dimension.Columns;

            //    for (int col = 1; col <= columnCount; col++)
            //    {
            //        List<string> columnData = new List<string>();

            //        for (int row = 1; row <= rowCount; row++)
            //        {
            //            var cellValue = worksheet.Cells[row, col].Value?.ToString();
            //            columnData.Add(cellValue);
            //        }

            //        columnValues.Add(columnData);
            //    }

            //    sheetColumns.Add(worksheet.Name, columnValues);
            //}
            //    return Ok(sheetColumns);
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

        //[HttpGet("volatility")]
        //public IActionResult CalculateVolatility()
        //{
        //    try
        //    {
        //        List<Stock> stocks = _stockAnalysisLogic.GetStockData();
        //        Dictionary<string, double> volatilityData = _stockAnalysisLogic.CalculateVolatility(stocks);
        //        return Ok(volatilityData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

        //[HttpGet("correlations")]
        //public IActionResult CalculateCorrelations()
        //{
        //    try
        //    {
        //        List<Stock> stocks = _stockAnalysisLogic.GetStockData();
        //        Dictionary<string, Dictionary<string, double>> correlationData = _stockAnalysisLogic.CalculateCorrelations(stocks);
        //        return Ok(correlationData);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred: {ex.Message}");
        //    }
        //}

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





//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace FinancialAnalysis
//{
//    // Define a class for representing an asset
//    public class Asset
//    {
//        public string Symbol { get; set; }
//        public List<double> Prices { get; set; }

//        public Asset(string symbol, List<double> prices)
//        {
//            Symbol = symbol;
//            Prices = prices;
//        }
//    }

//    // Define a class for financial analysis
//    public class FinancialAnalyzer
//    {
//        public double CalculateReturn(List<double> prices)
//        {
//            double firstPrice = prices.First();
//            double lastPrice = prices.Last();
//            return (lastPrice - firstPrice) / firstPrice;
//        }

//        public double CalculateVolatility(List<double> prices)
//        {
//            double mean = prices.Average();
//            double squaredDiffSum = prices.Sum(price => Math.Pow(price - mean, 2));
//            double variance = squaredDiffSum / prices.Count;
//            return Math.Sqrt(variance);
//        }

//        public double CalculateCorrelation(List<double> prices1, List<double> prices2)
//        {
//            double mean1 = prices1.Average();
//            double mean2 = prices2.Average();
//            double sumProduct = prices1.Zip(prices2, (price1, price2) => (price1 - mean1) * (price2 - mean2)).Sum();
//            double squaredDiffSum1 = prices1.Sum(price => Math.Pow(price - mean1, 2));
//            double squaredDiffSum2 = prices2.Sum(price => Math.Pow(price - mean2, 2));
//            double variance1 = squaredDiffSum1 / prices1.Count;
//            double variance2 = squaredDiffSum2 / prices2.Count;
//            return sumProduct / (Math.Sqrt(variance1) * Math.Sqrt(variance2));
//        }
//    }

//    class Program
//    {
//        static void Main(string[] args)
//        {
//            // Sample asset data
//            List<double> prices1 = new List<double> { 100, 105, 110, 95, 105 };
//            List<double> prices2 = new List<double> { 200, 210, 190, 205, 195 };

//            // Create assets
//            Asset asset1 = new Asset("Asset1", prices1);
//            Asset asset2 = new Asset("Asset2", prices2);

//            // Perform financial analysis
//            FinancialAnalyzer analyzer = new FinancialAnalyzer();
//            double return1 = analyzer.CalculateReturn(asset1.Prices);
//            double return2 = analyzer.CalculateReturn(asset2.Prices);
//            double volatility1 = analyzer.CalculateVolatility(asset1.Prices);
//            double volatility2 = analyzer.CalculateVolatility(asset2.Prices);
//            double correlation = analyzer.CalculateCorrelation(asset1.Prices, asset2.Prices);

//            // Print the results
//            Console.WriteLine($"Return for {asset1.Symbol}: {return1:P}");
//            Console.WriteLine($"Return for {asset2.Symbol}: {return2:P}");
//            Console.WriteLine($"Volatility for {asset1.Symbol}: {volatility1:P}");
//            Console.WriteLine($"Volatility for {asset2.Symbol}: {volatility2:P}");
//            Console.WriteLine($"Correlation between {asset1.Symbol} and {asset2.Symbol}: {correlation:F}");

//            // TODO: Implement data visualization and filtering/selecting options

//            // Perform unit tests
//            RunUnitTests();

//            Console.ReadLine();
//        }

//        static void RunUnitTests()
//        {
//            // TODO: Implement unit tests for the FinancialAnalyzer class
//        }
//    }
//}
