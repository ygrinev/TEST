using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class KidaAdventureHelper    {
        public static long getMaxFinished(int[] arr)
        {
            long idx = 0;
            long max = ArrayManipulationHelper.arrayManipulation(getFinishRange(arr), out idx);
            return idx%arr.Length+1;
        }

        private static int[][] getFinishRange(int[] arr)
        {
            int n = arr.Length;
            List<int[]> rg = new List<int[]>();
            int[] rgLast = null;
            for (int i = 0; i < n; i++)
            {
                if (arr[i] > n - 1) continue;
                if (arr[i] > 0)
                {
                    if (i >= arr[i]) // 2 segments
                    {
                        rg.Add(new int[3]);
                        rgLast = rg.Last();
                        rgLast[0] = 0;
                        rgLast[1] = i - arr[i];
                        rgLast[2] = 1;
                        if (i < n - 1)
                        {
                            rg.Add(new int[3]);
                            rgLast = rg.Last();
                            rgLast[0] = i + 1;
                            rgLast[1] = n - 1;
                            rgLast[2] = 1;
                        }
                    }
                    else
                    {
                        rg.Add(new int[3]);
                        rgLast = rg.Last();
                        rgLast[0] = i + 1;
                        rgLast[1] = i + n - arr[i];
                        rgLast[2] = 1;
                    }
                }
                else
                {
                    rg.Add(new int[3]);
                    rgLast = rg.Last();
                    rgLast[0] = 0;
                    rgLast[1] = n - 1;
                    rgLast[2] = 1;
                }
            }
            return rg.ToArray();
        }
    }
}
