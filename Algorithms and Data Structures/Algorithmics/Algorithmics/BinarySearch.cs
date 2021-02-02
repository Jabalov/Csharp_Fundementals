using System;
using System.Collections.Generic;
using System.Text;

namespace Algorithmics
{
    public class BinarySearch
    {
        int? BinarySearchAlg(List<int> hayStack, int needle, int min, int max)
        {
            var midPoint = (max + min) / 2;

            if (hayStack.Count > 0 && hayStack[midPoint] == needle)
                return midPoint;

            if (min >= max)
                return null;

            if (hayStack[midPoint] > needle)
                return BinarySearchAlg(hayStack, needle, min, midPoint - 1);

            return BinarySearchAlg(hayStack, needle, midPoint + 1, max);
        }
    }
}
