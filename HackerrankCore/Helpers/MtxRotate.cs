using System;
using System.Collections.Generic;
using System.Linq;

namespace HackerrankCore
{
    class MtxRotate
    {
        static int[][] origin;
        static int[][] cycles;
        static void InitCycleMtx(List<List<int>> src)
        {
            int n = src.Count, m = src.ElementAt(0).Count, min = Math.Min(m, n) / 2, idx = 0;
            cycles = new int[min][];
            origin = src.Aggregate(new int[n][], (cur, r) => {
                cur[idx++] = r.ToArray();
                return cur;
            });
            for (int i = 0; i < min; i++)
            {
                GetOneCycle(i);
            }
        }
        static void GetOneCycle(int i)
        {
            int n = origin.Length, m = origin[0].Length;
            cycles[i] = new int[2 * (m + n) - 4 * (2* i + 1)];
            int idx = 0;
            // get top horiz
            for (int t = i; t < m - i - 1; t++) cycles[i][idx++] = origin[i][t];
            // get right vert
            for (int r = i; r < n - i - 1; r++) cycles[i][idx++] = origin[r][m - i - 1];
            // get bottom horiz
            for (int b = m - i - 1; b > i; b--) cycles[i][idx++] = origin[n - i - 1][b];
            // get left vert
            for (int l = n - i - 1; l > i; l--) cycles[i][idx++] = origin[l][i];
        }
        static int IncrMod(ref int idx, int mod) { int old = idx; idx = (idx + 1) % mod; return old; }
        static void Rotate(int offs)
        {
            for (int i = 0; i < cycles.Length; i++)
            {
                int n = origin.Length, m = origin[0].Length, len = cycles[i].Length;
                int idx = offs < 4 ? offs : offs % len;
                // get top horiz
                for (int t = i; t < m - i - 1; t++)
                    origin[i][t] = cycles[i][IncrMod(ref idx, len)];
                // get right vert
                for (int r = i; r < n - i - 1; r++)
                    origin[r][m - i - 1] = cycles[i][IncrMod(ref idx, len)];
                // get bottom horiz
                for (int b = m - i - 1; b > i; b--)
                    origin[n - i - 1][b] = cycles[i][IncrMod(ref idx, len)];
                // get left vert
                for (int l = n - i - 1; l > i; l--)
                    origin[l][i] = cycles[i][IncrMod(ref idx, len)];
            }
        }
        static void PrintMtx()
        {
            Array.ForEach(origin, r => Console.WriteLine(string.Join(" ", r.Select(i=>$"{i}"))));
            Console.ReadKey();
        }

        public static void MatrixRotation(List<List<int>> matrix, int r)
        {
            InitCycleMtx(matrix);
            Rotate(r);
            PrintMtx();
        }
    }
}
