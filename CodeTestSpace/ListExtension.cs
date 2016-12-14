using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeTestSpace
{
    public static class ListExtension
    {
        public static double Sd(this IEnumerable<double> x)
        {
            var m = x.Average();
            var sumSq = 0d;
            foreach (var xi in x)
            {
                sumSq = (xi - m) * (xi - m);
            }
            var n = x.Count();
            var ans = sumSq / n;
            return ans;
        }
    }

}