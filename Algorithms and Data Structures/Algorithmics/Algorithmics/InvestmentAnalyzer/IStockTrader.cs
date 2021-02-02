using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics.InvestmentAnalyzer
{
    interface IStockTrader
    {
        void EnqueueStockForTrading(InvestmentQuery query);
        void HandledTradings();
    }
}
