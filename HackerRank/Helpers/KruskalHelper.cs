using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class KruskalHelper
    {
        public static MinHeap<Triplet> minHeap = new MinHeap<Triplet>();
        public static long getMSTWeight(int n, string[] queries)
        {
            long res = 0;
            TreeHelper tree = new TreeHelper(n);
            foreach(var q in queries)
            {
                minHeap.Add(new Triplet(q));
            }
            int count = 1;
            while(minHeap.GetSize() > 0 && count < n)
            {
                Triplet t = minHeap.PopMin();
                int leaf = 0;
                bool ok = tree.merge(t[0], t[1], out leaf);
                if(ok)
                {
                    res += t[2];
                    count++;
                }
            }
            return res;
        }
    }
}
