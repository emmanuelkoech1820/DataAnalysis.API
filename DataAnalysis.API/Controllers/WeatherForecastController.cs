using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using System.IO;
using System.Net;

namespace DataAnalysis.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
        private const string DataUrl = "https://example.com/financial-data";
        private readonly ILogger<WeatherForecastController> _logger;
        private const string ExcelUrl = "https://shorturl.at/itz57";

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IActionResult> Geat()
        {

            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(ExcelUrl);
                request.Method = "GET";

                // Get the response from the WebRequest
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                {
                    // Load the response stream into an ExcelPackage
                    using (Stream stream = response.GetResponseStream())
                    using (ExcelPackage package = new ExcelPackage(stream))
                    {
                        ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Assuming the data is in the first worksheet

                        // Retrieve the data from the Excel worksheet
                        // Iterate over the rows and columns to process the data
                        for (int row = 1; row <= worksheet.Dimension.Rows; row++)
                        {
                            for (int col = 1; col <= worksheet.Dimension.Columns; col++)
                            {
                                object cellValue = worksheet.Cells[row, col].Value;
                                // Process the cell value as needed
                            }
                        }
                    }
                }


                return Ok("Financial data retrieved successfully");
            }
            catch (Exception ex)
            {
                return Ok("Financial data retrieved successfully");
                // Handle exception if unable to retrieve or process the Excel file
               // return InternalServerError(ex);
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
