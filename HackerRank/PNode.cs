using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class PNode
    {
    public PNode parent;
    public int count = 1;
    int key = 0;
    int level = 0;
    public PNode getTopParent()
    {
        PNode tmp = this.parent;
        while (tmp?.parent != null)
            tmp = tmp.parent;
        return tmp;  
    }
    public static void merge(int i, int j, PNode[] a)
    {
        if (i == j) return;
        PNode top1 = (a[i] ?? (a[i] = new PNode() { key = i})).getTopParent(), 
                top2 = (a[j] ?? (a[j] = new PNode() { key = j})).getTopParent();
        if (top1 == null && top2 == null)
        {
            int i1 = Math.Min(i, j);
            int i2 = i > i1 ? i : j;
            a[i1].count = 2;
            a[i1].level = 1;
            a[i2].parent = a[i1];
        }
        else if (top1 == null || top2 == null)
        {
            (top1 ?? top2).count++;
            (top1 ?? top2).key = top1 == null ? Math.Min(i, top2.key) : Math.Min(j, top1.key);
            a[top1 == null ? i : j].parent = top1??top2;
        }
        else if (!top1.Equals(top2))
        {
            //*******************************************************************
            if(top2.level > top1.level)
            {
                PNode tmp = top2;
                top2 = top1;
                top1 = tmp;
            }
            top2.parent = top1;
            top1.key = Math.Min(top1.key, top2.key);
            top1.count += top2.count;
            if (top1.level == top2.level) top1.level++;
        }
    }
    public override bool Equals(object obj)
    {
        return key == ((PNode)obj).key;
    }

    public override int GetHashCode()
    {
        return 0;
    }
    }
}
