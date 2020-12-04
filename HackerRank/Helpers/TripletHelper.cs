using System;
using System.Linq;

namespace HackerRank
{
    class Triplet : IComparable
    {
        private int[] arr;
        private void Init(int[] a)
        {
            if ((a??new int[0]).Length != 3) throw new InvalidCastException("Wrong type is passed to Triplet ctor");
            arr = a;
        }
        public Triplet(int[] a)
        {
            Init(a);
        }
        public Triplet(string s) 
        {
            Init(Array.ConvertAll((s ?? "").Split(' '), qTemp => Convert.ToInt32(qTemp)));
        }
        public int this[int index]
        {
            get => arr?[index] ?? int.MinValue;
        }
        public int CompareTo(object obj)
        {
            if(obj == null) throw new ArgumentNullException("Cannot compare to null");
            return this[2].CompareTo(((Triplet)obj)[2]);
        }
    }
    class TripletHelper
    {
        static int findRoot(int x, int[] sets)
        {
            while (sets[x] >= 0)
            {
                x = sets[x];
            }
            return x;
        }

        static void merge(int rx, int ry, int[] sets)
        {
            if (rx == ry) return;
            int sx = -sets[rx];
            int sy = -sets[ry];
            if (sx >= sy)
            {
                sets[rx] = -sx - sy;
                sets[ry] = rx;
            }
            else
            {
                sets[ry] = -sx - sy;
                sets[rx] = ry;
            }
        }

        static long c3n(int n) => (long)n * (n - 1) * (n - 2) / 6L;
        static long c2n(int n) => (long)n * (n - 1) / 2L;

        public static long CountRedTriplets(string[] queries)
        {
            int n = queries.Count() + 1;
            int[] sets = Enumerable.Repeat(-1, n).ToArray();

            foreach (string q in queries)
            {
                string[] cmd = q.Split(' ');
                if (cmd[2][0] == 'r') continue;
                merge(findRoot(int.Parse(cmd[0]) - 1, sets),
                      findRoot(int.Parse(cmd[1]) - 1, sets), sets);
            }

            // Answer < n^3 = 1e15 < 2^50, so it's safe to use [long].

            // Number of all unique triplets.
            long ans = c3n(n);
            foreach (int v in sets)
            {
                if (v < 0)
                {
                    // -v is the size of this set.

                    // Minus number of unique triplets with all 3 nodes in this set.
                    ans -= c3n(-v);

                    // Minus number of unique triplets with 2 nodes in this set and
                    // 1 node in another set.
                    ans -= c2n(-v) * (n + v);
                }
            }
            return ans % 1000000007;
        }

    }
}
