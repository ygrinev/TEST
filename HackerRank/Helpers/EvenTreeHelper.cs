using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class EvenTreeHelper
    {
        List<int>[] graph;
        public int rmvCount = 0;
        public EvenTreeHelper(int n)
        {
            graph = new List<int>[n+1];
        }
        public int evenForest(string[] edges)
        {
            if (graph.Length < 4 || edges.Length < 4) return 0;
            int max = 0;
            Dictionary<int, int> counts = new Dictionary<int, int>();

            int root = edges.Aggregate(0, (idx, next) => 
            {
                int[] cur = Array.ConvertAll(next.Split(' '), e=>Convert.ToInt32(e));
                foreach (int i in cur) 
                { 
                    counts[i] = counts.ContainsKey(i) ? counts[i] + 1 : 1; 
                    if (counts[i] > max) { max = counts[i]; idx = i; } 
                }
                for(int k = 0; k < 2; k++) (graph[cur[k]] ?? (graph[cur[k]] = new List<int>())).Add(cur[(k+1)%2]);
                return idx; 
            });
            rmvCount = 0;
            int n = countEvenSubtrees(root);
            
            return rmvCount;
        }

        private int countEvenSubtrees(int node, int parent = -1)
        {
            int sum = graph[node].Sum(child=>child == parent ? 0 : graph[child].Count == 1 ? 1 : countEvenSubtrees(child, node));
            if (sum % 2 == 1 && parent > -1) rmvCount++;
            return sum+1;
        }
    }
}
