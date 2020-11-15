using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class MaxCostHelper
    {
        // Step 1: Sort the input by cost
        //public static Dictionary<int, int> costs = new Dictionary<int, int>();
        //public static void sortCosts() { }
        // Step 2: Creating the graph
        // Step 3: Save partial sets for every incremented cost value
        // Step 4: Spit answers for lower and higher costs

        //******************************************************************
        //******************************************************************
        //******************************************************************
        //******************************************************************
        //******************************************************************
        //******************************************************************

        static int find(int node, int[] parent)
        {
            while (parent[node] >= 0)
                node = parent[node];
            return node;
        }

        static int lower_bound(int target, int[][] dp)
        {
            int left = 0;
            int right = dp.Length - 1;
            while (left <= right)
            {
                int middle = (left + right) >> 1;
                if (dp[middle][2] == target) return dp[middle][2];
                else if (dp[middle][2] > target) right = middle - 1;
                else left = middle + 1;
            }
            return dp[right][2];
        }

        //static int lower_bound(int target, IOrderedEnumerable<IGrouping<int,int[]>> sorted)
        //{
        //    return sorted.LastOrDefault(g=>g.Key <= target)?.Key??0;
        //}

        public static long[] MaxCostCount(int[][] tree, int[][] queries)
        {
            // cost value from low to high
            //var sorted = tree.GroupBy(el => el[2]).OrderBy(g => g.First()[2]);
            int[][] sorted = tree.OrderBy(g => g[2]).ToArray();

            Dictionary<int, long> map = new Dictionary<int, long>();
            int[] parent = Enumerable.Repeat(-1, tree.Count() + 2).ToArray();
            long total = 0;
            foreach (var g in sorted)
            {
                int key = g[2];
                int u = find(g[0], parent), v = find(g[1], parent);
                long number1 = -parent[u], number2 = -parent[v];
                if (parent[u] > parent[v]) { int tmp = v; v = u; u = tmp; } // attach small to big
                parent[u] += parent[v];
                parent[v] = u;
                total = map[key] = (map.ContainsKey(key) ? map[key] : total) + number1 * number2;
            }

            List<long> result = new List<long>();
            int max = sorted.Last()[2], min = sorted.First()[2];
            foreach (var q in queries)
            {
                if (q[0] > max || q[1] < min) result.Add(0L);
                else
                {
                    long totMin = q[0] <= min ? 0 : map[lower_bound(q[0] - 1, sorted)]
                        ,totMax = q[1] >= max ? map[max] : map[lower_bound(q[1], sorted)];
                    result.Add(totMax - totMin);
                }
            }
            return result.ToArray();
        }
    }
}
