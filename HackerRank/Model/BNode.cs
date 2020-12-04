using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    class BNode<T> : IComparable where T : class, new()
    {
        public int key = 0;
        public BNode<T> left = null, right = null;
        public int CompareTo(object obj)
        {
            int key1 = ((BNode<T>)obj).key;
            return key < key1 ? -1 : key == key1 ? 0 : 1;
        }
        public bool Add(BNode<T> node, out BNode<T> found)
        {
            found = Find(node);
            if (found.key == node.key)
                return true;
            return false;
        }

        private BNode<T> Find(BNode<T> node)
        {
            if (key == node.key || (left == default(T) || left.key > node.key) && (right == default(T) || right.key < node.key))
                return this;
            return left != default(T) ? left.Find(node) : right.Find(node);
        }
    }
}
