using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HackerRank.Helpers
{

    class DijkstraHelper
    {
        private List<int>[] graph;
        private int[][] weight;
        public DijkstraHelper(int n, List<int[]> edges, int defWeight = 1)
        {
            weight = new int[n + 1][].Select(d => new int[n + 1]).ToArray();
            graph = new List<int>[n + 1];
            bool useDef = edges[0].Length <= 2;
            // Create Graph
            foreach (int[] e in edges)
            {
                int val = useDef ? defWeight : e[2];
                int curWeight = weight[e[0]][e[1]];
                if (curWeight == 0 || curWeight > val)
                {
                    if (graph[e[0]] == null) graph[e[0]] = new List<int>();
                    if (graph[e[1]] == null) graph[e[1]] = new List<int>();
                    if (curWeight == 0)
                    {
                        graph[e[0]].Add(e[1]);
                        graph[e[1]].Add(e[0]);
                    }
                    weight[e[0]][e[1]] = weight[e[1]][e[0]] = val;
                }
            }
        }
        public int[] FromNodeBFS(int startNode)
        {
            IEnumerable<int> toVisit = new List<int> { startNode };
            while (toVisit.Count() > 0)
            {
                toVisit = getAllShortestPaths(toVisit);
            }

            return weight.Select((node, j) => node[j] != 0 && j != startNode ? node[j] : j == 0 || j == startNode ? 0 : -1).Where(t => t != 0).ToArray();
        }

        private IEnumerable<int> getAllShortestPaths(IEnumerable<int> toVisit)
        {
            List<int> newVisit = new List<int>();
            foreach (int nIdx in toVisit)
            {
                if (graph[nIdx] == null) continue;
                newVisit.AddRange(graph[nIdx].Select(i => {
                    int newPath = weight[nIdx][i] + weight[nIdx][nIdx];
                    bool toAdd = i != nIdx && weight[nIdx][i] != 0
                        && (weight[i][i] == 0 || newPath < weight[i][i]);
                    if (toAdd) weight[i][i] = newPath;
                    return toAdd ? i : -1;
                }));
            }
            return newVisit.Where(it => it > -1);
        }
        public static void fullTest()
        {
            string dataFile = "DijkstraData.txt";
            string[] queries = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory.Replace(@"bin\Debug", "Data"), dataFile));
            int n = 0, nEdges = 0;
            int count = 0;
            int cases = 0;
            List<int[]> edges = new List<int[]>();
            foreach (string q in queries)
            {
                int[] edge = Array.ConvertAll(q.Split(' '), e => Convert.ToInt32(e));
                switch (edge.Length)
                {
                    case 1:
                        if (count > 0)
                        {
                            var result = new DijkstraHelper(n, edges).FromNodeBFS(edge[0]);
                            Console.WriteLine(string.Join(" ", result));
                        }
                        else
                            cases = edge[0];
                        break;
                    case 2:
                        n = edge[0];
                        nEdges = edge[1];
                        edges = new List<int[]>();
                        break;
                    case 3:
                        edges.Add(edge);
                        break;
                }
                count++;
            }
        }
    }
}
