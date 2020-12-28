using HackerRank.Data;
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
        public DijkstraHelper(int n, List<int[]> edges, int defWeight = 1, bool directed = false, bool useMinWeight = true)
        {
            weight = new int[n + 1][].Select(d => new int[n + 1]).ToArray();
            graph = new List<int>[n + 1];
            bool useDef = edges[0].Length <= 2;
            // Create Graph
            foreach (int[] e in edges)
            {
                int val = useDef ? defWeight : e[2];
                int curWeight = weight[e[0]][e[1]];
                if (!useMinWeight || curWeight == 0 || curWeight > val)
                {
                    if (graph[e[0]] == null) graph[e[0]] = new List<int>();
                    if (!directed && graph[e[1]] == null) graph[e[1]] = new List<int>();
                    if (curWeight == 0)
                    {
                        graph[e[0]].Add(e[1]);
                        if (!directed) graph[e[1]].Add(e[0]);
                    }
                    weight[e[0]][e[1]] = val;
                    if (!directed) weight[e[1]][e[0]] = val;
                }
            }
        }
        public int[] FromNodeBFS(int startNode)
        {
            getAllShortestPaths(startNode, startNode);

            return weight[startNode].Select((j, idx) => j < 0 && j != int.MinValue && idx != startNode ? -j : idx == 0 || idx == startNode ? 0 : -1).Where(t => t != 0).ToArray();
        }

        public int FromToNodeBFS(int startNode, int endNode)
        {
            if (startNode == endNode) return 0;
            if (weight[startNode][endNode] < 0) return -weight[startNode][endNode];
            getAllShortestPaths(startNode, startNode);

            return weight[startNode][endNode] == 0 ? -1 : -weight[startNode][endNode];
        }

        private void getAllShortestPaths(int nIdx, int startNode)
        {
            if (graph[nIdx] == null) return;
            foreach (int i in graph[nIdx])
            {
                int newPath = Math.Abs(weight[nIdx][i]) + Math.Abs(weight[startNode][nIdx]);
                bool visited = weight[startNode][i] < 0;
                bool update = weight[startNode][i] == 0 || newPath < Math.Abs(weight[startNode][i]);
                bool add = i != nIdx && weight[nIdx][i] != 0 && (!visited || update);
                if (add)
                {
                    if (update) weight[startNode][i] = -newPath;
                    else 
                    if (!visited) weight[startNode][i] = -weight[startNode][i];
                    getAllShortestPaths(i, startNode);
                }
            }
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
        public static int shortestPath(int n, string[] moves) // ladders and snakes
        {
            int[] grph = new int[n + 1].Select((v, i) => i).ToArray();
            int[] counts = new int[n + 1];
            moves.Aggregate(0, (cur, next) => { int[] vv = Array.ConvertAll(next.Split(' '), s => int.Parse(s)); grph[vv[0]] = vv[1]; return 0; });
            getMinPath(1, 6, n, grph, counts);
            return counts[n] <= 0 ? -1 : counts[n];
        }

        private static void getMinPath(int v, int delta0, int n, int[] grph, int[] counts, int path = 0)
        {
            path++;
            int delta = delta0;
            do
            {
                int idx = v + delta > n ? n + 1 : grph[v + delta];
                if (idx <= n)
                {
                    if (counts[idx] == 0 || path < counts[idx])
                    {
                        counts[idx] = path;
                        if (idx < n) getMinPath(idx, delta0, n, grph, counts, path);
                    }
                }
            }
            while (--delta > 0);
        }
        public static void fullTest1()
        {
            int cases = 2, count = 0;
            bool started = false;
            List<int> result = new List<int>();
            List<string> moves = new List<string>();
            string[] queries = DijkstraData.data100;

            foreach (string move in queries)
            {
                string[] ss = move.Split(' ');
                switch (ss.Length)
                {
                    case 1:
                        if (started)
                        {
                            cases--;
                            count = int.Parse(ss[0]);
                            if (count == 0 && cases == 0)
                            {
                                cases = 2;
                                Console.WriteLine(shortestPath(100, moves.ToArray()));
                            }
                        }
                        else started = true;
                        break;
                    case 2:
                        moves.Add(move);
                        if (--count == 0 && cases == 0)
                        {
                            cases = 2;
                            Console.WriteLine(shortestPath(100, moves.ToArray()));
                            moves.Clear();
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        public static void fullTest2()
        {
            string[] queries =  DijkstraData.dataLight; // DijkstraData.dataLight2; // DijkstraData.dataLight3; // 
            List<int[]> edges = new List<int[]>();
            DijkstraHelper helper = null;
            List<int> result = new List<int>();

            foreach (string q in queries)
            {
                int[] edge = Array.ConvertAll(q.Split(' '), e => Convert.ToInt32(e));
                switch (edge.Length)
                {
                    case 1:
                        helper = new DijkstraHelper(5, edges, 1, true, false);
                        break;
                    case 2:
                        result.Add(helper.FromToNodeBFS(edge[0], edge[1]));
                        break;
                    case 3:
                        edges.Add(edge);
                        break;
                }
            }
            Console.WriteLine(string.Join("\n", result));
        }
    }
}