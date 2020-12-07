using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class CubeWeghtHelper
    {
        private static bool inRange(string key, object[] limits, out bool cont)
        {
            int k = int.Parse(key.Length < 1 ? "0" : key);
            int from = (int)limits[0], to =(int)limits[1];
            bool ret = k >= from && k <= to;
            cont = k * 10 <= to;
            return ret;
        }
        private static long getCubeWeight(TNode<TNode<TNode<object>>> tree, int[] from, int[] to)
        {
            long res = 0;


            return res;
        }

        public static List<long> getCubes(int n, string[] queries)
        {
            List<long> res = new List<long>();
            TNode<TNode<TNode<object>>> tree = new TNode<TNode<TNode<object>>>();
            TNode<TNode<TNode<object>>>.travValidation = inRange;
            TNode<TNode<object>>.travValidation = inRange;
            TNode<object>.travValidation = inRange;
            int uOffs = "UPDATE ".Length, qOffs = "QUERY ".Length;
            foreach (string q in queries)
            {
                int[] a = Array.ConvertAll(q.Substring(q[0] == 'U' ? uOffs : qOffs).Split(' '), item => Convert.ToInt32(item));
                bool hasRef = false;
                switch(q[0])
                {
                    case 'U':
                        TNode<TNode<TNode<object>>> nodeX = tree.AddTNode(a[0], ref hasRef);
                        if (nodeX.data == null) nodeX.data = new TNode<TNode<object>>();
                        TNode<TNode<object>> nodeY = nodeX.data.AddTNode(a[1], ref hasRef);
                        if (nodeY.data == null) nodeY.data = new TNode<object>();
                        TNode<object> nodeZ = nodeY.data.AddTNode(a[2], ref hasRef);
                        nodeZ.data = a[3];
                        break;
                    case 'Q':
                        int from = Math.Min(a[0], a[3]), to = Math.Max(a[0], a[3]);
                        a[0] = from; a[3] = to;
                        from = Math.Min(a[1], a[4]); to = Math.Max(a[1], a[4]);
                        a[1] = from; a[4] = to;
                        from = Math.Min(a[2], a[5]); to = Math.Max(a[2], a[5]);
                        a[2] = from; a[5] = to;
                        long w = 0;
                        //tree.travResult = new List<TNode<TNode<object>>>();
                        foreach(var yNode in tree.traverse(new object[] { a[0], a[3] }))
                        {
                            //yNode.travResult = new List<TNode<object>>();
                            foreach(var zNode in yNode.traverse(new object[] { a[1], a[4] }))
                            {
                                //zNode.travResult = new List<object>();
                                foreach (object o in zNode.traverse(new object[] { a[2], a[5] }))
                                {
                                    w += long.Parse(o.ToString());
                                }
                            }
                        }
                            //.Select(zNode => zNode.traverse(new object[] { a[2], a[5] })
                            //.Aggregate(0L, (tot, item) => tot + long.Parse(item.ToString())))
                            //.Aggregate(0L, (tot, nx) => tot + nx)).Aggregate(0L, (tot, nx) => tot + nx);
                        res.Add(w);
                        break;
                }
            }
            return res;
        }
    }
}
