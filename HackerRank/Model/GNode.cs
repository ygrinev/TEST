using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Model
{
    class GNode
    {
        public int id;
        public bool visited = false;
        public long minDist = 0L;
        public List<GEdge> siblings = new List<GEdge>();
    }
    class GEdge { public int weight; public GNode node; }
}
