using DataAnalysis.Application;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using LicenseContext = OfficeOpenXml.LicenseContext;

namespace FinancialAnalysis.API.Logic
{
    public class StockAnalysisLogic : IStockAnalysisLogic
    {//private string filePath = Path.Combine(AppContext.BaseDirectory, "stock_prices_latest.xlsx");
        string filePath = @"C:\Users\manu\source\repos\DataAnalysis.API\DataAnalysis.API\stock_prices_latest.xlsx";
        private readonly List<Stock> stocks;

        public StockAnalysisLogic()
        {
            Dictionary<string, List<List<string>>> sheetColumns = new Dictionary<string, List<List<string>>>();

            // Set the LicenseContext
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial; // Assuming you are using EPPlus in a non-commercial project

            FileInfo fileInfo = new FileInfo(filePath);

            using (ExcelPackage package = new ExcelPackage(fileInfo))
            {
                ExcelWorksheet worksheet = package.Workbook.Worksheets[1]; // Fetch the first worksheet

                int rowCount = worksheet.Dimension.Rows;
                int columnCount = worksheet.Dimension.Columns;

                for (int row = 2; row <= rowCount; row++) // Start from row 2 to skip the header
                {
                    string ticker = worksheet.Cells[row, 1].Value?.ToString();
                    DateTime date = DateTime.ParseExact(worksheet.Cells[row, 2].Value?.ToString(), "dd-MM-yy", CultureInfo.InvariantCulture);
                    double open = Convert.ToDouble(worksheet.Cells[row, 3].Value);
                    double high = Convert.ToDouble(worksheet.Cells[row, 4].Value);
                    double low = Convert.ToDouble(worksheet.Cells[row, 5].Value);
                    double close = Convert.ToDouble(worksheet.Cells[row, 6].Value);
                    double volume = Convert.ToDouble(worksheet.Cells[row, 7].Value);

                    Stock stock = new Stock(ticker, date, open, high, low, close, volume);
                    stocks.Add(stock);
                }
            }
            // Sample data
            //stocks = new List<Stock>
            //{
            //    new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
            //    new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
            //    new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300),
            //    // Add more stocks here...
            //};
        }

        public List<Stock> GetStockData()
        {
            return stocks;
        }

        public void CalculateReturns(List<Stock> stocks)
        {
            for (int i = 1; i < stocks.Count; i++)
            {
                Stock currentStock = stocks[i];
                Stock previousStock = stocks[i - 1];
                currentStock.Returns = currentStock.CalculateReturns(previousStock);
            }
        }

        //public Dictionary<string, double> CalculateVolatility(List<Stock> stocks)
        //{
        //    Dictionary<string, double> volatilityData = new Dictionary<string, double>();

        //    foreach (var stock in stocks)
        //    {
        //        double[] stockReturns = stock.Returns.ToArray();
        //        double volatility = CalculateStandardDeviation(stockReturns);
        //        volatilityData.Add(stock.Ticker, volatility);
        //    }

        //    return volatilityData;
        //}

        //public Dictionary<string, Dictionary<string, double>> CalculateCorrelations(List<Stock> stocks)
        //{
        //    Dictionary<string, Dictionary<string, double>> correlationData = new Dictionary<string, Dictionary<string, double>>();

        //    foreach (var stock1 in stocks)
        //    {
        //        Dictionary<string, double> correlations = new Dictionary<string, double>();

        //        foreach (var stock2 in stocks)
        //        {
        //            if (stock1.Ticker != stock2.Ticker)
        //            {
        //                double[] returns1 = stock1.Returns.ToArray();
        //                double[] returns2 = stock2.Returns.ToArray();
        //                double correlation = CalculateCorrelation(returns1, returns2);
        //                correlations.Add(stock2.Ticker, correlation);
        //            }
        //        }

        //        correlationData.Add(stock1.Ticker, correlations);
        //    }

        //    return correlationData;
        //}

        public List<Stock> FilterByTimePeriod(List<Stock> stocks, DateTime startDate, DateTime endDate)
        {
            List<Stock> filteredStocks = stocks.FindAll(stock => stock.Date >= startDate && stock.Date <= endDate);
            return filteredStocks;
        }

        public List<Stock> FilterByAsset(List<Stock> stocks, string ticker)
        {
            List<Stock> filteredStocks = stocks.FindAll(stock => stock.Ticker == ticker);
            return filteredStocks;
        }

        private double CalculateStandardDeviation(double[] values)
        {
            double sum = 0;
            double mean = 0;

            foreach (double value in values)
            {
                sum += value;
            }

            mean = sum / values.Length;

            double sumOfSquaredDifferences = 0;

            foreach (double value in values)
            {
                double difference = value - mean;
                sumOfSquaredDifferences += Math.Pow(difference, 2);
            }

            double variance = sumOfSquaredDifferences / values.Length;
            double standardDeviation = Math.Sqrt(variance);

            return standardDeviation;
        }

        private double CalculateCorrelation(double[] returns1, double[] returns2)
        {
            if (returns1.Length != returns2.Length)
            {
                throw new ArgumentException("Arrays must have the same length");
            }

            int n = returns1.Length;

            double sum1 = 0;
            double sum2 = 0;
            double sum1Squared = 0;
            double sum2Squared = 0;
            double sumProduct = 0;

            for (int i = 0; i < n; i++)
            {
                double return1 = returns1[i];
                double return2 = returns2[i];

                sum1 += return1;
                sum2 += return2;
                sum1Squared += Math.Pow(return1, 2);
                sum2Squared += Math.Pow(return2, 2);
                sumProduct += return1 * return2;
            }

            double correlation = (n * sumProduct - sum1 * sum2) /
                                Math.Sqrt((n * sum1Squared - Math.Pow(sum1, 2)) *
                                          (n * sum2Squared - Math.Pow(sum2, 2)));

            return correlation;
        }
    }
}
