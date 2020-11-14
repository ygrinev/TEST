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

        static int lower_bound(int target, IOrderedEnumerable<IGrouping<int,int[]>> sorted)
        {
            return sorted.LastOrDefault(g=>g.Key <= target)?.Key??0;
        }

        public static long[] MaxCostCount(int[][] tree, int[][] queries)
        {
            // cost value from low to high
            var sorted = tree.GroupBy(el => el[2]).OrderBy(g => g.First()[2]);

            Dictionary<int, long> map = new Dictionary<int, long>();
            int[] parent = Enumerable.Repeat(-1, tree.Count() + 2).ToArray();
            long total = 0;
            foreach (var g in sorted)
            {
                map[g.Key] = total;
                foreach (int[] a in g)
                {
                    int u = find(a[0], parent);
                    int v = find(a[1], parent);
                    long number1 = -parent[u];
                    long number2 = -parent[v];
                    if (parent[u] > parent[v]) { int tmp = v; v = u; u = tmp; } // attach small to big
                    parent[u] += parent[v];
                    parent[v] = u;
                    map[g.Key] += number1 * number2;
                }
                total = map[g.Key];
            }

            List<long> result = new List<long>();
            int max = sorted.Last().Key, min = sorted.First().Key;
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
