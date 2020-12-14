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
        private GNode[] graph;
        private bool[][] done;
        public void Refresh()
        {
            Array.ForEach(graph, node => { if (node != null) { node.visited = false; node.minDist = 0; } });
        }
        public DijkstraHelper(int n, int[][] edges, int defWeight = 1)
        {
            done = new bool[n + 1][].Select(d => new bool[n + 1]).ToArray();
            graph = new GNode[n+1];
            bool useDef = edges[0].Length <= 2;
            // Create Graph
            foreach(int[] e in edges)
            {
                if (done[e[0]][e[1]]) continue;
                done[e[0]][e[1]] = done[e[1]][e[0]] = true;
                foreach (int i in e) if (graph[i] == null) graph[i] = new GNode();
                //if (graph[e[0]].siblings.Any(s => s.node.id == e[1])) continue;
                for (int j = 0; j < 2; j++)
                {
                    graph[e[j]].siblings.Add(new GEdge { weight = useDef ? defWeight : e[2], node = graph[e[(j + 1) % 2]] });
                }
            }
        }
        public long[] FromNodeBFS(int startNode)
        {
            // Step by step calculate path for every next sibling... 
            if (graph[startNode] == null) return Enumerable.Repeat(-1L, graph.Length - 2).ToArray();
            graph[startNode].visited = true;
            List<GNode> tmp = new List<GNode>();
            tmp.Add(graph[startNode]);
            getAllShortestPaths(tmp);

            return graph.Skip(1).Where((g,i)=>i+1 != startNode).Select(node=>node == null || node.minDist < 1 ? -1L : node.minDist).ToArray();
        }

        private void getAllShortestPaths(List<GNode> tmp)
        {
            List<GNode> newList = new List<GNode>();
            foreach (GNode g in tmp)
            {
                foreach (GEdge e in g.siblings)
                {
                    long path = g.minDist + e.weight;
                    if(!e.node.visited || path < e.node.minDist)
                    {
                        e.node.minDist = path;
                        if (!e.node.visited)
                        {
                            e.node.visited = true;
                            newList.Add(e.node);
                        }
                    }
                }
            }
            if (newList.Count() > 0)
                getAllShortestPaths(newList);
        }
    }
}
