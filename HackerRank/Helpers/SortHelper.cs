using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class SortHelper
    {
        private int[] temp;
        public SortHelper(int n)
        {
            temp = new int[n + 1];
        }
        public static int[] quickSortPartition(int[] arr, int idx = 0)
        {
            if (idx < 0 || idx > arr.Length - 1) return arr;
            List<int> ret = new List<int>();
            ret.AddRange(arr.Where(a => a < arr[idx]));
            ret.Add(arr[idx]);
            ret.AddRange(arr.Where(a => a > arr[idx]));
            return ret.ToArray();
        }
        public static int[] countingSort(int[] arr)
        {
            int[] res = new int[arr.Max() + 1];
            arr.Aggregate(res, (tot, cur) => { res[cur]++; return res; });
            return res.Select((r, i) => Enumerable.Repeat(i, r)).SelectMany(l => l).ToArray();
        }

        public static IEnumerable<string> countSort(string[] queries)
        {
            List<string>[] ord = new List<string>[queries.Max(q => int.Parse(q.Split(' ')[0])) + 1];
            queries.Aggregate(0, (idx, q) =>
            {
                string[] vals = q.Split(' '); int ordIdx = int.Parse(vals[0]);
                if (ord[ordIdx] == null) ord[ordIdx] = new List<string>();
                ord[ordIdx].Add(idx < queries.Length / 2 ? "-" : vals[1]); return idx + 1;
            });
            return ord.Aggregate(new List<string>(), (lst, cur) => { if (cur != null) lst.AddRange(cur); return lst; });
        }
        public long mergeSort(int[] s, int left, int right)
        {
            long shift = 0;
            if (left < right)
            {
                int mid = left + (right - left) / 2;
                shift += mergeSort(s, left, mid);
                shift += mergeSort(s, mid + 1, right);
                shift += joinParts(s, left, mid + 1, right);
            }
            return shift;
        }
        long joinParts(int[] s, int left, int mid, int right)
        {
            long shift = 0;
            int i = left, j = mid, k = left;
            while (i < mid && j <= right)
            {
                if (s[i] <= s[j])
                {
                    temp[k] = s[i];
                    k++; i++;
                }
                else
                {
                    temp[k] = s[j];
                    k++; j++;
                    shift += mid - i;
                }
            }
            while (i < mid)
            {
                temp[k] = s[i];
                k++; i++;
            }
            while (j <= right)
            {
                temp[k] = s[j];
                k++; j++;
            }
            while (left <= right)
            {
                s[left] = temp[left];
                left++;
            }
            return shift;
        }
    }
}
