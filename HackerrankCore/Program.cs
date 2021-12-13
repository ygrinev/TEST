using System;
using System.Linq;
using System.Collections.Generic;

namespace HackerrankCore
{
    partial class Solution
    {
        static void Main(string[] args)
        {
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
