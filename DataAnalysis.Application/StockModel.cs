namespace DataAnalysis.Application
{
    public class Stock
    {
        public string Ticker { get; set; }
        public DateTime Date { get; set; }
        public double Open { get; set; }
        public double High { get; set; }
        public double Low { get; set; }
        public double Close { get; set; }
        public double Volume { get; set; }
        public double Returns { get; set; }

        public Stock(string ticker, DateTime date, double open, double high, double low, double close, double volume)
        {
            Ticker = ticker;
            Date = date;
            Open = open;
            High = high;
            Low = low;
            Close = close;
            Volume = volume;
        }

        public double CalculateReturns(Stock previousStock)
        {
            return (Close - previousStock.Close) / previousStock.Close;
        }
    }
}