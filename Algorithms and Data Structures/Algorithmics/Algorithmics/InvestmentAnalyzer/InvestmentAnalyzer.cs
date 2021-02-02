using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics.InvestmentAnalyzer
{
    class InvestmentAnalyzer
    {
        IStockTrader stockTrader;
        C5.IntervalHeap<InvestmentQuery> queries = new C5.IntervalHeap<InvestmentQuery>();
        Dictionary<string, int> stock2rating = new Dictionary<string, int>();
        HashSet<Guid> blackListedInvestors = new HashSet<Guid>();
        Random random = new Random(29);

        public InvestmentAnalyzer(IStockTrader stockTrader)
        {
            this.stockTrader = stockTrader;
        }

        public void HandleQuery(InvestmentQuery query)
        {
            queries.Add(query);
        }

        public void AnalyzeQueries()
        {
            while (!queries.IsEmpty)
            {
                int rating;
                var query = queries.DeleteMin(); // Get first-priority queries first
                if (stock2rating.ContainsKey(query.StockID))
                    rating = stock2rating[query.StockID];
                else
                {
                    rating = CalculateRating(query.StockID);
                    stock2rating[query.StockID] = rating;
                }

                if (rating > 80) // Let's say that a rating of 80 triggers a stock trade
                    stockTrader.EnqueueStockForTrading(query);
            }
        }

        int CalculateRating(string stockID)
        {
            // Simulate some computation time that might involve
            // fetching data from various external data sources:
            //            Thread.Sleep(100);

            // Simulate some calculation result:
            return random.Next(100);
        }
    }
}
