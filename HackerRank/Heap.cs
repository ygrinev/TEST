using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    public class MinHeap<T> where T : IComparable
    {
        private List<T> elements = new List<T>();

        public int GetSize()
        {
            return elements.Count;
        }

        public T GetMin()
        {
            return elements.Count > 0 ? elements[0] : default(T);
        }

        public bool Find(T t, out T res, int index = 0)
        {
            res = default;
            if (index < 0 || index > elements.Count() - 1) return false;
            T found = elements[index];
            if (t.CompareTo(found) == 0)
            {
                res = found;
                return true;
            }
            var left = GetLeft(index);
            var right = GetRight(index);

            if (left >= GetSize() || elements[left].CompareTo(t) > 0)
            {
                left = -1;
            }

            if (right >= GetSize() || elements[right].CompareTo(t) > 0)
            {
                right = -1;
            }
            return Find(t, out res, left) ? true : Find(t, out res, right);
        }

        public void Add(T item)
        {
            elements.Add(item);
            HeapifyUp(elements.Count - 1);
        }

        public T PopMin()
        {
            if (elements.Count > 0)
            {
                T item = elements[0];
                elements[0] = elements[elements.Count - 1];
                elements.RemoveAt(elements.Count - 1);

                HeapifyDown(0);
                return item;
            }

            throw new InvalidOperationException("no element in heap");
        }

        private void HeapifyUp(int index)
        {
            var parent = GetParent(index);
            if (parent >= 0 && elements[index].CompareTo(elements[parent]) < 0)
            {
                var temp = elements[index];
                elements[index] = elements[parent];
                elements[parent] = temp;

                HeapifyUp(parent);
            }
        }

        private void HeapifyDown(int index)
        {
            var smallest = index;

            var left = GetLeft(index);
            var right = GetRight(index);

            if (left < GetSize() && elements[left].CompareTo(elements[index]) < 0)
            {
                smallest = left;
            }

            if (right < GetSize() && elements[right].CompareTo(elements[smallest]) < 0)
            {
                smallest = right;
            }

            if (smallest != index)
            {
                var temp = elements[index];
                elements[index] = elements[smallest];
                elements[smallest] = temp;

                HeapifyDown(smallest);
            }

        }

        private int GetParent(int index)
        {
            if (index <= 0)
            {
                return -1;
            }

            return (index - 1) / 2;
        }

        private int GetLeft(int index)
        {
            return 2 * index + 1;
        }

        private int GetRight(int index)
        {
            return 2 * index + 2;
        }
    }

    public class MaxHeap<T> where T : IComparable
    {
        private List<T> elements = new List<T>();

        public int GetSize()
        {
            return elements.Count;
        }

        public T GetMax()
        {
            return elements.Count > 0 ? elements[0] : default(T);
        }

        public void Add(T item)
        {
            elements.Add(item);
            HeapifyUp(elements.Count - 1);
        }

        public T PopMax()
        {
            if (elements.Count > 0)
            {
                T item = elements[0];
                elements[0] = elements[elements.Count - 1];
                elements.RemoveAt(elements.Count - 1);

                HeapifyDown(0);
                return item;
            }

            throw new InvalidOperationException("no element in heap");
        }

        private void HeapifyUp(int index)
        {
            var parent = GetParent(index);
            if (parent >= 0 && elements[index].CompareTo(elements[parent]) > 0)
            {
                var temp = elements[index];
                elements[index] = elements[parent];
                elements[parent] = temp;

                HeapifyUp(parent);
            }
        }

        private void HeapifyDown(int index)
        {
            var largest = index;

            var left = GetLeft(index);
            var right = GetRight(index);

            if (left < GetSize() && elements[left].CompareTo(elements[index]) > 0)
            {
                largest = left;
            }

            if (right < GetSize() && elements[right].CompareTo(elements[largest]) > 0)
            {
                largest = right;
            }

            if (largest != index)
            {
                var temp = elements[index];
                elements[index] = elements[largest];
                elements[largest] = temp;

                HeapifyDown(largest);
            }

        }

        private int GetParent(int index)
        {
            if (index <= 0)
            {
                return -1;
            }

            return (index - 1) / 2;
        }

        private int GetLeft(int index)
        {
            return 2 * index + 1;
        }

        private int GetRight(int index)
        {
            return 2 * index + 2;
        }
    }
}
