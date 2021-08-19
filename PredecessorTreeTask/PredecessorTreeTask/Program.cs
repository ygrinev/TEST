using HackerRank.Helpers;
using System;

namespace PredecessorTreeTask
{
    partial class Solution
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new EvenTreeHelper(10).evenForest(new string[] { "2 1", "3 1", "4 3", "5 2", "6 1", "7 2", "8 6", "9 8", "10 8" })); // 2
            Console.WriteLine("Press any key..."); // 2
            Console.ReadKey();
        }
    }
}
