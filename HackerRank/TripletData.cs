﻿using System.Linq;

namespace HackerRank
{
    public static class TripletData
    {
        public static string[] data0(int n)
        {
            return new string[n - 1].Select((e, i) => $"1 {i + 2} r").ToArray();
        }
        public static string[] data1(int n)
        {
            return new string[n - 1].Select((e, i) => $"1 {i + 2} {(i%2 == 0 ? "r" : "b")}").ToArray();
        }
        public static string[] data = new string[] {
            "37 26 r",
            "26 16 b",
            "16 18 b",
            "18 23 r",
            "2 17 r",
            "17 12 b",
            "12 36 r",
            "36 13 b",
            "40 39 b",
            "39 15 b",
            "15 4 b",
            "4 45 b",
            "41 21 b",
            "21 28 r",
            "28 32 b",
            "32 5 b",
            "20 35 b",
            "35 47 b",
            "47 27 r",
            "27 24 r",
            "29 43 b",
            "43 11 r",
            "11 34 r",
            "34 50 b",
            "3 42 b",
            "42 9 b",
            "9 1 r",
            "1 25 b",
            "46 14 b",
            "14 44 b",
            "44 7 b",
            "7 22 b",
            "49 48 b",
            "48 10 b",
            "10 8 b",
            "8 30 b",
            "33 31 b",
            "31 6 b",
            "6 38 b",
            "38 19 b",
            "18 2 b",
            "13 40 b",
            "15 28 b",
            "5 24 r",
            "24 29 b",
            "34 9 b",
            "1 14 b",
            "7 8 b",
            "30 31 r"
        };
    }
}
