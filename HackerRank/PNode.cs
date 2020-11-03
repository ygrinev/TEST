using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class PNode : IComparable
    {
        public PNode parent;
        public int count = 1;
        int key;
        public int level = 0;
        public virtual PNode getTopParent()
        {
            PNode tmp = this;
            while (tmp.parent != null)
                tmp = tmp.parent;
            return tmp;  
        }
        public static PNode merge(int i, int j, TNode<PNode> a)
        {
            if (i == j) return null;
            if(i > j) // order: i < j
            {
                int tmp = i; i = j; j = tmp;
            }

            bool hasi = false, hasj = false;
            foreach(int k in new int[] { i, j }) // evaluate
            {
                TNode<PNode> akT = a[k];
                PNode ak = akT?.data;
                if (akT == null)
                {
                    a.AddTNode(k, ref hasi).data = new PNode() { key = k };
                }
                else if (ak.key == 0) 
                    ak.key = k;
            }
            PNode top1 = a[i].data.getTopParent(),
                  top2 = a[j].data.getTopParent();
            if (!top1.Equals(top2))
            {
                //*******************************************************************
                if (top2.level > top1.level)
                {
                    PNode tmp = top2;
                    top2 = top1;
                    top1 = tmp;
                }
                top2.parent = top1;
                top1.key = Math.Min(top1.key, top2.key);
                top1.count += top2.count;
                if (top1.level == top2.level) top1.level++;
                return top1;
            }
            return null;
        }

        public override bool Equals(object obj)
        {
            return key == ((PNode)obj).key;
        }

        public override int GetHashCode()
        {
            return 0;
        }

        public int CompareTo(object obj)
        {
            int count2 = ((PNode)obj)?.count??1;
            return count > count2 ? 1 : count < count2 ? -1 : 0;
        }
    }
}
