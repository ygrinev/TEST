using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class TreeHelper
    {
        int[] forest;
        bool[] leafs; // excludes roots
        public TreeHelper(int n)
        {
            forest = Enumerable.Repeat(-1, n+1).ToArray();
            leafs = new bool[n+1];
        }
        public int getParent(int node)
        {
            while (forest[node] >= 0)
                node = forest[node];
            return node;
        }
        public bool merge(int n1, int n2, out int leaf)
        {
            leaf = -1;
            int u = getParent(n1), v = getParent(n2);
            if(u == v) return false;

            bool b1 = u == n1, b2 = v == n2;
            leaf = b1 && forest[u] <= forest[v] ? n1 : b2 && forest[u] > forest[v] ? n2 : -1;
            if (leaf == n1) leafs[n2] = !(leafs[n1] = true);
            else if (leaf == n2) leafs[n1] = !(leafs[n2] = true);
            else leafs[n1] = leafs[n2] = false;

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
