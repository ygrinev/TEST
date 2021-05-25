using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Model
{
    public class GNode<T> // where T : class, new()
    {
        public int _key;
        public T _data;
        public List<GNode<T>> siblings = new List<GNode<T>>();
        public GNode(IEnumerable<List<int>> src, int key) // init the tree from src and root
        {
            _key = key;
            siblings = src.Where(o => o.Any(k => k == key)).Select(ok=>ok.First(k=>k != key))
            .Aggregate(new List<GNode<T>>(), (sbl, k)=> {
                sbl.Add(new GNode<T>(src.Where(o=>!o.Any(e=>e == key)).ToList(), k));
                return sbl;
            });
        }
        public int traverse(Func<int, int, int> rcrTrvs, int seed = 0, bool useRoot = true)
        {
            return (useRoot ? seed : 0) + siblings.Aggregate(0, (sum, s) => rcrTrvs(sum, s.traverse(rcrTrvs, seed)));
        }
    }
}
