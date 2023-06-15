using DataAnalysis.Application;
using System;
using System.Collections.Generic;

namespace FinancialAnalysis.API.Logic
{
    public interface IStockAnalysisLogic
    {
        List<Stock> GetStockData();
        void CalculateReturns(List<Stock> stocks);
        //Dictionary<string, double> CalculateVolatility(List<Stock> stocks);
        //Dictionary<string, Dictionary<string, double>> CalculateCorrelations(List<Stock> stocks);
        List<Stock> FilterByTimePeriod(List<Stock> stocks, DateTime startDate, DateTime endDate);
        List<Stock> FilterByAsset(List<Stock> stocks, string ticker);
    }
}
