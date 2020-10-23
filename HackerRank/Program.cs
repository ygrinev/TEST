using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;
    using System.Text;
    using System;
    using System.CodeDom;

    class Node
    {
        public Node left, right;
        public int data;
        public Node(int data)
        {
            this.data = data;
            left = right = null;
        }
    }

    class Solution
    {
        //static void minimumBribes(int[] q)
        //{
        //    int bribe = 0;
        //    bool chaotic = false;
        //    int n = q.Length;
        //    for (int i = 0; i < n; i++)
        //    {
        //        if (q[i] - (i + 1) > 2)
        //        {
        //            chaotic = true;
        //            break;
        //        }
        //        for (int j = Math.Max(0, q[i] - 2); j < i; j++)
        //            if (q[j] > q[i])
        //                bribe++;
        //    }
        //    if (chaotic)
        //        Console.WriteLine("Too chaotic");
        //    else
        //        Console.WriteLine(bribe);
        //}

        static void minimumBribes(int[] q)
        {
            int n = q.Length;
            Dictionary<int, Dictionary<int, long>> segs = new Dictionary<int, Dictionary<int, long>>{ { 0, new Dictionary<int, long>{ { n, 0L } } }};
            var idx = segs.ElementAt(0);
            if (n < 2) return;
            Dictionary<int, int> cntr = new Dictionary<int, int>();
            int count = 10, total = 0;
            // bubble sort
            for (int i = 0; i < n - 1 && count > 0; i++)
            {
                count = 0;
                for (int j = 0; j < n - 1; j++)
                {
                    if (q[j] > q[j + 1])
                    {
                        count++;
                        int tmp = q[j];
                        q[j] = q[j + 1];
                        q[j + 1] = tmp;
                        //cntr[q[j]] = cntr.ContainsKey(q[j]) ? (cntr[q[j]] + 1) : 1;
                        cntr[q[j + 1]] = cntr.ContainsKey(q[j + 1]) ? (cntr[q[j + 1]] + 1) : 1;
                    }
                }
                total += count;
            }
            Console.WriteLine(total);
            if (cntr.Keys.Select((key) => cntr[key]).Max() > 2)
            {
                Console.WriteLine("Too chaotic");
            }
        }
        interface IVehicle
        {
            int GetHorsepower();
        }

        public class Car : IVehicle
        {
            int horsepower;
            public Car()
            {
                horsepower = 100;
            }

            int IVehicle.GetHorsepower()
            {
                return horsepower;
            }
        }
        static int minVal(int[] A)
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            //int num = list.ElementAt(list.Count);
            Random i = new Random();
            int v = Math.Abs(i.Next());
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int n = 1;
            foreach(var num in A.OrderBy(ii => ii))
            {
                if (num < 0)
                    continue;
                if (n < num)
                    return n;
                else
                    n = num + 1;

            }
            return n;
        }

        static int divisorSum(int n)
        {
            int sqrt = (int)Math.Sqrt(n);
            int sum = 0;
            int div = 0;
            while (++div <= sqrt)
            {
                if (n % div == 0)
                {
                    int div2 = n / div;
                    sum += div + (div == div2 ? 0 : div2);
                }
            }
            return sum;
        }

        static void PrintArray<T>(T[] A)
        {
            Array.ForEach(A, a => Console.WriteLine(a));
        }
        static int getHeight(Node root, int h = -1)
        {
            return root == null ? Math.Max(h, 0)
              : Math.Max(getHeight(root.left, h + 1), getHeight(root.right, h + 1));
        }

        static int GetFeeOnReturn(DateTime expDate, DateTime retDate)
        {
            int y1 = expDate.Year, y2 = retDate.Year, m1 = expDate.Month, m2 = retDate.Month, d1 = expDate.Day, d2 = retDate.Day;

            return y2 > y1 ? 10000 
                : (y2 == y1 && m2 > m1) ? (m2 - m1) * 500 
                    : (y2 == y1 && m2 == m1 && d2 > d1) ? (d2 - d1) * 15 : 0;
        }

        static string dayOfProgrammer(int year) // 256 th day of a year
        {
            string leapYearDay = $"12.09.{year}", regYearDay = $"13.09.{year}",
                    y1918Day = $"26.09.{year}";
            return year > 1918 && (year % 400 == 0 || year % 4 == 0 && year % 100 != 0) || year < 1918 && year % 4 == 0 ? leapYearDay : year == 1918 ? y1918Day : regYearDay;
        }

        static int GetMinValOfMaxCount(List<int> arr)
        {
            return arr.GroupBy(i => i).Select(g => new { Val = g.Key, Size = g.Count() }).OrderByDescending(p => p.Size).ThenBy(p => p.Val).FirstOrDefault().Val;

            //Dictionary<int, int> groups = new Dictionary<int, int>();
            //int minVal = 0, maxCount = 0;
            //foreach (int i in arr)
            //{
            //    if (groups.ContainsKey(i))
            //        groups[i] = groups[i] + 1;
            //    else
            //        groups[i] = 1;
            //    if (groups[i] > maxCount || groups[i] == maxCount && i < minVal)
            //    {
            //        maxCount = groups[i];
            //        minVal = i;
            //    }
            //}
            //return minVal;

        }

        static int sockMerchant(int n, int[] ar)
        {
            return ar.GroupBy(k => k).Select(g => g.Count() / 2).Sum();
        }

        static int pageCount(int n, int p) //min flips 1 by 1 after opened at the start or end, page #1 - on the right
        {
            return Math.Min((n - n % 2 + 1 - p) / 2, p / 2);
        }

        static int getMoneySpent(int[] keyboards, int[] drives, int b)
        {
            try
            {
                return (from k in keyboards
                        from d in drives
                        select k + d).Where(e => e <= b).Max();
            }
            catch
            {
                return -1;
            }
        }

        static int beautifulDays(int i, int j, int k)
        {
            int count = 0;
            while (i <= j)
            {
                if (IsBeautiful(i++, k))
                {
                    count++;
                }
            }
            return count;
        }

        static bool IsBeautiful(int n, int k)
        {
            return Math.Abs(ReverseNum(n) - n) % k == 0;
        }

        static long ReverseNum(int n)
        {
            long r = 0;
            while (n > 0)
            {
                r = r * 10 + n % 10;
                n /= 10;
            }
            return r;
        }

        static int[] circularArrayRotation(int[] a, int k, int[] queries) // return idx to a-val map [queries] after k-right cyc rotation
        {
            return queries.Select((e, i) => a[(e + a.Length - (k % a.Length)) % a.Length]).ToArray();
        }

        static void extraLongFactorials(int n)
        {
            Dictionary<int, ulong> dict = new Dictionary<int, ulong> { { 0, 1 } };
            ulong rmdr = 0, div = 10000000000;
            while (n > 1)
            {
                for (int i = 0; i < dict.Keys.Count; i++)
                {
                    dict[i] = dict[i] * (ulong)n + rmdr;
                    rmdr = dict[i] / div;
                    dict[i] %= div;
                }
                n--;
                if (rmdr != 0) // add 1 more dict entry
                {
                    dict[dict.Keys.Max() + 1] = rmdr;
                    rmdr = 0;
                }
            }
            StringBuilder sb = new StringBuilder();
            for (int k = dict.Keys.Max(); k >= 0; k--)
            {
                string toAppend = k == dict.Keys.Max() ? $"{dict[k]}" : $"{dict[k]}".PadLeft(10, '0');
                sb.Append(toAppend);
            }
            Console.WriteLine(sb);
        }

        static int squares(int a, int b) // count sqrts in the range
        {
            return Math.Max(0, 1 + (int)Math.Floor(Math.Sqrt(b)) - (int)Math.Ceiling(Math.Sqrt(a)));
        }

        static int libraryFine(int d1, int m1, int y1, int d2, int m2, int y2)
        {
            //DateTime ret = new DateTime(y1, m1, d1), due = new DateTime(y2, m2, d2);
            return y1 < y2 || y1 == y2 && m1 < m2 || y1 == y2 && m1 == m2 && d1 <= d2 ? 0
            : y1 > y2 ? 10000 : m1 > m2 ? (m1 - m2) * 500 : (d1 - d2) * 15;
        }

        static int chocolateFeast(int n, int c, int m) // n=$$$, c=cost of 1 chocolate, m - num of wrappers exchanged to 1 chocolate
        {
            int wrCount = n / c;
            int total = wrCount;
            while (wrCount >= m)
            {
                int newWraps = wrCount / m;
                total += newWraps;
                wrCount = newWraps + wrCount % m;
            }
            return total;
        }

        static int[] serviceLane(int[] width, int[][] cases)
        {
            return cases.Select(sePair => width.Skip(sePair[0]).Take(sePair[1] - sePair[0] + 1).Min()).ToArray();
        }

        static int workbook(int n, int k, int[] arr)
        {
            int count = 0, curPage = 0, pageOffs = 0;
            for (int i = 0; i < arr.Length; i++) // chapters
            {
                curPage++;
                pageOffs = 0;
                for (int j = 0; j < arr[i]; j++) // ch problems
                {
                    pageOffs++;
                    if (pageOffs > k)
                    {
                        curPage++;
                        pageOffs = 1;
                    }
                    if (j + 1 == curPage) count++;
                }
            }
            return count;
        }

        static int flatlandSpaceStations(int n, int[] c)
        {
            int count = 0, max = 0, left = 0, mid = 0, leftDist = 0, rightDist = 0;
            int[] ord = c.OrderBy(e => e).ToArray();
            foreach (int i in ord)
            {
                if (count++ == 0)
                {
                    left = 0;
                    mid = i;
                    if (c.Length == 1) max = Math.Max(i, (n - i - 1));
                    continue;
                }

                leftDist = (mid - left) / (count == 2 ? 1 : 2);
                rightDist = (i - mid) / 2;
                if (leftDist > max) max = leftDist;
                if (rightDist > max) max = rightDist;
                if (count == c.Length && max < n - i - 1) max = n - i - 1;

                left = mid;
                mid = i;
            }
            return max;
        }

        static int fairRations(int[] B)
        {
            int count = 0, prev = 0;
            for (int i = 0; i < B.Length; i++)
            {
                bool even = (B[i] + prev) % 2 == 0;
                if (i == B.Length - 1)
                {
                    return even ? count : -1;
                }
                if (even)
                {
                    prev = 0;
                    continue;
                }
                count += 2;
                prev = 1;
            }
            return count;
        }

       static string[] cavityMap(string[] grid)
        {
            //if (grid.Length < 3 || grid[1].Length < 3) return grid;
            for (int i = 1; i < grid.Length - 1; i++)
            {
                for (int j = 1; j < grid[i].Length - 1; j++)
                {
                    string prev = grid[i - 1], cur = grid[i], next = grid[i + 1];
                    int mid = cur[j], top = prev[j], btm = next[j], lft = cur[j-1], rht = cur[j+1];
                    if(mid > top && mid > btm && mid > lft && mid > rht)
                    {
                        grid[i] = cur.Substring(0, j) + "X" + cur.Substring(j+1, cur.Length - j - 1);
                    }
                }
            }
            return grid;
        }

        static int[] stones(int n, int a, int b)
        {
            int step = Math.Max(a, b) - Math.Min(a, b), min = Math.Min(a, b) * (n - 1);
            return Enumerable.Range(1, step == 0 ? 1 : n).Select(x => min + (x - 1) * step).ToArray();
        }

        static int[] acmTeam(string[] topic)
        {
            int max = 0, nTeams = 0;
            for (int i = 0; i < topic.Length - 1; i++)
            {
                for (int j = i + 1; j < topic.Length; j++)
                {
                    int count = 0;
                    for (int k = 0; k < topic[j].Length; k++)
                    {
                        if (topic[i][k] == '1' || topic[j][k] == '1')
                            count++;
                    }
                    if (count > max)
                    {
                        max = count;
                        nTeams = 1;
                    }
                    else if (count == max)
                    {
                        nTeams++;
                    }
                }
            }
            return new int[] { max, nTeams };
        }

        static string gridSearch(string[] G, string[] P)
        {
            for (int i = 0; i <= G.Length - P.Length; i++)
            {
                for (int ii = 0; ii <= G[i].Length - P[0].Length; ii++)
                {
                    bool ok = true;
                    for (int j = 0; ok && j < P.Length; j++)
                    {
                        for (int jj = 0; ok && jj < P[j].Length; jj++)
                        {
                            if (P[j][jj] == G[i + j][ii + jj])
                            {
                                if (j == P.Length - 1
                                && jj == P[j].Length - 1)
                                    return "YES";
                            }
                            else
                            {
                                ok = false;
                            }
                        }
                    }
                }
            }
            return "NO";
        }

        private static string[] units = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
        private static string[] dzUnits = { "twenty", "thirty", "forty", "fifty" };
        static string timeToWords(int h, int m)
        {
            if (m > 30)
            {
                h = h % 11 + 1;
            }
            return $"{(m == 0 ? "" : m == 15 ? "quarter past " : m == 30 ? "half past " : m == 45 ? "quarter to " : m == 1 ? "one minute past " : m == 59 ? "one minute to " : m < 20 ? units[m] + " minutes past " : m < 31 ? dzUnits[m / 10 - 2] + " " + units[m % 10] + " minutes past " : (m > 40 ? units[60 - m] : dzUnits[(60 - m) / 10 - 2] + " " + units[(60 - m) % 10]) + " minutes to ")}{(h < 20 ? units[h] : dzUnits[h / 10 - 2] + " " + units[h % 10])}{(m == 0 ? " o' clock" : "")}";
        }

        static string kaprekarNumbers(int p, int q)
        {
            List<string> lst = new List<string>();
            for (int i = p; i <= q; i++)
            {
                string sNum = ((long)i * i).ToString();
                int len = sNum.Length, len2 = i.ToString().Length, len1 = len - len2;
                int i1 = int.TryParse(sNum.Substring(0, len1), out i1) ? i1 : 0,
                    i2 = int.TryParse(sNum.Substring(len1, len2), out i2) ? i2 : 0;
                if (i1 + i2 == i)
                {
                    lst.Add(i.ToString());
                }
            }

            Console.WriteLine(lst.Count == 0 ? "INVALID RANGE" : string.Join(" ", lst));
            return lst.Count == 0 ? "INVALID RANGE" : string.Join(" ", lst);
        }

        static string encryption(string s)
        {
            string ss = (s ?? "").Replace(" ", "");
            double d = Math.Sqrt(ss.Length);
            int cols = (int)Math.Ceiling(d), rows = (int)Math.Ceiling((double)ss.Length/cols);
            StringBuilder[] grid = new StringBuilder[cols];
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols && i * cols + j < ss.Length; j++)
                {
                    if (grid[j] == null)
                        grid[j] = new StringBuilder();
                    grid[j].Append(ss.Substring(i * cols + j, 1));
                }
            }
            return string.Join<StringBuilder>(" ", grid);
        }

        static string biggerIsGreater(string w)
        {
            char[] arr = w.ToCharArray();
            int frMax = 0, toMax = -1;
            for (int i = arr.Length - 1; i > frMax; i--)
            {
                char fr = arr[i];
                for (int j = i - 1; j >= frMax; j--)
                {
                    if (arr[i] > arr[j] && (j > frMax || toMax == -1 || fr < arr[toMax]))
                    {
                        frMax = j;
                        toMax = i;
                    }
                }
            }
            return toMax == -1 ? "no answer" : ProcessArray(ref arr, frMax, toMax);
        }

        static string ProcessArray(ref char[] arr, int frIdx, int toIdx)
        {
            // exchange from and to
            char temp = arr[frIdx]; arr[frIdx] = arr[toIdx]; arr[toIdx] = temp;
            //sort the tail
            return new string(arr.Take(frIdx + 1).Concat(arr.Skip(frIdx + 1).OrderBy(ch => ch)).ToArray());
        }

        static long repeatedString(string s, char a, long n)
        {
            long len = s.Length;
            char[] arr = s.ToCharArray();
            return arr.Where(c => c == a).Count() * (n / len) + arr.Take((int)(n % len)).Where(c => c == a).Count();
        }

        public static int nonDivisibleSubset(int k, List<int> s)
        {
            if (k == 1) return 1;
            IEnumerable<IGrouping<int, int>> grp = s.Select(l => l % k).GroupBy(l => l).OrderBy(g => g.First());

            int max = 0;

            for (int loIdx = 0, hiIdx = grp.Count() - 1; loIdx <= hiIdx;)
            {
                int lo = grp.ElementAt(loIdx).First();
                int hi = grp.ElementAt(hiIdx).First();
                int loCnt = (lo * 2) % k == 0 ? 1 : grp.ElementAt(loIdx).Count();
                int hiCnt = (hi * 2) % k == 0 ? 1 : grp.ElementAt(hiIdx).Count();

                if (loIdx == hiIdx)
                {
                    max += loCnt;
                    loIdx++;
                }
                else
                {
                    bool isDiv = (lo + hi) % k == 0;

                    if (lo + hi < k)
                    {
                        max += loCnt;
                        loIdx++;
                    }
                    else
                    {
                        max += isDiv ? Math.Max(loCnt, hiCnt) : hiCnt;
                        hiIdx--;
                        if (isDiv) loIdx++;
                    }
                }
            }
            return max;
        }

        static int queensAttack(int n, int k, int r_q, int c_q, int[][] obstacles)
        {
            Dictionary<int, int> dirs = new Dictionary<int, int>();
            if (obstacles == null || obstacles.Length == 0 || obstacles[0] == null)
            {
                obstacles = new int[1][] { new int[2] { 2 * n, 2 * n } };
            }
            for (int dir = 0; dir < 8; dir++)
            {
                switch (dir)
                {
                    case 0:
                        dirs[dir] = obstacles.Where(o => o[1] == c_q && o[0] > r_q).Select(oo => oo[0] - r_q - 1).Aggregate(n - r_q, Math.Min);
                        break;
                    case 1:
                        dirs[dir] = obstacles.Where(o => o[0] - r_q == c_q - o[1] && o[0] > r_q).Select(oo => oo[0] - r_q - 1).Aggregate(Math.Min(n - r_q, c_q - 1), Math.Min);
                        break;
                    case 2:
                        dirs[dir] = obstacles.Where(o => o[0] == r_q && o[1] < c_q).Select(oo => c_q - oo[1] - 1).Aggregate(c_q - 1, Math.Min);
                        break;
                    case 3:
                        dirs[dir] = obstacles.Where(o => o[0] - r_q == o[1] - c_q && o[0] < r_q).Select(oo => r_q - oo[0] - 1).Aggregate(Math.Min(r_q - 1, c_q - 1), Math.Min);
                        break;
                    case 4:
                        dirs[dir] = obstacles.Where(o => o[1] == c_q && o[0] < r_q).Select(oo => r_q - oo[0] - 1).Aggregate(r_q - 1, Math.Min);
                        break;
                    case 5:
                        dirs[dir] = obstacles.Where(o => o[0] - r_q == c_q - o[1] && o[0] < r_q).Select(oo => r_q - oo[0] - 1).Aggregate(Math.Min(r_q - 1, n - c_q), Math.Min);
                        break;
                    case 6:
                        dirs[dir] = obstacles.Where(o => o[0] == r_q && o[1] > c_q).Select(oo => oo[1] - c_q - 1).Aggregate(n - c_q, Math.Min);
                        break;
                    case 7:
                        dirs[dir] = obstacles.Where(o => o[0] - r_q == o[1] - c_q && o[0] > r_q).Select(oo => oo[0] - r_q - 1).Aggregate(Math.Min(n - r_q, n - c_q), Math.Min);
                        break;
                }
            }
            return dirs.Select(p => p.Value).Sum();
        }

        static string organizingContainers(int[][] container)  // check if containers sortable
        {
            List<long> vols = new List<long>();
            List<long> clrs = new List<long>();
            for (int i = 0; i < container.Length; i++)
            {
                vols.Add(container[i].Select(el => (long)el).Sum());
                if (i == 0)
                    clrs = container[i].Select(e => (long)e).ToList();
                else
                    clrs = clrs.Select((el, idx) => el += container[i][idx]).ToList();
            }
            vols.Sort(); clrs.Sort();
            return vols.SequenceEqual(clrs) ? "Possible" : "Impossible";
        }

        static int[][] container = new int[][]
        {
            new int[]{997612619, 934920795, 998879231, 999926463},
            new int[]{ 960369681, 997828120, 999792735, 979622676 }
            ,
            new int[]{999013654, 998634077, 997988323, 958769423},
            new int[]{ 997409523, 999301350 ,940952923, 993020546 }
        };

        static string superReducedString(string s)
        {
            string reduced = Regex.Replace(s, "([a-z])\\1{1}", "");
            while (reduced.Length != s.Length)
            {
                s = reduced;
                reduced = Regex.Replace(s, "([a-z])\\1{1}", "");
            }
            return reduced.Length == 0 ? "Empty String" : reduced;
        }

        static int camelcase(string s)
        {
            return Regex.Matches(s, "[A-Z]").Count + 1;
        }

        static string funnyString(string s) // diffs array is equal same for the reversed string
        {
            return (s ?? "").Length < 3 || !s.ToCharArray().Where((c, i) => i <= s.Length / 2 && Math.Abs((int)c - s[i + 1]) != Math.Abs((int)s[s.Length - 1 - i] - s[s.Length - 2 - i])).Any() ? "Funny" : "Not Funny";
        }

        static int alternatingCharacters(string s) // get min # of char to delete to avoid adjacent chars
        {
            return s.Length - Regex.Matches(s, "([ABab])\\1*").Count;
        }

        static int gemstones(string[] arr) // count common chars for all strings in array
        {
            int len = arr.Length;
            return arr.Select(s => s.ToCharArray().Distinct()).SelectMany(a => a).GroupBy(c => c).Count(g => g.Count() == len);
        }

        static int theLoveLetterMystery(string s) // count atomic iterations to convert a word to anagram
        {
            int len = s.Length;
            return len < 2 ? 0 : s.ToCharArray().Select((c, i) => i < len / 2 ? Math.Abs(c - s[len - 1 - i]) : 0).Sum();
        }

        static int beautifulBinaryString(string b) // min steps to eiminate "010"
        {
            int count = 0;
            for (int i = b.IndexOf("010"); i > -1 && i < b.Length - 2; i = b.IndexOf("010", i + 3))
            {
                count++;
            }
            return count;
        }

        static void stackManipulation(string[] queries) // optimized printing MAX element
        {
            Stack<long> st = new Stack<long>();
            long max = long.MinValue;
            int idx = -1;
            foreach (string q in queries)
            {
                long[] cmd = q.Split(' ').Select(s => long.Parse(s)).ToArray();
                switch (cmd[0])
                {
                    case 1: // Push
                        st.Push(cmd[1]);
                        if (cmd[1] > max)
                        {
                            idx = 0;
                            max = cmd[1];
                        }
                        else
                            idx++;
                        break;
                    case 2: // Pop
                        st.Pop();
                        if (idx == 0)
                        {
                            max = long.MinValue;
                            for (int i = 0; i < st.Count(); i++)
                            {
                                long el = st.ElementAt(i);
                                if (el > max)
                                    max = el;
                            }
                        }
                        else
                            idx--;
                        break;
                    case 3: // print MAX element
                        Console.WriteLine(max);
                        break;
                }
            }
        }

        static string isBalanced(string s)
        {
            string opnBrkts = "[({";
            Dictionary<char, char> pairs = new Dictionary<char, char> { { ']', '[' }, { ')', '(' }, { '}', '{' } };
            Stack<char> st = new Stack<char>();
            foreach (char c in s)
            {
                if (opnBrkts.Contains(c))
                    st.Push(c);
                else if (st.Count() == 0 || pairs[c] != st.Pop())
                    return "NO";
            }
            return st.Count() > 0 ? "NO" : "YES";
        }

        public static int equalStacks(List<int> h1, List<int> h2, List<int> h3) // remove min blocks from 3 columns to equalize
        {
            int hh1 = h1.Sum(), hh2 = h2.Sum(), hh3 = h3.Sum();
            int i1 = 0, i2 = 0, i3 = 0, len1 = h1.Count(), len2 = h2.Count(), len3 = h3.Count();
            while (i1 < len1 || i2 < len2 || i3 < len3)
            {
                while (hh2 > hh1 && i2 < len2) hh2 -= h2.ElementAt(i2++);
                while (hh3 > hh1 && i3 < len3) hh3 -= h3.ElementAt(i3++);
                int min = Math.Min(hh2, hh3);
                if (hh1 == min) return min;
                while (hh1 > min && i1 < len1) hh1 -= h1.ElementAt(i1++);
            }
            return 0;
        }

        public class EditItem
        {
            public int startIdx;
            public int delta;
            public string addStr;
        }

        public static void editAttribute()
        {
            //int nCmds = int.Parse(Console.ReadLine());
            //int rmv = 0;
            //Stack<int> lens = new Stack<int>(); lens.Push(0);// lenghts history
            //Stack<string> adds = new Stack<string>(); // rmv history to append later upon undo
            //StringBuilder sb = new StringBuilder(); // current string
            int curLen = 0;
            Stack<EditItem> st = new Stack<EditItem>();
            //for (int i = 0; i < nCmds; i++)
            foreach (string s in new string[] {
"1 zsfncpxdzl",
"3 4",
"3 6",
"2 1",
"3 7",
"3 2",
"4",
"2 4",
"2 6",
"4",
"4",
"1 l",
"1 hpe",
"3 6",
"2 7",
"4",
"3 6",
"4",
"3 6",
"1 zipsqagri",
"1 vuqxstnj",
"4",
"3 13",
"4",
"3 10",
"3 6",
"1 uzdpy",
"1 bupqp",
"1 kn",
"2 6",
"3 8",
"1 iiuvfbn",
"4",
"2 1",
"2 12",
"4",
"3 7",
"4",
"2 9",
"3 1",
"1 axbhx",
"1 wovbfyvt",
"3 11",
"3 7",
"3 2",
"4",
"1 tjmqp",
"4",
"2 6",
"3 4"
            }) // 
            // foreach (string s in new string[] { "1 lchbfcjtfpsmjrqsdgci", "3 19", "1 cpcvixlm", "1 apdjgjydvpbpvyiy", "2 29", "4","4","3 9", "4","4"}) // c f
            {
                switch (s[0])
                {
                    case '1':
                        st.Push(new EditItem { startIdx = curLen, delta = s.Length - 2, addStr = s.Substring(2) });
                        curLen += st.Peek().delta;
                        break;
                    case '2':
                        st.Push(new EditItem { startIdx = curLen, delta = int.Parse(s.Substring(2)) });
                        curLen -= st.Peek().delta;
                        break;
                    case '3':
                        int idx = int.Parse(s.Substring(2)) - 1;
                        EditItem item = st.First(it => it.addStr != null && it.startIdx <= idx && it.startIdx + it.delta >= idx);
                        Console.WriteLine(item.addStr[idx - item.startIdx]);
                        break;
                    case '4':
                        if(st.Count > 0)
                        {
                            EditItem it = st.Pop();
                            curLen += it.addStr == null ? it.delta : -it.delta;
                        }
                        break;
                }
                //string[] pair = s.Split(' ');
                //string[] pair = Console.ReadLine().Split(' ');

                //    switch (pair[0])
                //    {
                //        case "1":
                //            if (rmv > 0)
                //            {
                //                sb.Remove(sb.Length - rmv, rmv);
                //                rmv = 0;
                //            }
                //            sb.Append(pair[1]);
                //            lens.Push(curLen = sb.Length);
                //            break;
                //        case "2":
                //            int rmvDelta = int.Parse(pair[1]);
                //            curLen -= rmvDelta;
                //            rmv += rmvDelta;
                //            lens.Push(curLen); // after removal
                //            adds.Push(sb.ToString(curLen, rmvDelta));
                //            break;
                //        case "3":
                //            Console.WriteLine(sb[int.Parse(pair[1]) - 1]);
                //            break;
                //        case "4":
                //            int newLen = lens.Pop();
                //            while (curLen == newLen) newLen = lens.Pop();
                //            if(newLen > curLen)
                //            {
                //                if (rmv > 0)
                //                {
                //                    sb.Remove(sb.Length - rmv, rmv);
                //                    rmv = 0;
                //                }
                //                sb.Append(adds.Pop());
                //            }
                //            else
                //            {
                //                rmv += curLen - newLen;
                //            }
                //            lens.Push(curLen = newLen);
                //            break;
                //    }
            }
        }

        static void simpleTextEditor() // to improve speed - use TextWriter for output
        {
            int nCmds = int.Parse(Console.ReadLine()), curLen = 0;
            Stack<EditItem> st = new Stack<EditItem>();
            for (int i = 0; i < nCmds; i++)
            {
                string s = Console.ReadLine();

                switch (s[0])
                {
                    case '1':
                        st.Push(new EditItem { startIdx = curLen, delta = s.Length - 2, addStr = s.Substring(2) });
                        curLen += st.Peek().delta;
                        break;
                    case '2':
                        st.Push(new EditItem { startIdx = curLen, delta = int.Parse(s.Substring(2)) });
                        curLen -= st.Peek().delta;
                        break;
                    case '3':
                        int idx = int.Parse(s.Substring(2)) - 1;
                        EditItem item = st.First(it => it.addStr != null && it.startIdx <= idx && it.startIdx + it.delta >= idx);
                        Console.WriteLine(item.addStr[idx - item.startIdx]);
                        break;
                    case '4':
                        if (st.Count > 0)
                        {
                            EditItem it = st.Pop();
                            curLen += it.addStr == null ? it.delta : -it.delta;
                        }
                        break;
                }
            }
        }

        private static int getNextPrime(int prime)
        {
            bool status = false;
            while(!status)
            {
                prime++;
                status = true;
                int topDiv = (int)Math.Floor(Math.Sqrt(prime));
                for (int i = 2; status && i <= topDiv; i++)
                {
                    if (prime % i == 0)
                        status = false;
                }
            }
            return prime;
        }

        static int[] waiter(int[] number, int q) // using q conceq primes move plates from init pile to B1, B2, ...Bq piles and Aq leftover pile
        {
            int prime = 2; // the very 1st prime
            List<int> src = number.ToList();
            List<List<int>> dest = new List<List<int>>();
            List<int> trgA = new List<int>();
            while( q-- > 0 && src.Count > 0)
            {
                src.Reverse();
                List<int> trgB = new List<int>();
                foreach(int i in src)
                {
                    (i % prime == 0 ? trgB : trgA).Add(i);
                }
                if (trgB.Count > 0)
                {
                    trgB.Reverse();
                    dest.Add(trgB);
                }
                src.Clear();
                src.AddRange(trgA);
                trgA.Clear();
                prime = getNextPrime(prime);
            }
            if (src.Count > 0)
            {
                src.Reverse();
                dest.Add(src);
            }
            return dest.SelectMany(el=>el).ToArray();
        }


        static int truckTour(int[][] petrolpumps)
        {
            int n = petrolpumps.Length;
            for (int i = 0; i < n; i++)
            {
                if (petrolpumps[i][0] - petrolpumps[i][1] < 0) continue;
                long fuel = 0;
                for (int j = i; j - i < n && fuel >= 0; j++)
                {
                    int k = j % n;
                    fuel += (long)petrolpumps[k][0] - (long)petrolpumps[k][1];
                }
                if (fuel >= 0) return i;
            }
            return -1;
        }

        static int GetMinOfMax(int[] a, int d)
        {
            IEnumerable<int> segm1 = a.Take(d);
            int min = segm1.Max();
            int idx = segm1.Select((e,i)=>new int[] { e,i} ).Last(el=>el[0] == min)[1], idxNext = 0;
            for (int i = idx+1; i <= a.Length - d; i++)
            {
                int max = a[i];
                idx = idxNext = i;
                // find max for the next d-interval
                for (int j = i + 1; j < i + d; j++)
                {
                    if (a[j] >= max)
                    {
                        idx = j;
                        max = a[j];
                    }
                    if (a[j] >= min)
                    {
                        idxNext = j;
                    }
                }
                if (min > max)
                {
                    min = max;
                    i = idx;
                }
                else
                {
                    i = idxNext;
                }
            }
            return min;
        }

        static int[] solveMinOfMax(int[] arr, int[] queries)
        {
            // return queries.Select(d=>arr.Select((e,i) => {return i <= arr.Length - d ? arr.Skip(i).Take(d).Max() : int.MaxValue;}).Min()).ToArray();
            return queries.Select(q => GetMinOfMax(arr, q)).ToArray();
        }

        static int ProcessHeapOp(string s, ref int min, ref List<int> src, ref List<int> res)
        {
            switch(s[0])
            {
                case '1':
                    int n = int.Parse(s.Split(' ')[1]);
                    src.Add(n);
                    if (n < min) min = n;
                    break;
                case '2':
                    int d = int.Parse(s.Split(' ')[1]);
                    src.Remove(d);
                    if (d <= min) min = src.Count < 1 ? int.MaxValue : src.Min();
                    break;
                case '3':
                    res.Add(min);
                    break;
            }
            return min;
        }

        static int[] HeapManipulation(string[] queries)
        {
            int min = int.MaxValue;
            List<int> src = new List<int>();
            List<int> res = new List<int>();
            Array.ForEach(queries, q => ProcessHeapOp(q, ref min, ref src, ref res));
            return res.ToArray();
        }

        private static int Sweeten(ref bool finished, int k, ref int prev, int cur, int next, ref int count, ref Queue<int> ins)
        {
            if (finished || prev >= k)
            {
                finished = true;
                return next;
            }
            count++;
            // fill out prev [newCur, lastQ] and cur [next, lastQ]
            int newCur = prev + 2 * cur;
            int lastQ = ins.Count > 0 ? ins.Peek() : int.MaxValue;
            cur = lastQ <= next ? ins.Dequeue() : next;
            if (newCur > next) ins.Enqueue(newCur);
            return cur;
        }

        static int cookies(int k, int[] A) // 
        {
            bool psbl = A.Any(n=>n>k);
            int count = 0;
            int[] a = A.Where(j=>j<k).OrderBy(n => n).ToArray();
            if (a.Count() == 0) return 0;
            if (a.Count() == 1) return a.First() >= k ? 0 : psbl ? count + 1 : -1;
            Queue<int> q = new Queue<int>();

            for(int i = 1; i < a.Length + 1; i++)
            {
                if (a[i - 1] < k)
                {
                    count++;
                    bool isQMin = i > a.Length -1 ||q.Count > 0 && q.Peek() <= a[i];
                    int sum = a[i - 1] + (isQMin ? q.Dequeue() : a[i]) * 2; // new created element instead of 2 old
                    if (!psbl && sum >= k) psbl = true;
                    if(i >= a.Length || sum >= a[i-(isQMin ? 1 : 0)])
                    {
                        q.Enqueue(sum);
                        if (i < a.Length && !isQMin) i++;
                    }
                    else if(i < a.Length)
                    {
                        if(sum >= k) return count;
                        int delta = isQMin ? 1 : 0;
                        a[i - delta] = sum;
                        i -= delta;
                    }
                    if(i >= a.Length)
                    {
                        if (q.Count < 1) return psbl ? count : -1;
                        while(q.Count > 1)
                        {
                            int minEl = q.Dequeue();
                            if(minEl >= k) return count;
                            q.Enqueue(minEl + 2 * q.Dequeue());
                            count++;
                        }
                        return q.Peek() >= k ? count : psbl ? count + 1 : -1;
                    }
                }
                else
                    return count;
            }
            return -1;
        }

        static int[] runningMedian(int[] a)
        {
            List<int> res = new List<int>();
            //******************    OPTIMIZED WITH HEAPS    *****************
            MinHeap<int> maxs = new MinHeap<int>();
            MaxHeap<int> mins = new MaxHeap<int>();
            for (int i = 0; i < a.Length; i++)
            {
                int k = a[i];
                if (k > mins.GetMax())
                {
                    maxs.Add(k);
                    if(maxs.GetSize() - mins.GetSize() > 1)
                    {
                        mins.Add(maxs.PopMin());
                    }
                }
                else
                {
                    mins.Add(k);
                    if(mins.GetSize() - maxs.GetSize() > 1)
                    {
                        maxs.Add(mins.PopMax());
                    }
                }
                int minSz = mins.GetSize(), maxSz = maxs.GetSize();
                int loMid = mins.GetMax(), hiMid = maxs.GetMin();
                bool even = (loMid + hiMid) % 2 == 0;
                res.Add(minSz > maxSz ? loMid : minSz < maxSz ? hiMid : (loMid + hiMid) /(even ? 2 : -2));
            }

            //******************  NOT OPTIMIZED BUT SHORT *******************
            //List<int> sorted = new List<int>();
            //int midIdx = 0;
            //for (int i = 0; i < a.Length; i++)
            //{
            //    int k = a[i], mid = midIdx < 0 ? k : a[midIdx], len = i;
            //    int insIdx = sorted.Where(n => n < k).Count();
            //    if(insIdx < len) sorted.Insert(insIdx, k);
            //    else sorted.Add(k);
            //    if (len % 2 != 0) midIdx++;

            //    mid = sorted[midIdx];
            //    int mid_1 = midIdx < 1 ? mid : sorted[midIdx - 1];
            //    bool even = midIdx > 0 && (mid + mid_1) % 2 == 0;
            //    mid = (len + 1) % 2 != 0 ? mid : (mid + mid_1) / (even ? 2 : -2);
            //    res.Add(mid);
            //}
            return res.ToArray();
        }

        static int[] contacts(string[][] queries)
        {
            List<int> res = new List<int>();
            SNode root = new SNode();
            foreach(string[] cmd in queries)
            {
                switch(cmd[0])
                {
                    case "add":
                        root.Add(cmd[1]);
                        break;
                    case "find":
                        res.Add(root.FindNumOf(cmd[1]));
                        break;
                }
            }
            return res.ToArray();
        }

        /// <summary>
        /// //////////////////////////////////////////////
        /// </summary>
        /// <param name="args"></param>
        static void Main(string[] args)
        {
            int[] cntNames = contacts(new string[][] { new string[] { "add", "s" }, new string[] { "add", "ss" }, new string[] { "add", "sss" }, new string[] { "add", "ssss" }, new string[] { "add", "sssss" },
                                        new string[] { "find", "s" }, new string[] { "find", "ss" }, new string[] { "find", "sss" }, new string[] { "find", "ssss" }, new string[] { "find", "sssss" }, new string[] { "find", "ssssss" } });
            //int[] cntNames = contacts(new string[][] { new string[] { "add", "hack" }, new string[] { "add", "hackerrank" }, new string[] { "find", "hac" }, new string[] { "find", "hak" } });
            int[] med = runningMedian(new int[] { 12, 4, 5, 3, 8, 7 });
            int cntSweets = cookies(105823341, Enumerable.Repeat(1, 100000).ToArray()); // 99999
            //int cntSweets = cookies(1000, new int[] { 52, 96, 13, 37 }); // -1
            //int cntSweets = cookies(7, new int[] { 1, 2, 3, 9, 10, 12 }); // 2
            //int cntSweets = cookies(47245, new int[] { 3554, 2227, 8866, 9890, 212, 8669, 2423, 7651, 3878, 3379, 1419, 6134, 5767, 859, 
            //                                           2848, 9309, 1449, 8408, 8041, 3367, 6676, 6382, 4136, 4871 }); // 20
            int[] heapRes = HeapManipulation(new string[] {"1 3","1 65","2 65","3","2 3","1 7","3","1 -1","3","2 -1","3","2 7"}); // 3 7 -1 7
            //int[] heapRes = HeapManipulation(new string[] {"1 4","1 9","3","2 4","3"}); // 4 9
            int[] minMax = solveMinOfMax(MinMaxData.arr, MinMaxData.segm);
            //int[] minMax = solveMinOfMax(new int[] { 33, 11, 44, 11, 55 }, new int[] { 1, 2, 3, 4, 5 }); // 11 33 44 44 55
            int pumpIdx = truckTour(PumpData.Data); // 573
            //int pumpIdx = truckTour(new int[][] { new int[] { 1, 5 }, new int[] { 10, 3 }, new int[] { 3, 4 } }); // 1
            int[] waiterPlates = waiter(new int[] { 3,4,7,6,5}, 1); // 4 6 3 7 5
            //int[] waiterPlates = waiter(new int[] { 3,3,4,4,9}, 2); // 4 4 9 3 3
            editAttribute();
            string bal = isBalanced("{{}("); // "YES"
            //string bal = isBalanced("{[(])}"); // "NO"
            stackManipulation(new string[] { "1 97","2","1 20","2","1 26","1 20","2","3","1 91","3" }); // 26 91
            //int stepsToKill010 = beautifulBinaryString("0101010"); // 2
            //int angrmCount = theLoveLetterMystery("abcd"); // 4
            //int cntGems = gemstones(new string[] { "abcdde","baccd","eeabg" }); // 2
            //string funny = funnyString("abcdefghijklmnopqrstuvwxyz"); // "Funny"
            //int countWords = camelcase("saveChangesInTheEditor"); // 5
            //string reduced = superReducedString("aaabccddd"); // "abd"
            //int nonDivCount = nonDivisibleSubset(1, new List<int> { 1 });
            //int nonDivCount = nonDivisibleSubset(4, new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 });
            //int nonDivCount = nonDivisibleSubset(3, new List<int> { 1, 7, 2, 4 });
            //int nonDivCount = nonDivisibleSubset(2, new List<int> { 422346306, 940894801, 696810740, 862741861, 85835055, 313720373 });
            //string strPossible = organizingContainers(container);
            //int queenCells = queensAttack(88587, 9, 20001, 20003, new int[][] { new int[] { 20001, 20002 },
            //        new int[] { 20001, 20004 }, new int[] { 20000, 20003 }, new int[] { 20002, 20003 },
            //        new int[] { 20000, 20004 }, new int[] { 20000, 20002 }, new int[] { 20002, 20004 },
            //        new int[] { 20002, 20002 } });
            //int queenCells = queensAttack(5, 3, 4, 3, new int[][] { new int[] { 5, 5 }, new int[] { 4, 2 }, new int[] { 2, 3 } });
            //int queenCells = queensAttack(100000, 0, 4187, 5068, null);
            //string s = biggerIsGreater("ehdegnmorgafrjxvksc");
            //string encr = encryption("chillout");
            //string kkn = kaprekarNumbers(1, 99999);
            //string tm = timeToWords(5, 47);
            //int mx = flatlandSpaceStations(20, new int[] { 13, 1, 11, 10, 6 });
            //int n = workbook(5, 3, new int[] { 4, 2, 6, 1, 10 });
            //string progrDay = dayOfProgrammer(1916);

            //int fee = GetFeeOnReturn(new DateTime(2015, 6, 6), new DateTime(2015, 6, 9));

            //int sum = divisorSum(6);
            //var c = new Car().GetHorsepower();
            //Console.WriteLine(((int)Math.Round(12.00 * (1 + (8 + 20) / 100.00))).ToString());
            //string ex = 8.345.ToString("F1");
            //string ee = ex;
            //int t = Convert.ToInt32(Console.ReadLine());

            //for (int tItr = 0; tItr < t; tItr++)
            //{
            //    int n = Convert.ToInt32(Console.ReadLine());

            //    int[] q = Array.ConvertAll(Console.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp))
            //    ;
            //    minimumBribes(q);
            //}
            //1 5 3
            //4 8 7
            //6 9 1
            /**/
            int[][] queries = new int[][] { 
            new int[] {29, 40, 787},
            new int[] {9, 26, 219},
            new int[] {21, 31, 214},
            new int[] {8, 22, 719},
            new int[] {15, 23, 102},
            new int[] {11, 24, 83},
            new int[] {14, 22, 321},
            new int[] {5, 22, 300},
            new int[] {11, 30, 832},
            new int[] {5, 25, 29},
            new int[] {16, 24, 577},
            new int[] {3, 10, 905},
            new int[] {15, 22, 335},
            new int[] {29, 35, 254},
            new int[] {9, 20, 20},
            new int[] {33, 34, 351},
            new int[] {30, 38, 564},
            new int[] {11, 31, 969},
            new int[] {3, 32, 11},
            new int[] {29, 35, 267},
            new int[] {4, 24, 531},
            new int[] {1, 38, 892},
            new int[] {12, 18, 825},
            new int[] {25, 32, 99},
            new int[] {3, 39, 107},
            new int[] {12, 37, 131},
            new int[] {3, 26, 640},
            new int[] {8, 39, 483},
            new int[] {8, 11, 194},
            new int[] {12, 37, 502}
            };
            int len = 40;
            long res = arrayManipulation(len, queries); // 8628
            Console.WriteLine(res);
            Console.ReadKey();
        }

        // ********************************************************************************************************
        // **********************************  FOR READ WRITE OPERATIONS*******************************************
        // ********************************************************************************************************
        static void TextWriterCode(string[] args)
        {
            TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

            string[] nq = Console.ReadLine().Split(' ');

            int n = Convert.ToInt32(nq[0]);

            int q = Convert.ToInt32(nq[1]);

            int[] arr = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp))
            ;

            int[] queries = new int[q];

            for (int queriesItr = 0; queriesItr < q; queriesItr++)
            {
                int queriesItem = Convert.ToInt32(Console.ReadLine());
                queries[queriesItr] = queriesItem;
            }

            int[] result = new int[] { }; // solve(arr, queries);

            textWriter.WriteLine(string.Join("\n", result));

            textWriter.Flush();
            textWriter.Close();
        }
        // ********************************************************************************************************
        // ********************************************************************************************************
        // ********************************************************************************************************

        // Complete the hourglassSum function below.
        //static int hourglassSum(int[][] arr)
        //{
        //    int[] a = new int[10];
        //    int sum = Int32.MinValue;
        //    for (int i = 0; i < arr.Length - 2; i++)
        //    {
        //        for (int j = 0; j < arr[0].Length - 2; j++)
        //        {
        //            Console.Write(arr[i][j].ToString() + " ");
        //            int tmpSum = arr[i][j] + arr[i][j + 1] + arr[i][j + 2]
        //                + arr[i + 1][j + 1]
        //                + arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];
        //            if (tmpSum > sum)
        //            {
        //                sum = tmpSum;
        //            }
        //        }
        //        Console.Write("\r\n");
        //    }
        //    return sum;
        //}

        //static void Main(string[] args)
        //{
        //    //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        //    int[][] arr = new int[6][];

        //    for (int i = 0; i < 6; i++)
        //    {
        //        arr[i] = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
        //    }

        //    int result = hourglassSum(arr);
        //    Console.WriteLine("The HG sum is; " + result.ToString());
        //    Console.WriteLine("Press any key to exit...");

        //    Console.ReadKey();

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
        public static string ReverseIntArray(int[] arr)
        {
            return string.Join(" ", arr.Select((val, idx) => new { Value = val, Index = idx }).OrderByDescending(el => el.Index).Select(item => item.Value));
        }
        public static int IndexOf(List<int> search, List<int> find)
        {
            foreach (int item in find)
            {
                for(int i = 0; i < search.Count; i++)
                {
                    for(int j = 0; j < find.Count && find.ElementAt(j) == search.ElementAt(j); j++)
                    {
                        if (j == find.Count - 1)
                            return i;
                    }

                }
            }
            return -1;
        }
        public static string ReverseStringIterative(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            char[] arr = input.ToCharArray();
            int iStart = 0, iEnd = input.Length - 1;
            while (iStart < iEnd)
            {
                // swap arr items
                char tmp = arr[iStart];
                arr[iStart] = arr[iEnd];
                arr[iEnd] = tmp;
                iStart++;
                iEnd--;
            }
            return new string(arr);
        }
        static long arrayManipulation(int n, int[][] queries)
        {
            long max = 0;
            queries.Select(q => new int[2][] { new int[] { q[0], q[2] }, new int[] { q[1]+1, -q[2] } }).SelectMany(e => e).GroupBy(e=>e[0]).OrderBy(g=>g.ElementAt(0)[0]).Aggregate(0L, func: (sum, item) => { long res = sum + item.Sum(e=>e[1]); if (res > max) max = res; return res; });
            return max;
        }
    }

    class Person
    {
        protected string firstName;
        protected string lastName;
        protected int id;

        public Person() { }
        public Person(string firstName, string lastName, int identification)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = identification;
        }
        public void printPerson()
        {
            Console.WriteLine("Name: " + lastName + ", " + firstName + "\nID: " + id);
        }
    }

    class Student : Person
    {
        readonly char[] symbols = "OEAPDT".ToCharArray();
        private int[] testScores;
        readonly int[,] rules = new int[,] {
            { 90, 101},
            { 80, 90},
            { 70, 80},
            { 55, 70},
            { 40, 55},
            { int.MinValue, 40}
        };
        public char Calculate()
        {
            double avg = testScores.Average();
            return symbols.Where((s,idx) => rules[idx,0] <= avg && avg < rules[idx, 1]).FirstOrDefault();
        }
    }
    class Difference
    {
        private int[] elements;
        public int maximumDifference;

        public int computeDifference(int[] a)
        {
            return a.Max() - a.Min();
        }

    }
}

