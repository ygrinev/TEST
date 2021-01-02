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
        public static int findIdxToInsert(int v, ref List<int> lst, bool toInsert = false)
        {
            if (lst.Count() == 0 || v >= lst.Last())
            {
                if(toInsert) lst.Add(v);
                return lst.Count - 1;
            }
            else if(v < lst.First())
            {
                if(toInsert) lst.Insert(0, v);
                return 0;
            }
            // find index inside the list
            int idx = (lst.Count() - 1) / 2;
            for (int iLeft = 0, iRight = lst.Count() - 1; idx > iLeft; idx = (iLeft + iRight) / 2)
            {
                if (v > lst.ElementAt(idx)) iLeft = idx;
                else if (v < lst.ElementAt(idx)) iRight = idx;
                else break;
            }
            if(toInsert) lst.Insert(idx + 1, v); // insert after same or lower value
            return !toInsert && lst.ElementAt(idx) == v ? idx : idx + 1;
        }
        public int getSwapsToSort(int[] arr)
        {
            int count = 0, nextIdx = 0, visited = 0, countRvs = 0, visitedRvs = 0, nextIdxRvs = 0;
            int[][] srtMap = arr.Select((a, i) => new int[] { a, i }).OrderBy(e=>e[0]).ToArray();
            int[][] srtMapRvs = srtMap.Reverse().ToArray();
            int[] tempRvs = new int[arr.Length];
            while(nextIdx < arr.Length || nextIdxRvs < arr.Length)
            {
                if(nextIdx < arr.Length) // asc
                {
                    visited = nextIdx;
                    int idx = nextIdx;
                    int origIdx = srtMap[idx][1];
                    temp[idx] = idx + 1;
                    while(idx != origIdx && origIdx != nextIdx)
                    {
                        count++;
                        temp[origIdx] = origIdx + 1;
                        idx = origIdx;
                        origIdx = srtMap[idx][1];
                    }
                    while(visited < arr.Length - 1 && temp[visited+1] > 0)
                    {
                        visited++;
                    }
                    nextIdx = visited;
                    nextIdx++;
                }
                if (nextIdxRvs < arr.Length) // desc
                {
                    visitedRvs = nextIdxRvs;
                    int idxRvs = nextIdxRvs;
                    int origIdxRvs = srtMapRvs[idxRvs][1];
                    tempRvs[idxRvs] = idxRvs + 1;
                    while (idxRvs != origIdxRvs && origIdxRvs != nextIdxRvs)
                    {
                        countRvs++;
                        tempRvs[origIdxRvs] = origIdxRvs + 1;
                        idxRvs = origIdxRvs;
                        origIdxRvs = srtMapRvs[idxRvs][1];
                    }
                    while (visitedRvs < arr.Length - 1 && tempRvs[visitedRvs + 1] > 0)
                    {
                        visitedRvs++;
                    }
                    nextIdxRvs = visitedRvs;
                    nextIdxRvs++;
                }
            }
            return Math.Min(count, countRvs);
        }
    }
}
