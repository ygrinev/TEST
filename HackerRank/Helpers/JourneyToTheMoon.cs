using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank
{
    class JourneyToTheMoon
    {
        public static long solve(int totalAstronauts, string[] astronauts)
        {
            long totalPairs = astronauts.Length;
            if (totalAstronauts == 1) Console.Write(1);

            var visited = new bool[totalAstronauts];
            var sets = new List<long>();

            var graph = new Dictionary<long, List<long>>();

            for (var i = 0; i < totalAstronauts; i++)
            {
                graph.Add(i, new List<long>());
            }

            foreach (string s in astronauts)
            {
                var pair = s.Split(' ');
                var first = Convert.ToInt64(pair[0]);
                var second = Convert.ToInt64(pair[1]);

                if (first >= totalAstronauts || second >= totalAstronauts) continue;

                graph[first].Add(second);
                graph[second].Add(first);
            }

            for (var i = 0; i < totalAstronauts; i++)
            {
                if (visited[i]) continue;
                sets.Add(DFS(i, graph, visited));
            }

            var answer = 0L;
            var sum = sets.Aggregate(0L, (tot, cur) => { answer += tot * cur; return tot + cur; });

            return answer;
        }
        static long DFS(long item, Dictionary<long, List<long>> graph, bool[] visited)
        {
            if (visited[item]) return 0;
            var count = 1L;

            visited[item] = true;
            count = graph[item].Aggregate(count, (sum, cur) => sum + DFS(cur, graph, visited));

            return count;
        }
    }
}
