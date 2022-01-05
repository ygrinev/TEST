using System;
using System.Linq;
using System.Collections.Generic;

namespace HackerrankCore
{
    partial class Solution
    {
        static void Main(string[] args)
        {
            int modOfOnes52316 = solveModOfOnes(52316, 49422); // 19181
            int modOfOnes3 = solveModOfOnes(3, 3); // 0
            int modOfOnes4 = solveModOfOnes(4, 7); // 5 = 1111%7
            int modOfOnes5 = solveModOfOnes(5, 7); // 2 = 11111%7
            int modOfOnes7 = solveModOfOnes(7, 50); // 11 = 1111111%50
            int modOfOnes37 = solveModOfOnes(37, 6); // 1
            int modOfOnes66 = solveModOfOnes(66, 19); // 7
            long mod2Pow = minPow2Mod(1073741824, 1000000007);
            int maxPlusProd = twoPluses(new List<string> { "GGGGGGGGGGGG", "GGGGGGGGGGGG", "BGBGGGBGBGBG", "BGBGGGBGBGBG", "GGGGGGGGGGGG", "GGGGGGGGGGGG", "GGGGGGGGGGGG", "GGGGGGGGGGGG", "BGBGGGBGBGBG", "BGBGGGBGBGBG", "BGBGGGBGBGBG", "BGBGGGBGBGBG", "GGGGGGGGGGGG", "GGGGGGGGGGGG" }); // 189=9*21
            //int maxPlusProd = twoPluses(new List<string> { "GGGGGGG", "BGBBBBG", "BGBBBBG", "GGGGGGG", "GGGGGGG", "BGBBBBG" }); // 5
            //int maxPlusProd1 = twoPluses(new List<string> { "BGBBGB", "GGGGGG", "BGBBGB", "GGGGGG", "BGBBGB", "BGBBGB" });  // 25
            //int maxPlusProd2 = twoPluses(new List<string> { "GGGGGG", "GBBBGB", "GGGGGG", "GGBBGB", "GGGGGG" }); // 5
            //int maxPlusProd = twoPluses(new List<string> { "GBGBGGB", "GBGBGGB", "GBGBGGB", "GGGGGGG", "GGGGGGG", "GBGBGGB", "GBGBGGB" }); // 45
            long cntPrsOfSum1000000000_2 = solveDivPairs(1000000000, 2); // 249999999500000000
            //long cntPrsOfSum10_4 = solveDivPairs(10, 4); // 10
            //long cntPrsOfSum7_3 = solveDivPairs(7, 3); // 7
            //long cntPrsOfSum46931070_183 = solveDivPairs(46931070, 183); //6017828645940 [39]
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
