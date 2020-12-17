using System.Collections.Generic;

namespace HackerRank.Model
{
    class GNode
    {
        public int id = 0;
        public bool visited = false;
        public long minDist = 0L;
        public List<GNode> siblings = new List<GNode>();
    }
    //class GEdge { public int weight; public GNode node; }
}
