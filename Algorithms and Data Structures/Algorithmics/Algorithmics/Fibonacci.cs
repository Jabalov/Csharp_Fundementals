using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics
{
    public class Fibonacci
    {
        int Fib(int n)
        {
            int low = 1;
            int high = 1;

            for (int i = 0; i < n; i++)
            {
                var tmp = high;
                high = low + high;
                low = tmp;
            }
            return low;
        }

        int RecursiveFib(int n)
        {
            if (n <= 1)
                return 1;

            return RecursiveFib(n - 1) + RecursiveFib(n - 2);
        }
    }
}
