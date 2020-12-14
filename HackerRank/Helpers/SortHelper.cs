using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class SortHelper
    {
        public static int[] quickSortPartition(int[] arr, int idx = 0)
        {
            if (idx < 0 || idx > arr.Length - 1) return arr;
            List<int> ret = new List<int>();
            ret.AddRange(arr.Where(a => a < arr[idx]));
            ret.Add(arr[idx]);
            ret.AddRange(arr.Where(a => a > arr[idx]));
            return ret.ToArray();
        }
    }
}
