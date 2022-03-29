using HackerrankCore.Models;
using HackerrankCore.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;

namespace HackerrankCore
{
    partial class Solution
    {
        public static List<int> solveRussPeas(int a, int b, long k, int m)
        {
            var p = powRussPeas(new ComplexInt(a, b, m), k);
            return new List<int> { p.a, p.b };
        }
        public static ComplexInt powRussPeas(ComplexInt x, long k)
        {
            ComplexInt p = k % 2 == 0 ? new ComplexInt(1, 0, x.mod) : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static string solveReachFromTo(long a, long b, long x, long y) // is it possible to get from pt(a,b) to pt(x,y) ?
        {
            // (a,b) => (a±b,b) or (a,b±a)
            return IntOp.GCD(a, b) == IntOp.GCD(x, y) ? "YES" : "NO";
        }
        public static int powRussPeas(int x, long k)
        {
            int p = k % 2 == 0 ? 1 : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static BigInteger powRussPeas(BigInteger x, BigInteger k)
        {
            BigInteger p = k % 2 == 0 ? 1 : x;
            while ((k >>= 1) > 0)
            {
                x *= x;
                if (k % 2 == 1)
                    p *= x;
            }
            return p;
        }
        public static long powRussPeasMod(int x, long k, int mod = 0)
        {
            long p = k % 2 == 0 ? 1 : x, pow = x;
            while ((k >>= 1) > 0)
            {
                pow *= pow; if (mod > 0) pow %= mod;
                if (k % 2 == 1)
                { p *= pow; if (mod > 0) p %= mod; }
            }
            return p;
        }
        public static int closestNumber(int a, int b, int x)
        {
            int p = b >= 0 ? powRussPeas(a, b) : a == 1 ? 1 : 0;
            int mod = p % x;
            return p == 0 ? 0 : p >= x ? (mod < x - mod ? (p - mod) : (p+x-mod)) : p < x - p ? 0 : x;
        }
        public static string minGCD(List<int> a) // detect if there exists not empty subset with GCD == 1
        {
            var dist = a.Distinct();
            if (dist.Count() == 1) return dist.ElementAt(0) > 1 ? "NO" : "YES";
            return dist.OrderBy(d => d).Aggregate((cur, next) => IntOp.GCD(cur, next)) == 1 ? "YES" : "NO";
        }
        public static int getModInverse(int a, int mod, int r = 1) // a and mod - coprimes
        {
            if (a > mod) a %= mod;
            int b = mod % a, fctr = ((mod / a + 1)*r)%mod;
            if (b == a - 1) return fctr;
            else if (b == 1) return (int)((mod / a * (long)(mod - 1)) % mod);
            int newA = a-b;
            return getModInverse(newA, mod, fctr);
        }
        public static int gcdExtended(int a, int b, out int x, out int y)
        {
            // Base Case
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            // To store results of recursive call
            int gcd = gcdExtended(b % a, a, out int x1, out int y1);

            // Update x and y using results of recursive call
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
        public static long gcdExtended(long a, long b, out long x, out long y)
        {
            // Base Case
            if (a == 0)
            {
                x = 0; y = 1;
                return b;
            }
            // To store results of recursive call
            long gcd = gcdExtended(b % a, a, out long x1, out long y1);

            // Update x and y using results of recursive call
            x = y1 - (b / a) * x1;
            y = x1;

            return gcd;
        }
        public static int solvePowMod(int a, int b, int x)
        {
            int fa = a;
            if (b < 0)
            {
                int gcd = gcdExtended(a, x, out fa, out int fb);
                fa = fa < 0 ? fa + x : fa;
            }
            return (int)BigInteger.ModPow(fa, Math.Abs(b), x); // powRussPeas(fa, b, x);
        }
        public static long[] fibs = new long[2000];
        public static int lastIdx = 0;
        public static string isFibo(long n)
        {
            if (lastIdx == 0) fibs[++lastIdx] = 1;
            int prevIdx = lastIdx;
            while (fibs[lastIdx] < n)
            {
                fibs[++lastIdx] = fibs[lastIdx - 1] + fibs[lastIdx - 2];
            }
            if (fibs[lastIdx] == n) return "IsFibo";
            if (prevIdx == lastIdx)
            {
                int left = 0, right = lastIdx;
                do
                {
                    if (fibs[left] == n || fibs[right] == n) return "IsFibo";
                    int mid = (left + right) / 2;
                    if (fibs[mid] < n) left = mid;
                    else right = mid;
                }
                while (left < right - 1);
                if (fibs[left] == n || fibs[right] == n) return "IsFibo";
            }
            return "IsNotFibo";
        }
        private static int ConvertToBase10(List<int> pr)
        {
            int bs = pr.ElementAt(0), nm = pr.ElementAt(1);
            if (bs < 10 && nm.ToString().Any(c => c - '0' >= bs)) return -1;
            long pow = 1;
            long ret = 0;
            while (nm > 0)
            {
                ret += (nm % 10) * pow;
                pow *= bs;
                nm /= 10;
            }
            return (int)ret;
        }
        public static long solveDateJoke(List<List<int>> dates)
        {
            return (long)dates.Select(pr => ConvertToBase10(pr)).Where(n => n >= 0).GroupBy(k => k).Select(g => (long)g.Count() * ((long)g.Count() - 1) / 2).Aggregate(0L, (sum, cur) => sum + cur);
        }
        private static List<int> CalcNext(ref int prev, List<int> lst, int cur)
        {
            int gcd = IntOp.GCD(prev, cur);
            lst.Add((prev / gcd) * (cur / gcd) * gcd);
            prev = cur;
            return lst;
        }
        public static List<int> solveJohnGCD(List<int> a)
        {
            int prev = a.ElementAt(0), prevB = prev;
            List<int> ret = a.Skip(1).Aggregate(new List<int> { prev }, (lst, cur) => CalcNext(ref prev, lst, cur)).ToList();
            ret.Add(a.Last());
            return ret;
        }
        private static int[] quads = new int[498].AsEnumerable().Select((n, i) => 4 * (i + 1) * (i + 1)).ToArray();
        private static bool contains(int div)
        {
            int left = 0, right = 497;
            do
            {
                if (quads[left] == div || quads[right] == div) return true;
                int mid = (left + right) / 2;
                if (quads[mid] < div) left = mid;
                else right = mid;
            }
            while (left < right - 1);
            return quads[left] == div || quads[right] == div;
        }
        private static int getDivs(int n, out int evn)
        {
            int cnt = 1;
            evn = 0;
            for (int i = 2; i <= (int)Math.Sqrt(n); i++)
            {
                if (n % i == 0)
                {
                    cnt += n / i == i ? 1 : 2;
                    if(contains(i)) evn++;
                    if(n / i != i && contains(n/i)) evn++;
                }
            }
            return cnt;
        }
        public static string solveEvenQuadDiv(int n)
        {
            if (n < 8 || n % 4 != 0) return "0";
            int tot = getDivs(n, out int evn);
            int gcd = IntOp.GCD(evn, tot);
            return $"{evn/gcd}/{tot/gcd}";
        }
        private static string calcRow(string cur, string prev, string prev2 = null)
        {
            int len = (cur ?? "").Length;
            return string.Join("", prev.Select((c, i) => c == 'O' || (i > 0 && prev[i - 1] == 'O') || (i < len - 1 && prev[i + 1] == 'O') || cur[i] == 'O' || (prev2 != null && prev2[i] == 'O') ? '.' : 'O'));
        }
        private static List<string> afterExpls(List<string> grid)
        {
            int cnt = 0, len = grid[0].Length, sz = grid.Count;
            string prev = new string('.', len), prev2 = new string('.', len);
            return grid.Aggregate(new List<string>(), (lst, cur) => {
                if (cnt++ > 0) lst.Add(calcRow(cur, prev, cnt > 1 ? prev2 : null));
                if (cnt == sz) lst.Add(calcRow(prev, cur));
                prev2 = prev; prev = cur;
                return lst;
            });
        }
        public static List<string> bomberMan(int n, List<string> grid)
        {
            if (n < 2) return grid;
            if (n % 2 == 0) return grid.Select(s => new string('O', s.Length)).ToList();
            List<string> res3 = afterExpls(grid), res5 = afterExpls(res3);
            return n % 4 == 3 ? res3 : res5;
        }
        public static int solveAntiPalindrome(int n, int m)
        {
            return n == 1 ? m % 1000000007 : (int)((BigInteger.ModPow(m - 2, n - 2, 1000000007) * m * (m - 1)) % 1000000007);
        }
        public static long countPairsOfSum(long n, long sum)
        {
            return sum < 3 || 2 * n <= sum ? 0 : Math.Min((sum - 1) / 2, n - sum / 2);
        }
        public static long solveDivPairs(int n, int k)
        {
            long max = n / k, mod = n % k;
            return (k - 1) / 2 * ((max + 1) * max / 2) + (k / 2 + 1) * ((max - 1) * max / 2) + mod * max + Math.Max(mod - k / 2, 0);
        }
        private static int[,] mtx;
        private static void initMtx(List<string> grid)
        {
            int rowIdx = 0;
            mtx = grid.Aggregate(new int[grid[0].Length, grid.Count], (m, s) =>
            {
                s.Aggregate(0, (idx, c) =>
                {
                    m[idx, rowIdx] = c.ToString().ToUpper() == "G" ? 1 : 0;
                    return ++idx;
                });
                rowIdx++;
                return m;
            });

        }
        private static int getSingleProd(int k1, int[] coord1, int k2, int[] coord2)
        {
            if (k1 == 3 && coord1[0] == 9 && coord1[1] == 4 && k2 == 6 && coord2[0] == 5 && coord2[1] == 7)
            { }
            int dx = Math.Abs(coord1[0] - coord2[0]), dy = Math.Abs(coord1[1] - coord2[1]);
            if (dx > dy) { dx ^= dy; dy ^= dx; dx ^= dy; } // i.e. make dx <= dy
            if (k1 > k2) { k1 ^= k2; k2 ^= k1; k1 ^= k2; } // i.e. make k1 <= k2


            while (dx == 0 && k2 + k1 > dy + 1)
            {
                if (k2 > k1) k2--; else k1--;
            }
            if (dx > 0 && dy < k1) // i.e. dx < k1 && dx < k2 - 2 intersections
            {
                if ((1 + 4 * (dy - 1)) * (1 + 4 * (dy - 1)) > (1 + 4 * (dx - 1)) * (1 + 4 * (k2 - 1))) { k1 = dy; k2 = dy; } else { k1 = dx; }
            }
            else if (dx > 0 && dx < k1 && dy < k2) // 1 intersection
            {
                if ((1 + 4 * (dx - 1)) * (1 + 4 * (k2 - 1)) > (1 + 4 * (k1 - 1)) * (1 + 4 * (dy - 1))) k1 = dx; else k2 = dy;
            }
            return (1 + 4 * (k1 - 1)) * (1 + 4 * (k2 - 1));
        }
        private static int getMaxProdSameKey(int k, IEnumerable<int[]> lstCoords) // , ref int max, ref int min
        {
            int[] coords1 = lstCoords.ElementAt(0);

            return Math.Max(lstCoords.Skip(1).Aggregate(0, (prod, cur) => Math.Max(prod, getSingleProd(k, cur, k, coords1))),
                lstCoords.Count() > 1 ? getMaxProdSameKey(k, lstCoords.Skip(1)) : 0);
        }
        private static int getMaxProd2Lists(int k1, IEnumerable<int[]> lstCoordsLo, int k2, IEnumerable<int[]> lstCoordsHi) // , ref int max, ref int min
        {
            return lstCoordsHi.Aggregate(0, (prod, curHi) => Math.Max(prod, lstCoordsLo.Aggregate(prod, (prd, curLo) => Math.Max(prd, getSingleProd(k1, curLo, k2, curHi)))));
        }
        public static int twoPluses(List<string> grid)
        {
            initMtx(grid);
            int max = 0, totCnt = 0;
            var dict = new Dictionary<int, List<int[]>>();
            for (int j = 0; j < mtx.GetLength(1); j++) // cols-loop
                for (int i = 0; i < mtx.GetLength(0); i++) // rows-loop
                {
                    if (mtx[i, j] == 0) continue;
                    int k = 1;
                    while (i - k >= 0 && i + k < mtx.GetLength(0) && j - k >= 0 && j + k < mtx.GetLength(1)
                    && mtx[i - k, j] * mtx[i + k, j] * mtx[i, j - k] * mtx[i, j + k] == 1) k++;
                    max = Math.Max(max, k);
                    totCnt++;
                    if (dict.ContainsKey(k)) dict[k].Add(new int[] { i, j });
                    else dict[k] = new List<int[]> { new int[] { i, j } };
                }
            if (totCnt < 2) return 0;
            if (max == 1) return 1;
            int idx = 0;
            var ordKeys = dict.Keys.OrderByDescending(k => k);
            return ordKeys.Aggregate(0, (prod, k) =>
            {
                if (prod < (1 + 4 * (k - 1)) * (1 + 4 * (k - 1)))
                {
                    prod = Math.Max(prod, getMaxProdSameKey(k, dict[k]));
                    if (idx++ < dict.Keys.Count)
                        prod = Math.Max(prod, ordKeys.Skip(idx).Aggregate(prod, (p, kk) => p < (1 + 4 * (k - 1)) * (1 + 4 * (kk - 1)) ? Math.Max(p, getMaxProd2Lists(kk, dict[kk], k, dict[k])) : p));
                }
                return prod;
            });
        }
        public static int solveZeroXorSubsets(long n)
        {
            // 2^((2^n)-n)
            return (int)BigInteger.ModPow(2, BigInteger.ModPow(2, n, 73699066) - n, 1000000007);
        }
        public static long minPow2Mod(long n, int mod)
        {
            long gcd = gcdExtended(n, mod, out long fn, out long dummy);
            return fn;
        }
        private static long[] initMods(int m, int len)
        {
            long[] mods = new long[len];
            long d = 10 % m, idx = 1;
            mods[0] = 1; mods[1] = 11 % m;
            while (idx++ < len-1)
            {
                d = (d * d) % m;
                mods[idx] = (mods[idx - 1]*(d + 1)) % m;
            }
            return mods;
        }
        public static int solveModOfOnes(long n, int m)
        {
            if (n-- < 2) return 1;
            long d = 1, r = 0, res = 1;
            do
            {
                r = r == 0 ? 1 : (r * d % m + r) % m;
                d = d == 1 ? 10 % m : d * d % m;
                res = n % 2 == 1 ? (res * d % m + r) % m : res;
            }
            while ((n >>= 1) > 0);
            return (int)res;
        }
        private static int longStrToModNum(string a, int m)
        {
            int offs = 18, len = a.Length, idx = len - ((len-1)%offs + 1);
            long fctr = long.Parse($"1{new string('0', (len-1)%offs + 1)}") % m, res = long.Parse(a.Substring(idx)) % m;
            long mod10s = long.Parse($"1{new string('0', offs)}") % m;
            while (idx > 0)
            {
                idx -= offs;
                res = (res + long.Parse(a.Substring(idx, offs)) % m * fctr) % m;
                fctr = fctr * mod10s % m;
            }
            return (int)res;
        }
        public static int solveBigIntModPow(string a, string b) // mod = 1000000007, 
        {
            return (int)BigInteger.ModPow(longStrToModNum(a,1000000007), longStrToModNum(b,1000000006), 1000000007);
        }
        public static int countXorZeroSubsets(long n)
        {
            // 2^((2^n)-n), using little Fermat's theorem: for 2 co-prime a,b => a^(b-1)%b == b^(a-1)%a == 1 => 2^N%m == 2^((N-1)%m)%m
            return (int)BigInteger.ModPow(2, (powRussPeasMod(2, n, 1000000006) - n % 1000000006 + 1000000006) % 1000000006, 1000000007);
        }
        public static void almostSorted(List<int> arr)
        {
            int prev2 = -1, prev = arr.First(), idx = 0, len = arr.Count,
                z1Offs = 0, z1Idx = -1, z1Max = -1, z1Min = -1, z1L = -1, z1R = -1,
             /*z2Offs = 0,*/z2Idx = -1, z2Val = -1, z2L = -1, z2R = -1;
            bool up = true;
            string ret = arr.Skip(1).Aggregate("yes", (r, n) =>
            {
                if (r == "no") 
                    return r;

                if (up)
                {
                    if (n < prev) // set up left edge of zigzag
                    {
                        //max = Math.Max(max, prev);
                        if(z2R > 0 || z1Offs > 1)
                        {
                            return "no";
                        }
                        if (z1Idx < 0) // set up left edge of Z1
                        {
                            z1Idx = idx;
                            z1Max = prev;
                            z1L = prev2;
                            z1Offs = 1;
                            if (idx+1 == len - 1)
                            {
                                z2Idx = idx + 1;
                                z2Val = n;
                                z2L = prev2;
                                z1R = z2R = prev + 1;
                            }
                        }
                        else if(z2Idx < 0) // set up left edge of Z2
                        {
                            z2Idx = idx+1;
                            z2Val = n;
                            z2L = prev;
                            if (idx+1 == len - 1)
                            {
                                z2R = z1Max + 1;
                            }
                        }
                        up = false;
                    }
                }
                else // was going down
                {
                    if (n > prev) // set up right edge of zigzag
                    {
                        if(n < z1L || z2L > -1 && n < z2L || z1Offs > 1 && n < z1Max)
                        {
                            return "no";
                        }
                        if(z2Idx < 0)
                        {
                            z1R = n;
                            z1Min = prev;
                        }
                        else
                        {
                            z2R = n;
                        }
                        up = true;
                    } else
                    {
                        if (z1R > 0)
                        {
                            return "no";
                        }
                        else
                        {
                            z1Offs++;
                            if (idx+1 == len - 1 && z1R < 0)
                            {
                                z1R = z1Max + 1;
                                z1Min = n;
                            }
                        }
                    }
                }
                prev2 = prev; prev = n;
                idx++;
                return "yes";
            });
            if(ret == "yes" && validOrder(z1L, z1Min, z1Max, z2Val, z1R, z2L, z2R))
            { 
                Console.WriteLine("yes");
                if(z1Idx > -1)
                {
                    Console.WriteLine(z2Idx > -1 
                        ? $"swap {z1Idx+1} {z2Idx+1}" 
                        : $"{(z1Offs == 1 ? "swap" : "reverse")} {z1Idx+1} {z1Idx+z1Offs+1}");
                }
            }
            else
            {
                Console.WriteLine("no");
            }
        }
        private static bool validOrder(int z1L, int z1Min, int z1Max, int z2Val, int z1R, int z2L, int z2R)
        {
            return z1Max < 0 || z2Val > 0 && z1L < z2Val && z2Val < z1R && z2L < z1Max && z1Max < z2R
                || z1L < z1Min && z1Max < z1R;
        }
        public static int solveOnesMod(long n, int m)
        {
            // 111...(n=SUM(2^k)) = 10^n+111...(n-1) = 10^n+10^(n-1)+111...(n-2) = SUM[1..n]10^k = (10^n-1)/9 + 1
            // => c: (10^n-1)%m==9*c%m => 
            if (n < 21) return (int)long.Parse(new string('1',(int)n))%m;
            long pow10 = 10 % m, curModOnes = 1;
            return (int)new int[17].Aggregate((n%2)*curModOnes, (mod, i) =>
            {
                if (n > 0)
                {
                    mod = n % 2 == 0 ? mod : ((mod * pow10)%m + curModOnes)%m;
                    curModOnes = (curModOnes * (pow10 + 1)) % m;
                    pow10 = (pow10 * pow10) % m;
                }
                n >>= 1;
                return mod;
            });
        }

        public static int awesDiv(List<int> A, int k)
        {
            int gcd = GCD(A);
            return getMaxDivsInt(gcd, k);
        }
        private static int GCD(List<int> A)
        {
            return A.Aggregate((cur, next) => IntOp.GCD(cur, next));
        }

        private static int getMaxDivsInt(int n, int max = 0)
        {
            if (max == 0) return n;
            int ret = max/n*n;
            int sqrt = (int)Math.Floor(Math.Sqrt(n));
            for (int i = 2; i <= Math.Min(sqrt, max); i++)
            {
                if (n % i == 0)
                {
                    int maxLo = max/i*i;
                    int p = n / i;
                    int maxHi = max/p*p;
                    if(maxLo > ret) ret = maxLo;
                    if(maxHi <= max && maxHi > ret) ret = maxHi;
                }
            }
            return ret;
        }
    }
}
