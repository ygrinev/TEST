using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class StonePilesHelper
    {
        static void get_grundy(int prev, int start, int rest, int key, ref int[] grundy, bool[,] record)
        {
            for (int i = start; i <= rest / 2; i++)
            {
                if (rest - i > i)
                {
                    int res = prev ^ grundy[i] ^ grundy[rest - i];
                    record[key, res] = true;
                    get_grundy(prev ^ grundy[i], i + 1, rest - i, key, ref grundy, record);
                }
            }

            int k = 0;

            while (record[key, k]) ++k;

            grundy[key] = k;
        }
        public static string stonePiles(List<int> arr)
        {

            bool[,] record = new bool[51, 51];
            int[] grndArr = Enumerable.Repeat(-1, 51).ToArray();
            grndArr[0] = grndArr[1] = grndArr[2] = (grndArr[3] = 1) - 1; // 0,0,0,1


            for (int i = 4; i <= 50; i++) get_grundy(0, 1, i, i, ref grndArr, record);

            return arr.Aggregate(0, (xor, a) => xor ^ grndArr[a]) == 0 ? "BOB" : "ALICE";
        }
    }
}
