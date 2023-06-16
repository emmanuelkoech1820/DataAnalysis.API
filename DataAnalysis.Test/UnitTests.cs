using DataAnalysis.Application;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace FinancialAnalysis.API.Logic.Tests
{
    [TestFixture]
    public class StockAnalysisLogicTests
    {
        private StockAnalysisLogic stockAnalysisLogic;

        [SetUp]
        public void Setup()
        {
            // Create an instance of StockAnalysisLogic
            stockAnalysisLogic = new StockAnalysisLogic();
        }

        [Test]
        public void CalculateReturns_UpdatesStockReturnsCorrectly()
        {
            // Arrange
            List<Stock> stocks = new List<Stock>
            {
                new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
                new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
                new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300)
            };

            // Act
            stockAnalysisLogic.CalculateReturns(stocks);

            // Assert
            Assert.AreEqual(0.019409937888198756, stocks[1].Returns);
            Assert.AreEqual(0.14868901303538175, stocks[2].Returns);
        }

        [Test]
        public void CalculateVolatility_CalculatesVolatilityCorrectly()
        {
            // Arrange
            List<Stock> stocks = new List<Stock>
            {
                new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
                new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
                new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300)
            };

            // Act
            var volatilityData = stockAnalysisLogic.CalculateVolatility(stocks);

            // Assert
            Assert.AreEqual(0.00884760069270728, volatilityData["MSFT"]);
            Assert.AreEqual(0.046366986642494, volatilityData["ATTU"]);
            Assert.AreEqual(0.05189232339687206, volatilityData["ATU"]);
        }

        [Test]
        public void CalculateCorrelations_CalculatesCorrelationsCorrectly()
        {
            // Arrange
            List<Stock> stocks = new List<Stock>
            {
                new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
                new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
                new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300)
            };

            // Act
            var correlationData = stockAnalysisLogic.CalculateCorrelations(stocks);

            // Assert
            Assert.AreEqual(-0.8819820559863034, correlationData["MSFT"]["ATTU"]);
            Assert.AreEqual(-0.935764721614977, correlationData["MSFT"]["ATU"]);
            Assert.AreEqual(-0.8723467744588743, correlationData["ATTU"]["MSFT"]);
            Assert.AreEqual(-0.8500350248935016, correlationData["ATTU"]["ATU"]);
            Assert.AreEqual(-0.9277499475969787, correlationData["ATU"]["MSFT"]);
            Assert.AreEqual(-0.8928020667365846, correlationData["ATU"]["ATTU"]);
        }

        [Test]
        public void FilterByTimePeriod_FiltersStocksByTimePeriod()
        {
            // Arrange
            List<Stock> stocks = new List<Stock>
            {
                new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
                new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
                new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300)
            };

            DateTime startDate = new DateTime(2016, 1, 1);
            DateTime endDate = new DateTime(2016, 12, 31);

            // Act
            var filteredStocks = stockAnalysisLogic.FilterByTimePeriod(stocks, startDate, endDate);

            // Assert
            Assert.AreEqual(2, filteredStocks.Count);
            Assert.AreEqual("MSFT", filteredStocks[0].Ticker);
            Assert.AreEqual("ATTU", filteredStocks[1].Ticker);
        }

        [Test]
        public void FilterByAsset_FiltersStocksByAsset()
        {
            // Arrange
            List<Stock> stocks = new List<Stock>
            {
                new Stock("MSFT", new DateTime(2016, 5, 16), 50.8, 51.96, 50.75, 51.83, 20032017),
                new Stock("ATTU", new DateTime(2016, 1, 2), 68.85, 69.84, 67.85, 67.87, 30977700),
                new Stock("ATU", new DateTime(2018, 9, 1), 53.41, 55, 53.17, 54.32, 41591300)
            };

            string ticker = "ATTU";

            // Act
            var filteredStocks = stockAnalysisLogic.FilterByAsset(stocks, ticker);

            // Assert
            Assert.AreEqual(1, filteredStocks.Count);
            Assert.AreEqual("ATTU", filteredStocks[0].Ticker);
        }
    }
}
