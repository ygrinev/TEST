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
        private int[] ptStart, ptEnd;
        private int maxLuck = int.MaxValue;
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
        public DijkstraHelper(int n, int m)
        {
            weight = new int[n][];
            Array.ForEach(weight, item => item = new int[m]);
        }
        public DijkstraHelper(string[] matrix, char go, char start, char end)
        {
            weight = new int[matrix.Length][];
            int row = 0;
            Array.ForEach(weight, item =>
            {
                weight[row] = matrix[row].ToCharArray().Select((c, col) =>
                {
                    if (c == start) ptStart = new int[] { row, col };
                    if (c == end) ptEnd = new int[] { row, col };
                    return c == go || c == end ? -1 : -2;
                }).ToArray(); row++;
            });
            weight[ptEnd[0]][ptEnd[1]] = int.MaxValue;
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
            string[] queries = DijkstraData.dataLight; // DijkstraData.dataLight2; // DijkstraData.dataLight3; // 
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

        public static int countConnectedCells(int i, int j, int[][] matrix)
        {
            int count = matrix[i][j] > 0 ? 1 : 0;
            if (matrix[i][j] > 0)
            {
                matrix[i][j] = 0;
                foreach (int[] pair in new int[8][] { new int[] { i - 1, j - 1 }, new int[] { i - 1, j }, new int[] { i, j - 1 }, new int[] { i - 1, j + 1 }, new int[] { i + 1, j - 1 }, new int[] { i, j + 1 }, new int[] { i + 1, j }, new int[] { i + 1, j + 1 } })
                {
                    if (pair[0] >= 0 && pair[0] < matrix.Length
                    && pair[1] >= 0 && pair[1] < matrix[i].Length
                    && matrix[pair[0]][pair[1]] > 0)
                        count += countConnectedCells(pair[0], pair[1], matrix);
                }

            }
            return count;
        }

        public static int connectedCell(int[][] matrix)
        {
            int max = 0;
            for (int i = 0; i < matrix.Length; i++)
            {
                for (int j = 0; j < matrix[i].Length; j++)
                {
                    if (matrix[i][j] > 0)
                    {
                        max = Math.Max(max, countConnectedCells(i, j, matrix));
                    }
                }
            }
            return max;
        }
        List<int[]> getDirs(int i, int j, int prevI, int prevJ, int k, out int cnt)
        {
            bool start = i - prevI == 0 && j - prevJ == 0;
            List<int[]> dirs = new List<int[]>();
            int[][] nextCells = new int[][] { new int[] { i - 1, j - 1 }, new int[] { i, j - 1 }, new int[] { i + 1, j - 1 }, new int[] { i + 1, j }, new int[] { i + 1, j + 1 }, new int[] { i, j + 1 }, new int[] { i - 1, j + 1 }, new int[] { i - 1, j }, new int[] { i - 1, j - 1 } };
            int cntDirs = 0, count = 0;
            nextCells.Aggregate(true, (stat, next) =>
            {
                bool tmpStat = next[0] >= 0
                         && next[0] < weight.Length
                         && next[1] >= 0
                         && next[1] < weight[i].Length
                         && (weight[next[0]][next[1]] >= -1);
                if (tmpStat)
                {
                    if(count < 8 
                    && (start || Math.Max(Math.Abs(prevI - next[0]), Math.Abs(prevJ - next[1])) == 2)
                    && (weight[next[0]][next[1]] == -1 || weight[next[0]][next[1]] > k)) 
                        dirs.Add(next);
                    if (!stat) cntDirs++;
                }
                count++;
                return tmpStat;
            });
            cnt = cntDirs > 0 ? cntDirs : dirs.Count > 0 ? 1 : 0;
            return dirs;
        }
        List<int[]> getDirsUDLR(int i, int j, int prevI, int prevJ, int k, out int cnt)
        {
            bool start = i - prevI == 0 && j - prevJ == 0;
            List<int[]> dirs = new List<int[]>();
            int[][] nextCells = new int[][] { new int[] { i + 1, j }, new int[] { i - 1, j }, new int[] { i, j - 1 }, new int[] { i, j + 1 } };
            cnt = nextCells.Aggregate(0, (cntDirs, next) =>
            {
                if(next[0] >= 0
                && next[0] < weight.Length
                && next[1] >= 0
                && next[1] < weight[i].Length
                && (start || prevI != next[0] || prevJ != next[1])
                && (weight[next[0]][next[1]] == -1
                || weight[next[0]][next[1]] > k))
                {
                    dirs.Add(next);
                    cntDirs++;
                }
                return cntDirs;
            });
            return dirs;
        }
        bool hasLuck(int i, int j, int prevI, int prevJ, int k = 0)
        {
            if (ptEnd[0] == i && ptEnd[1] == j)
            {
                if (weight[ptEnd[0]][ptEnd[1]] > k)
                {
                    weight[ptEnd[0]][ptEnd[1]] = k;
                }
                return k == maxLuck;
            }
            bool isMatch = false;
            int cntDirs = 0;
            List<int[]> nextCells = getDirsUDLR(i, j, prevI, prevJ, k, out cntDirs);
            if (cntDirs > 1) k++;
            if (cntDirs < 1 || weight[i][j] > -1 && weight[i][j] < k || maxLuck < k) return false;
            weight[i][j] = k;
            foreach (int[] pair in nextCells)
            {
                if (weight[pair[0]][pair[1]] == -1 || weight[pair[0]][pair[1]] > k)
                    isMatch = hasLuck(pair[0], pair[1], i, j, k) || isMatch;
            }
            return isMatch;
        }
        public string countLuck(int k)
        {
            maxLuck = k;
            hasLuck(ptStart[0], ptStart[1], ptStart[0], ptStart[1]);
            return weight[ptEnd[0]][ptEnd[1]] == k ? "Impressed" : "Oops!";
        }

    }
}