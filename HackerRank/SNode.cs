using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class SNode
    {
        public string key = "";
        public int size;
        public List<SNode> children = new List<SNode>();
        public int FindNumOf(string pattern)
        {
            int idx = key.Length;
            if (children.Count < 1) return 0;
            SNode node = children.FirstOrDefault(ch => ch.key[idx] == pattern[idx]);
            return node == null ? 0 : idx == pattern.Length - 1 ? node.size : node.FindNumOf(pattern);
        }
        public bool Add(string newKey, ref bool hasPrefix)
        {
            size++;
            int idx = key.Length;
            bool isLevel = idx == newKey.Length - 1;
            SNode child = children.FirstOrDefault(ch => ch.key[idx] == newKey[idx]);
            if(child != null)
            {
                if (child.children.Count() < 1) hasPrefix = true;
                return isLevel ? !(hasPrefix = true) : child.Add(newKey, ref hasPrefix);
            }
            else
            {
                SNode newNode = new SNode { key = newKey.Substring(0, idx + 1) };
                children.Add(newNode);
                return isLevel ? newNode.size++ == 0 : newNode.Add(newKey, ref hasPrefix);
            }
        }
    }
}
