using System.Collections.Generic;
using System.Linq;

namespace HackerRank
{
    public class TNode<T> : SNode where T : class, new()
    {
        public T data = default(T);
        public delegate bool travFunc(string key, object[] args, out bool cont);
        public static travFunc travValidation;
        private static bool travDefault(string key, object[] args, out bool cont){ cont = true; return true; }
        private static List<T> travResult = new List<T>();
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

        public TNode<T> AddTNode(TNode<T> node, ref bool hasKey)
        {
            TNode<T> res = AddTNode(node.key, ref hasKey);
            res.data = node.data;
            return res;
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
        public List<T> traverse(object[] args)
        {
            if (key.Length == 0) travResult = new List<T>(); // start from the top node all over
            bool cont;
            bool res = (travValidation??travDefault)(key, args, out cont);
            if(res && data != default(T))
            {
                travResult.Add(data);
            }
            if (cont)
            {
                foreach(var child in children)
                {
                    ((TNode<T>)child).traverse(args);
                }
            }
            return travResult;
        }
    }
}
