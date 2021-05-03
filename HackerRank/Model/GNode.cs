using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerRank.Model
{
    public class GNode<T>  where T : class, new()
    {
        public T _data;
        public List<GNode<T>> siblings = new List<GNode<T>>();
        public GNode(object src, T data, Func<object, T, List<T>> recur)
        {
            _data = data;
            siblings = recur(src, data).Aggregate(new List<GNode<T>>(), (sbl, cur)=> {
                sbl.Add(new GNode<T>(src, cur, recur));
                return sbl;
            }); // init the tree from src and root
        }
    }
    //class GEdge { public int weight; public GNode node; }
}
