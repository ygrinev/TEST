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
        static long minimumLoss(long[] price)
        {
            long prev = long.MaxValue;
            int pIdx = int.MaxValue;
            return price.Select((p, i) => new { key = p, idx = i }).OrderByDescending(el => el.key).Aggregate(long.MaxValue, (min, pp) => {
                if (pp.idx > pIdx && prev - pp.key > 0 && prev - pp.key < min)
                    min = prev - pp.key;
                prev = pp.key; pIdx = pp.idx;
                return min;
            });
        }
        static long maximumSum(long[] a, long m)
        {
            int idx = 0;
            long maxMdl = 0;
            long curSum = 0;
            Dictionary<long, List<int>> dct = a.Aggregate(new Dictionary<long, List<int>>(), (map, cur) => {
                long curMdl = cur % m;
                if (curMdl > maxMdl) maxMdl = curMdl; // el level
                curSum = (curSum + curMdl) % m;
                if (curSum > maxMdl) maxMdl = curSum; // part.sum level
                if (map.ContainsKey(curSum)) { map[curSum].Add(idx); }
                else { map[curSum] = new List<int> { idx }; }
                idx++;
                return map;
            });
            if (maxMdl == m - 1) return maxMdl;
            dct.Keys.OrderBy(k => k).Aggregate((cur, next) => {
                if (maxMdl < m - 1 && dct[cur].First() > dct[next].Last() && (cur - next + m) % m > maxMdl) { maxMdl = (cur - next + m) % m; }
                return next;
            });
            return maxMdl;
        }
        static int[][] knightlOnAChessboard(int n)
        {
            bool[,] done = new bool[n, n];
            int[] fin = new int[] { n-1,n-1};
            int[][] res = new int[n-1][].Select(a=>Enumerable.Repeat(-1, n-1).ToArray()).ToArray();
            for(int i = 1; i < n; i++)
            {
                for(int j = i; j < n; j++)
                {
                    for (int bi = 0; bi < n; bi++) for (int bj = bi; bj < n; bj++) done[bi, bj] = done[bj, bi] = false;
                    int[] offs = new int[] { i, j };
                    List<int[]> nextMoves = GetNextMoves(done, n, new int[] { 0, 0 }, offs);
                    int cnt = 0;
                    while (nextMoves.Count > 0)
                    {
                        cnt++;
                        if (nextMoves.Any(el => el[0] == fin[0] && el[1] == fin[1]))
                            break;
                        nextMoves = nextMoves.Aggregate(new List<int[]>(), (lst, cur) => {
                            lst.AddRange(GetNextMoves(done, n, cur, offs));
                            return lst;
                        });
                    }
                    res[i - 1][j - 1] = res[j - 1][i - 1] = nextMoves.Count > 0 ? cnt : -1;
                }
            }

            return res;
        }

        private static List<int[]> GetNextMoves(bool[,] done, int n, int[] start, int[] offs)
        {
            done[start[0], start[1]] = true;
            var nextMoves = new List<int[]>{new int[] {start[0] + offs[0], start[1] + offs[1]},
                                            new int[] {start[0] + offs[0], start[1] - offs[1]},
                                            new int[] {start[0] - offs[0], start[1] + offs[1]},
                                            new int[] {start[0] - offs[0], start[1] - offs[1]},
            };
            if (offs[0] != offs[1])
                nextMoves.AddRange(new List<int[]>{
                                    new int[] {start[0] + offs[1], start[1] + offs[0]},
                                    new int[] {start[0] + offs[1], start[1] - offs[0]},
                                    new int[] {start[0] - offs[1], start[1] + offs[0]},
                                    new int[] {start[0] - offs[1], start[1] - offs[0]}
                });
            return nextMoves.Where(el=> el[0] >= 0 && el[1] >= 0 && el[0] < n && el[1] < n && !done[el[0], el[1]]).ToList();
        }
        static int[][] knightlOnAChessboard_fast(int n)
        {
            int[][] ret = new int[n - 1][].Select(a => new int[n - 1]).ToArray();

            for (int r = 1; r < n; r++)
            {
                for (int c = 1; c < n; c++)
                {
                    if (ret[c - 1][r - 1] != 0)
                    {
                        ret[r - 1][c - 1] = ret[c - 1][r - 1];
                        continue;
                    }

                    if (ret[r - 1][c - 1] == 0)
                    {
                        ret[r - 1][c - 1] = move(n, r, c);
                    }
                }
            }

            return ret;
        }

        private static int move(int n, int a, int b)
        {
            int[] rd = new int[] { b, a, b, a, -a, -b, -a, -b };
            int[] cd = new int[] { a, b, -a, -b, b, a, -b, -a };

            bool[,] visit = new bool[n, n];
            int er = n - 1;
            int ec = n - 1;

            Queue<int[]> q = new Queue<int[]>();
            q.Enqueue(new int[] { 0, 0, 0 });
            visit[0, 0] = true;

            while (q.Count > 0)
            {
                int[] cur = q.Dequeue();

                if (cur[0] == er && cur[1] == ec)
                {
                    return cur[2];
                }

                for (int i = 0; i < 8; i++)
                {
                    int nr = rd[i] + cur[0];
                    int nc = cd[i] + cur[1];

                    if (nr >= 0 && nr < n && nc >= 0 && nc < n && !visit[nr, nc])
                    {
                        q.Enqueue(new int[] { nr, nc, cur[2] + 1 });
                        visit[nr, nc] = true;
                    }
                }
            }

            return -1;
        }
        static long minimumAbsoluteDifference(int[] arr)
        {
            long min = long.MaxValue;
            arr.OrderBy(a => a).Aggregate((cur, next) => {
                min = Math.Min(min, Math.Abs(next - cur));
                return next;
            });
            return min;
        }
        static string gridChallenge(string[] grid)
        {
            string cur = string.Join("", grid[0].OrderBy(c => c));
            for (int i = 1; i < grid.Length; i++)
            {
                string next = string.Join("", grid[i].OrderBy(c => c));
                if (next.Select((nc, j) => nc < cur[j]).Any(b => b))
                    return "NO";
                cur = next;
            }
            return "YES";
        }
        static int powerSum(int X, int N)
        {
            if (X == 1) return 1;
            int max = (int)Math.Floor(Math.Pow(X, 1 / (double)N));
            int[] nums = new int[max].Select((n,i) => (int)Math.Pow(max - i, N)).ToArray();
            return numOfSums(nums, 0, -1, X);
        }
        static int numOfSums(int[] nums, int partSum, int lastIdx, int X) // count num of ways when SUM of subset of nums = X
        {
            if(lastIdx > nums.Length - 1 || partSum > X) return 0;
            if(partSum == X) return 1;
            int count = 0;
            while(++lastIdx < nums.Length)
                count += numOfSums(nums, partSum + nums[lastIdx], lastIdx, X);
            return count;
        }
        static int superDigit(string n, int k) // find super digit of n as int repeated k times
        {
            BigInteger b = n.Aggregate((BigInteger)0, (sum, cur) => sum + (cur-'0')) * k;
            while (b > 9)
            {
                BigInteger a = b % 10;
                while ((b /= 10) > 0) a += b % 10;
                b = a;
            }
            return (int)b;
        }
        static int towerBreakers(int n, int m) // remove bricks from n equal towers at each step by 2 players until all == 1
        {
            return m > 1 && n % 2 == 1 ? 1 : 2;
        }
        public static string findWinner(int x, int y) // chess knight
        {
            return x % 4 % 3 == 0 || y % 4 % 3 == 0 ? "First" : "Second";
        }
        static string nimGame(int[] pile) // last player removing stones WINS
        {
            return pile.Aggregate(0, (xor, cur) => xor ^ cur) == 0 ? "Second" : "First";
        }
        public static string misereNim(List<int> s) // last player removing stones LOOSES
        {
            return s.Count == 1 && s.Sum() == 1 || s.Count == s.Sum() && s.Count % 2 == 1
            || s.Count < s.Sum() && s.Aggregate(0, (cnt, cur) => cnt ^ cur) == 0 ? "Second" : "First";

            //return s.Count == 1 ? (s.Sum() > 1 ? "First" : "Second")
            //: s.Count == s.Sum() ? (s.Count % 2 == 0 ? "First" : "Second")
            //: s.Aggregate(0, (cnt, cur) => cnt ^ cur) > 0 ? "First" : "Second";

            //long sum = s.Sum();
            //int xor = s.Aggregate(0, (cnt, cur) => cnt ^ cur);
            //return sum == 1 || s.Count == sum && xor > 0 || s.Count < sum && xor == 0 ? "Second" : "First";

        }
        public static string nimbleGame(List<int> s) // move 1 by 1 all stones to 0-pos, last moved - wins
        {
            int cnt = 0;
            return s.Skip(1).Aggregate(0, (xor, cur) => { cnt++; return xor ^ (cur % 2 == 1 ? cnt : 0); }) > 0 ? "First" : "Second";
        }
        public static List<int> initPrimes(int max) // 975852
        {
            List<int> primes = new List<int> { 2, 3 };
            for (int i = 5; i <= (int)Math.Sqrt(max); i += 2)
            {
                if (!primes.Any(p => i % p == 0))
                    primes.Add(i);
            }
            return primes;
        }
        public static int getPrimCnt(int num, ref List<int> primes)
        {
            if (num == 1) return 0;
            else if (primes.Any(p => p == num)) return 1;
            int maxSqrt = (int)Math.Sqrt(num), cnt = 0;
            foreach (int prim in primes)
            {
                if (num < 2 || prim > maxSqrt)
                    break;
                while (num % prim == 0)
                {
                    num /= prim;
                    cnt++;
                }
            }
            cnt = num > 1 ? cnt + 1 : cnt;
            Console.Write($"{cnt},");
            return cnt;
        }
        public static int countPrims(int num)
        {
            int numPrimeFactors = 0;
            if (num == 1)
            {
                numPrimeFactors = 0;
            }
            else if (((num != 0) && (num & (num - 1)) == 0))
            {
                // If num is a power of 2
                numPrimeFactors = num / 2;
            }
            else
            {
                // Calculate # of prime factors
                while (num > 1)
                {
                    bool foundFactor = false;
                    for (int j = 2; j * j <= num; j++)
                    {
                        if (num % j == 0)
                        {
                            numPrimeFactors++;
                            num /= j;
                            foundFactor = true;
                            break;
                        }
                    }
                    if (!foundFactor)
                    {
                        // num is prime
                        numPrimeFactors++;
                        break;
                    }
                }
            }
            return numPrimeFactors;
        }
        public static int towerBreakers(List<int> arr) // modified version 
        {
            int max = arr.Max();
            if (max < 2) return 2;
            else if (arr.Count < 2) return 1;
            List<int> primes = initPrimes(max);
            int ret = arr.Where(a => a > 1).Aggregate(0, (xor, cur) => xor ^ getPrimCnt(cur, ref primes)) == 0 ? 2 : 1;
            Console.WriteLine();
            return ret;
        }
        public static string sillyGame(int num)
        {
            if (num < 3) return num % 2 == 0 ? "Alice" : "Bob";
            bool[] primes = new bool[num + 1];
            primes[0] = primes[1] = true;
            for (int idx = 4; idx < num + 1; idx += 2) primes[idx] = true; // exclude
            int cnt = 1; // {2}
            for (int i = 3; i <= num; i += 2)
            {
                if (!primes[i])
                {
                    cnt++;
                    for (int idx = 2 * i; idx < num + 1; idx += i) primes[idx] = true;
                }
            }
            return primes.Where(p => !p).Count() % 2 == 1 ? "Alice" : "Bob";
        }
        public static int towerBreakers2(List<int> arr) // modified 2 version - towers are broken into multi..
        {
            arr = arr.Select(a => { while (a % 4 == 0) a /= 2; return a; }).ToList();
            int max = arr.Max();
            if (max < 2) return 2;
            else if (arr.Count < 2) return 1;
            List<int> primes = initPrimes(max);
            int ret = arr.Aggregate(0, (xor, cur) => xor ^ getPrimCnt(cur, ref primes)) == 0 ? 2 : 1;
            //Console.WriteLine();
            return ret;
        }
        public static char zeroMoveNim(List<int> p) // same as nim classic + 1 zero-move per pile is allowed
        {
            return p.Aggregate(0, (xor, cur) => xor ^ (cur % 2 == 0 ? cur - 1 : cur + 1)) == 0 ? 'L' : 'W';
        }
        public static int log2(int n) // n > 0
        {
            int log = 0;
            while ((n >>= 1) > 0) log++;
            return log;
        }
        public static int chocolateInBox(List<int> arr) // how many way to start a NIM game to win
        {
            int totXor = arr.Aggregate(0, (xor, cur) => xor ^ cur);
            return arr.Where(a => a > 0 && (a ^ totXor) < a).Count();
        }
    }
}
