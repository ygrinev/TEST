using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class MedianHelper
    {
        MinHeap<int> maxs = new MinHeap<int>();
        MaxHeap<int> mins = new MaxHeap<int>();
        private void addNum(int k)
        {
            if (k > mins.GetMax())
            {
                maxs.Add(k);
                if (maxs.GetSize() - mins.GetSize() > 1)
                {
                    mins.Add(maxs.PopMin());
                }
            }
            else
            {
                mins.Add(k);
                if (mins.GetSize() - maxs.GetSize() > 1)
                {
                    maxs.Add(mins.PopMax());
                }
            }
        }
        private double getMedian()
        {
            int minSz = mins.GetSize(), maxSz = maxs.GetSize();
            int loMid = mins.GetMax(), hiMid = maxs.GetMin();
            bool even = (loMid + hiMid) % 2 == 0;
            return minSz > maxSz ? loMid : minSz < maxSz ? hiMid : (loMid + hiMid) / 2.0;
        }
        public int[] runningMedian(int[] a)
        {
            List<int> res = new List<int>();
            //******************    OPTIMIZED WITH HEAPS    *****************
            for (int i = 0; i < a.Length; i++)
            {
                addNum(a[i]);
                res.Add((int)getMedian());
            }
            return res.ToArray();
        }
        private void Remove(int k)
        {
            if (k <= mins.GetMax()) mins.Remove(k);
            else maxs.Remove(k);
        }
        public int activityNotifications(int[] xd, int d)
        {
            if (d >= xd.Length) return 0;
            int idx = 0,
                count = 0;
            while(idx < d)
            {
                addNum(xd[idx++]);
            }
            while(idx < xd.Length)
            {
                double mdn = getMedian();
                if ((int)Math.Round(2 * mdn) <= xd[idx]) count++;
                if(idx < xd.Length - 1)
                {
                    Remove(xd[idx - d]);
                    addNum(xd[idx]);
                }
                idx++;
            }
            return count;
        }
        public int activityNotifications2(int[] xd, int d)
        {
            if (d >= xd.Length) return 0;
            int count = 0;
            int dlt1 = (int)Math.Floor((d - 1) / 2.0), dlt2 = (int)Math.Ceiling((d - 1) / 2.0);
            // sort first d elements of the array xd
            List<int> ord = new List<int>();
            ord.AddRange(xd.Take(d).OrderBy(el => el));
            // loop from idx = d to xd.Length - 1
            for(int idx = d; idx < xd.Length; idx++)
            {
                // find 2*median
                int med2 = ord[dlt1] + ord[dlt2];
                // compare 2*med VS xd[idx] => count
                if (med2 <= xd[idx]) count++;
                //  if idx < xd.Length-1 remove from sorted array xd[idx - d] and add xd[idx]
                if(idx < xd.Length-1)
                {
                    int rmvIdx = SortHelper.findIdxToInsert(xd[idx - d], ref ord, false);
                    ord.RemoveAt(rmvIdx);
                    int insIdx = SortHelper.findIdxToInsert(xd[idx], ref ord, true);
                }
            }
            return count;
        }
    }
}
