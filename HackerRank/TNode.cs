using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class TNode<T> : SNode where T : class, new()
    {
        public T data = new T();
        public TNode<T> this[string index]
        {
            get => getAt(index);
        }

        public TNode<T> this[int index]
        {
            get => getAt(index.ToString());
        }

        private new TNode<T> getAt(string index)
        {
            return (TNode<T>)base.getAt(index);
        }

        public TNode<T> AddTNode(string newKey, ref bool hasKey)
        {
            size++;
            int idx = key.Length;
            bool isLevel = idx >= newKey.Length - 1;
            TNode<T> child = (TNode<T>)children.FirstOrDefault(ch => ch.key[idx] == newKey[idx]);
            TNode<T> newNode = child == null ? new TNode<T> { key = newKey.Substring(0, idx + 1) } : null;
            if(child == null) children.Add(newNode);
            if (isLevel)
            {
                if (child == null)
                {
                    newNode.size++;
                }
                else 
                    hasKey = true;
            }
            return isLevel ? child??newNode : (child??newNode).AddTNode(newKey, ref hasKey);

        }
        public TNode<T> AddTNode(int newKey, ref bool hasKey)
        {
            return AddTNode(newKey.ToString(), ref hasKey);
        }
    }
}
