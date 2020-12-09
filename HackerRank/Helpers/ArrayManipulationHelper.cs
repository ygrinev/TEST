using System.Linq;

namespace HackerRank.Helpers
{
    class ArrayManipulationHelper
    {
        public static long arrayManipulation(int[][] queries, out long idx)
        {
            long i = 0L;
            long[] ret = queries.Select(q => new int[2][] { new int[] { q[0], q[2] }, new int[] { q[1] + 1, -q[2] } })
                .SelectMany(e => e).GroupBy(e => e[0]).OrderBy(g => g.ElementAt(0)[0])
                .Aggregate(new long[] { 0L,0L,0L }, func: (sum, item) => {
                    sum[1] += item.Sum(e => e[1]);
                    if (sum[1] > sum[0]) 
                    { 
                        sum[0] = sum[1]; 
                        sum[2] = item.ElementAt(0)[0];
                    }
                    i++;
                    return sum; 
                });
            idx = ret[2];
            return ret[0];
        }
    }
}
