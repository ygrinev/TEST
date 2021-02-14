﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    partial class Solution
    {
        static int[] largestPermutation(int k, int[] arr)
        {
            int idx = 0;
            int[] map = arr.Aggregate(new int[arr.Length + 1], (m, n) => { m[n] = idx++; return m; });
            for (idx = 0; idx < arr.Length && k > 0; idx++)
            {
                if (arr[idx] != arr.Length - idx)
                {
                    k--;
                    int tmp = arr[idx];
                    arr[idx] = arr.Length - idx;
                    arr[map[arr.Length - idx]] = tmp;
                    // update map
                    map[tmp] = map[arr.Length - idx];
                    map[arr.Length - idx] = idx;

                }
            }
            return arr;
        }
        static int maximumToys(int[] prices, int k)
        {
            int sum = 0;
            return prices.OrderBy(p => p).Aggregate(0, (cnt, n) =>
            {
                if (sum + n <= k)
                {
                    sum += n;
                    cnt++;
                }
                return cnt;
            });
        }
        static int getMinimumCost(int k, int[] c)
        {
            int m = c.Length / k + 1;
            int cnt = c.Length % k;
            return c.OrderBy(e => e).Aggregate(0, (sum, p) =>
            {
                if (cnt-- == 0) { cnt = k - 1; m--; }
                return sum + p * m;
            });
        }
        static int maxMin(int k, int[] arr)
        {
            int[] ord = arr.OrderBy(a => a).ToArray();
            int min = int.MaxValue;
            for (int idx = 0; idx <= ord.Length - k; idx++)
            {
                int newMin = ord[idx + k - 1] - ord[idx];
                if (newMin < min)
                    min = newMin;
            }
            return min;
        }
        static int[] jimOrders(int[][] orders)
        {
            return orders.Select((e, i) => new int[] { i + 1, e[0] + e[1], e[0] }).
                OrderBy(it => it[1]).ThenBy(it => it[0]).Select(e => e[0]).ToArray();
        }
    }
}
