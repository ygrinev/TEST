using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Model
{
    public class GNode<T> // where T : class, new()
    {
        public int _key;
        public T _data;
        public int _grundy;
        private List<GNode<T>> _siblings;
        public List<GNode<T>> siblings
        {
            get { return _siblings ?? (_siblings = new List<GNode<T>>()); }
            set { _siblings = value; }
        }
        public GNode(int key, int grundy = 0)
        {
            _key = key;
            _grundy = grundy;
        }
        public GNode(IEnumerable<List<int>> src, int key, List<KeyValuePair<int,T>> data = null) // init the tree from src and root
        {
            _key = key;
            if(data?.Count > 0)
            {
                _data = data.FirstOrDefault(p => p.Key == _key).Value;
            }
            siblings = src.Where(l => l.First() == key).Select(el=>el.ElementAt(1))
            .Aggregate(new List<GNode<T>>(), (sbl, k)=> {
                sbl.Add(new GNode<T>(src.Where(o=>!o.Any(e=>e == key)).ToList(), k, data));
                return sbl;
            });
        }
        public GNode(int n, IEnumerable<KeyValuePair<int, int>> src, out List<GNode<T>> roots)
        {
            bool isInt = typeof(T).Name == "System.Int32";
            GNode<T>[] arr = new GNode<T>[n];
            roots = src.Aggregate(arr, (a, pr) =>
            {
                bool keyEmpty = a[pr.Key] == null, valEmpty = a[pr.Value] == null;
                (a[pr.Key] ?? (a[pr.Key] = new GNode<T>(pr.Key, -2))).siblings.Add(a[pr.Value] ?? (a[pr.Value] = new GNode<T>(pr.Value)));
                if (a[pr.Key]._grundy == 0) a[pr.Key]._grundy = -1;
                if (a[pr.Value]._grundy == -2) a[pr.Value]._grundy = -1;
                return arr;
            }).Where(a=>a._grundy == -2).ToList();
            if (roots.Count > 1) siblings = roots;
            else if(roots.Count == 1)
            {
                GNode<T> root = roots.First();
                _key = root._key;
                _grundy = root._grundy;
                siblings = root.siblings;
                roots = null;
            }
        }
        public int traverse(Func<int, int, int> rcrTrvs, int seed = 0, bool useRoot = true)
        {
            return (useRoot ? (seed == 0 ? (_data is int ? Convert.ToInt32(_data) :_key) : seed) : 0) + siblings.Aggregate(0, (sum, s) => rcrTrvs(sum, s.traverse(rcrTrvs, seed)));
        }
        public int getGrundy(List<GNode<T>> roots)
        {
            return roots.Aggregate(0, (g, r) => g ^ mex(r.siblings));
        }

        private int mex(List<GNode<T>> siblings)
        {
            int ret = 0;
            foreach(int i in siblings.Select(s=>s.siblings == null ? s._grundy : s.getGrundy(s.siblings)).Distinct().OrderBy(g=>g))
            {
                if (ret != i)
                    return i;
                ret++;
            }
            return ret;
        }
    }
    public class GNodeHelper
    {

    }
}
