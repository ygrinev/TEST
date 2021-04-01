using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    partial class Solution
    {
        static string morganAndString(string a, string b)
        {
            int aLen = a.Length, bLen = b.Length;
            StringBuilder sb = new StringBuilder();
            int i = 0, j = 0;
            while (i < aLen && j < bLen)
            {
                if (a[i] != b[j])
                {
                    sb.Append(a[i] < b[j] ? a[i++] : b[j++]);
                }
                else
                {
                    bool isA = strCompare(a, i + 1, b, j + 1);
                    string s = isA ? a : b;
                    int idx = isA ? i : j, len = isA ? aLen : bLen;

                    sb.Append(s[idx++]);
                    while (idx < len && s[idx] == s[idx - 1])
                    {
                        sb.Append(s[idx++]);
                    }
                    if (isA) i = idx;
                    else j = idx;
                }
            }
            sb.Append(i < aLen ? a.Substring(i, aLen - i) : j < bLen ? b.Substring(j, bLen - j) : "");
            return sb.ToString();

        }

        private static bool strCompare(string a, int i, string b, int j)
        {
            while (i < a.Length && j < b.Length)
            {
                if (a[i] < b[j]) return true;
                if (a[i] > b[j]) return false;
                i++; j++;
            }
            return i < a.Length;
        }

        static string morganAndString3(string a, string b)
        {
            int i = 0, j = 0, aLen = a.Length, bLen = b.Length;
            StringBuilder sb = new StringBuilder();
            List<int> l = null;
            if (l?.Any()??false)
            { }
            while (i < aLen && j < bLen)
            {
                if (a[i] != b[j])
                {
                    sb.Append(a[i] > b[j] ? b[j++] : a[i++]);
                }
                else
                {
                    int aEq = i, bEq = j, offs = -1;
                    char ch = a[i];
                    while (i < aLen && j < bLen && a[i] == b[j] && a[i] <= ch)
                    {
                        if (a[i] == ch)
                        {
                            if (i > aEq && a[i - 1] < ch)
                            {
                                offs = i - aEq;
                            }
                        }
                        else
                        {
                            if (offs > -1)
                            {
                                if (a[i] > a[i - offs])
                                {   // flush both for offs
                                    sb.Append(a.Substring(aEq, offs));
                                    sb.Append(a.Substring(aEq, offs));
                                    aEq += offs;
                                    bEq += offs;
                                    offs = -1;
                                }
                                else if (a[i] < a[i - offs])
                                {
                                    offs = -1;
                                }
                            }
                        }
                        i++; j++;
                    }
                    if (i == aLen && j == bLen
                    || i == aLen && ch < b[j]
                    || j == bLen && ch < a[i]
                    || ch < a[i] && ch < b[j])
                    {   // flush both
                        sb.Append(a.Substring(aEq, i - aEq));
                        sb.Append(a.Substring(aEq, i - aEq));
                    }
                    else if (i == aLen && ch >= b[j]
                        || j == bLen && ch >= a[i])
                    {
                        sb.Append(a.Substring(aEq, i - aEq));
                        if (i == aLen) i = aEq;  // flush b, reverse a
                        else j = bEq; // flush a, reverse b
                    }
                    else
                    {
                        sb.Append(a.Substring(aEq, i - aEq));
                        if (a[i] > b[j]) i = aEq;  // flush b, reverse a
                        else j = bEq; // flush a, reverse b
                    }
                }
            }
            sb.Append(i < aLen ? a.Substring(i, aLen - i) : j < bLen ? b.Substring(j, bLen - j) : "");
            return sb.ToString();
        }

        static string[] bigSorting(string[] unordered)
        {
            return unordered.Count() > 199000 ? unordered.OrderBy(s => BigInteger.Parse(s)).ToArray() : unordered.OrderBy(s => s.Length).ThenBy(s => s).ToArray();
        }
        static int hackerlandRadioTransmitters(int[] x, int k)
        {   // [ 2, *4, 5, 6, *9, *12, *15 ]
            if (x.Length < 4 && x.Max() - x.Min() <= k) return 1;
            int i = 0, iLastTrsm = -1, iNewGrp = 0, distFromLastCvrg = k + 1;
            x = x.OrderBy(el => el).ToArray();
            return x.Aggregate(0, (cnt, l) => {
                if (i == 0) { i++; return cnt; }
                if (x[i] - x[i - 1] > k)
                {
                    iNewGrp = i;
                    if (iLastTrsm < 0 || distFromLastCvrg > 0)
                    {
                        iLastTrsm = i - 1;
                        cnt++;
                    }
                    distFromLastCvrg = x[i] - x[iLastTrsm] - k;
                }
                else
                {
                    if (iLastTrsm >= iNewGrp && (distFromLastCvrg = x[i] - x[iLastTrsm] - k) > 0)
                    {
                        iNewGrp = i;
                    }
                    if (iLastTrsm < iNewGrp && x[i] - x[iNewGrp] > k)
                    {
                        iLastTrsm = i - 1;
                        distFromLastCvrg = x[i] - x[iLastTrsm] - k;
                        cnt++;
                    }
                }
                if (i == x.Length - 1 && distFromLastCvrg > 0) cnt++;
                i++;
                return cnt;
            });
        }
        static long roadsAndLibraries(int n, int c_lib, int c_road, int[][] cities)
        {
            if (c_lib <= c_road) return c_lib * (long)n;
            TreeHelper forest = new TreeHelper(n);
            long maxRoads = n * ((long)n - 1) / 2;
            int _d;
            long actRoads = cities.Aggregate(0L, (sum, pair) => sum + (forest.merge(pair[0], pair[1], out _d) ? 1 : 0));
            return forest.countTopParents() * (long)c_lib + actRoads * c_road;
        }
        static int stringSimilarity(string s)
        {
            char c0 = s[0];
            int idx = 0, lastIdx = 0;
            int[] sums = new int[s.Length];
            int[] offs = s.Aggregate(new int[s.Length], (arr, c) =>
            {
                for(int curIdx = lastIdx, prevIdx = lastIdx; curIdx > 0; prevIdx = curIdx, curIdx = arr[curIdx])
                {
                    if(idx == curIdx) continue;
                    if (s[idx - curIdx] == s[idx])
                        sums[curIdx]++;
                    else
                    {
                        if (curIdx == lastIdx) lastIdx = arr[lastIdx];
                        else
                        {
                            arr[prevIdx] = arr[curIdx];
                            curIdx = arr[curIdx];
                            if (curIdx < 0)
                                break;
                        }
                    }
                }
                arr[idx] = c == c0 && idx > 0 ? lastIdx : -1;
                if (c == c0)
                {
                    lastIdx = idx;
                    sums[idx] = idx == 0 ? s.Length : 1; // 
                }
                idx++;
                return arr;
            });
            if (lastIdx == 0) return s.Length;
            return sums.Sum();
        }
        static long stringSimilarity2(String str)
        {
            long c = str.Length;
            int L = 0, R = 0, n = str.Length;
            char[] s = str.ToCharArray();
            int[] z = new int[n];
            for (int i = 1; i < n; i++)
            {
                if (i > R)
                {
                    L = R = i;
                    while (R < n && s[R - L] == s[R]) R++;
                    z[i] = R - L; R--;
                    c += z[i];
                }
                else
                {
                    int k = i - L;
                    if (z[k] < R - i + 1)
                    {
                        z[i] = z[k];
                        c += z[i];
                    }
                    else
                    {
                        L = i;
                        while (R < n && s[R - L] == s[R]) R++;
                        z[i] = R - L;
                        c += z[i];
                        R--;
                    }
                }
            }
            return c;
        }
        static long gridlandMetro(int n, int m, int k, int[][] track)
        {
            int rowIdx = -1, curIdx = 0; ;
            Dictionary<int, int> row = new Dictionary<int, int>();
            long tot = n * (long)m;
            return k == 0 ? tot : track.OrderBy(t => t[0]).Aggregate(tot, (cnt, crds) => {
                k--;
                curIdx = crds[0] - 1;
                if (curIdx > rowIdx)
                {
                    if (rowIdx >= 0)
                    {
                        cnt -= countTakenCells(m, row);
                    }
                    while (curIdx > ++rowIdx) continue;
                    row.Clear();

                }
                row[crds[1] - 1] = (row.ContainsKey(crds[1] - 1) ? row[crds[1] - 1] : 0) + 1;
                if (crds[2] < m) row[crds[2]] = (row.ContainsKey(crds[2]) ? row[crds[2]] : 0) - 1;
                if (k == 0) // last track
                {
                    cnt -= countTakenCells(m, row);
                }
                return cnt;
            });
        }

        private static int countTakenCells(int m, Dictionary<int, int> row)
        {
            int cnt = 0, trVal = 0, idx = 0, len = row.Keys.Count;
            foreach (int key in row.Keys.OrderBy(k => k))
            {
                len--;
                cnt += (key - idx) * (trVal > 0 ? 1 : 0);
                idx = key;
                trVal += row[key];
                if(len == 0)
                    cnt += (m - key) * (trVal > 0 ? 1 : 0);
            }
            return cnt;
        }
    }
}
