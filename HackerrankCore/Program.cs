using System;
using System.Linq;
using System.Collections.Generic;

namespace HackerrankCore
{
    partial class Solution
    {
        static void Main(string[] args)
        {
            //long cntPrsOfSum10_4 = solveDivPairs(10, 4); // 10
            //long cntPrsOfSum7_3 = solveDivPairs(7, 3); // 7
            long cntPrsOfSum1000000000_2 = solveDivPairs(1000000000, 2); // 249999999500000000
            //long cntPrsOfSum46931070_183 = solveDivPairs(46931070, 183); //6017828645940 [39]
            //long cntPrsOfSum49760679_186 = solveDivPairs(49760679, 186);
            //long cntPrsOfSum89641571_149 = solveDivPairs(89641571, 149);
            //long cntPrsOfSum92393744_105 = solveDivPairs(92393744, 105);
            //long cntPrsOfSum39314228_86 = solveDivPairs(39314228, 86);
            //long cntPrsOfSum15990660_90 = solveDivPairs(15990660, 90);
            //long cntPrsOfSum50158474_20 = solveDivPairs(50158474, 20);
            //long cntPrsOfSum31105252_30 = solveDivPairs(31105252, 30);
            long cntPrsOfSumTest3 = solveDivPairs(28994546, 65);
            string evnTot = solveEvenQuadDiv(843408);
            //int extGCD =  gcdExtended(545349, 916192, out int x, out int y);
            long numJokes = solveDateJoke(Enumerable.Repeat(new List<int> { 12, 31 }, 100000).ToList());
            //int numJokes = solveDateJoke(new List<List<int>> { new List<int> { 10, 11 }, new List<int> { 11, 10 } });
            string isFib = isFibo(34);
            int inv = solvePowMod(545349, 584, 916192);
            int clsNum = closestNumber(674, 2, 6);
            Console.WriteLine("Hello .NET 5.0!");
        }
    }
}
