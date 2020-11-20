using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class TreeHelper
    {
        private int[] forest;
        public TreeHelper(int n)
        {
            forest = Enumerable.Repeat(-1, n+1).ToArray();
        }
        public int getParent(int node)
        {
            while (forest[node] >= 0)
                node = forest[node];
            return node;
        }
        public bool merge(int n1, int n2)
        {
            int u = getParent(n1), v = getParent(n2);
            if(u == v) return false;
            if (forest[u] > forest[v]) { int tmp = v; v = u; u = tmp; } // attach small to big
            forest[u] += forest[v];
            forest[v] = u;
            return true;
        }
        public long initTrees(int[][] queries) // returns count of all possible paths
        {
            long totalPairs = 0;
            foreach (var q in queries)
            {
                int u = getParent(q[0]), v = getParent(q[1]);
                totalPairs += forest[u] * forest[v];
                if (forest[u] > forest[v]) { int tmp = v; v = u; u = tmp; } // attach small to big
                forest[u] += forest[v];
                forest[v] = u;
            }
            return totalPairs;
        }
    }
}
