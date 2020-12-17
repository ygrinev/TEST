using HackerRank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{

    class DijkstraHelper
    {
        private int[][] weight;
        public DijkstraHelper(int n, int[][] edges, int defWeight = 1)
        {
            weight = new int[n + 1][].Select(d => new int[n + 1]).ToArray();
            bool useDef = edges[0].Length <= 2;
            // Create Graph
            foreach(int[] e in edges)
            {
                int val = useDef ? defWeight : e[2];
                if(weight[e[0]][e[1]] == 0 || weight[e[0]][e[1]] > val)
                    weight[e[0]][e[1]] = weight[e[1]][e[0]] = val;
            }
        }
        public int[] FromNodeBFS(int startNode)
        {
            int n = weight.Length - 1;
            bool[] toVisit = new bool[n + 1];
            toVisit[startNode] = true;
            while (toVisit.Any(v => v))
            {
                toVisit = getAllShortestPaths(toVisit);
            }
            return weight.Select((node, j)=>node[j] != 0 && j != startNode ? node[j] : j == 0 || j == startNode ? 0 : -1).Where(t=>t != 0).ToArray();
        }

        private bool[] getAllShortestPaths(bool[] toVisit)
        {
            bool[] newVisit = new bool[toVisit.Length];
            for(int nIdx = 1; nIdx < toVisit.Length; nIdx++)
            {
                if (!toVisit[nIdx]) continue;
                for(int i = 1; i < toVisit.Length; i++)
                {
                    int newPath = weight[nIdx][i] + weight[nIdx][nIdx];
                    if (i == nIdx || weight[nIdx][i] == 0
                    || weight[i][i] != 0 
                    && newPath >= weight[i][i])
                        continue;
                    weight[i][i] = newPath;
                    newVisit[i] = true;
                }
            }
            return newVisit;
        }
    }
}
