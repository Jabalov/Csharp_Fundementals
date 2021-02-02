using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics.InvestmentAnalyzer
{
    public class InvestmentQuery : IComparable<InvestmentQuery>
    {
        public string StockID { get; set; }
        public DateTime QueryTime { get; set; }
        public int Priority { get; set; }
        public Guid Investor { get; set; }

        public int CompareTo(InvestmentQuery other)
        {
            var idCompare = StockID.CompareTo(other.StockID);
            if (idCompare != 0)
                return idCompare;
            return QueryTime.CompareTo(other.QueryTime);
        }
    }
}
