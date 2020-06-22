using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    using System.CodeDom.Compiler;
    using System.Collections.Generic;
    using System.Collections;
    using System.ComponentModel;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.Serialization;
    using System.Text.RegularExpressions;
    using System.Text;
    using System;

    class Node
    {
        public Node left, right;
        public int data;
        public Node(int data)
        {
            this.data = data;
            left = right = null;
        }
    }

    class Solution
    {
        //static void minimumBribes(int[] q)
        //{
        //    int bribe = 0;
        //    bool chaotic = false;
        //    int n = q.Length;
        //    for (int i = 0; i < n; i++)
        //    {
        //        if (q[i] - (i + 1) > 2)
        //        {
        //            chaotic = true;
        //            break;
        //        }
        //        for (int j = Math.Max(0, q[i] - 2); j < i; j++)
        //            if (q[j] > q[i])
        //                bribe++;
        //    }
        //    if (chaotic)
        //        Console.WriteLine("Too chaotic");
        //    else
        //        Console.WriteLine(bribe);
        //}

        static void minimumBribes(int[] q)
        {
            long v = long.MinValue;
         int n = q.Length;
           Dictionary<int, Dictionary<int, long>> segs = new Dictionary<int, Dictionary<int, long>>{ { 0, new Dictionary<int, long>{ { n, 0L } } }};
            var idx = segs.ElementAt(0);
            if (n < 2) return;
            Dictionary<int, int> cntr = new Dictionary<int, int>();
            int count = 10, total = 0;
            // bubble sort
            for (int i = 0; i < n - 1 && count > 0; i++)
            {
                count = 0;
                for (int j = 0; j < n - 1; j++)
                {
                    if (q[j] > q[j + 1])
                    {
                        count++;
                        int tmp = q[j];
                        q[j] = q[j + 1];
                        q[j + 1] = tmp;
                        //cntr[q[j]] = cntr.ContainsKey(q[j]) ? (cntr[q[j]] + 1) : 1;
                        cntr[q[j + 1]] = cntr.ContainsKey(q[j + 1]) ? (cntr[q[j + 1]] + 1) : 1;
                    }
                }
                total += count;
            }
            Console.WriteLine(total);
            if (cntr.Keys.Select((key) => cntr[key]).Max() > 2)
            {
                Console.WriteLine("Too chaotic");
            }
        }
        interface IVehicle
        {
            int GetHorsepower();
        }

        public class Car : IVehicle
        {
            int horsepower;
            public Car()
            {
                horsepower = 100;
            }

            int IVehicle.GetHorsepower()
            {
                return horsepower;
            }
        }
        static int minVal(int[] A)
        {
            List<int> list = new List<int> { 1, 2, 3, 4 };
            //int num = list.ElementAt(list.Count);
            Random i = new Random();
            int v = Math.Abs(i.Next());
            // write your code in C# 6.0 with .NET 4.5 (Mono)
            int n = 1;
            foreach(var num in A.OrderBy(ii => ii))
            {
                if (num < 0)
                    continue;
                if (n < num)
                    return n;
                else
                    n = num + 1;

            }
            return n;
        }

        static int divisorSum(int n)
        {
            int sqrt = (int)Math.Sqrt(n);
            int sum = 0;
            int div = 0;
            while (++div <= sqrt)
            {
                if (n % div == 0)
                {
                    int div2 = n / div;
                    sum += div + (div == div2 ? 0 : div2);
                }
            }
            return sum;
        }

        static void PrintArray<T>(T[] A)
        {
            Array.ForEach(A, a => Console.WriteLine(a));
        }
        static int getHeight(Node root, int h = -1)
        {
            return root == null ? Math.Max(h, 0)
              : Math.Max(getHeight(root.left, h + 1), getHeight(root.right, h + 1));
        }

        static int GetFeeOnReturn(DateTime expDate, DateTime retDate)
        {
            int y1 = expDate.Year, y2 = retDate.Year, m1 = expDate.Month, m2 = retDate.Month, d1 = expDate.Day, d2 = retDate.Day;

            return y2 > y1 ? 10000 
                : (y2 == y1 && m2 > m1) ? (m2 - m1) * 500 
                    : (y2 == y1 && m2 == m1 && d2 > d1) ? (d2 - d1) * 15 : 0;
        }

    static void Main(string[] args)
        {

            int fee = GetFeeOnReturn(new DateTime(2015, 6, 6), new DateTime(2015, 6, 9));

            int sum = divisorSum(6);
            //var c = new Car().GetHorsepower();
            //Console.WriteLine(((int)Math.Round(12.00 * (1 + (8 + 20) / 100.00))).ToString());
            //string ex = 8.345.ToString("F1");
            //string ee = ex;
            //int t = Convert.ToInt32(Console.ReadLine());

            //for (int tItr = 0; tItr < t; tItr++)
            //{
            //    int n = Convert.ToInt32(Console.ReadLine());

            //    int[] q = Array.ConvertAll(Console.ReadLine().Split(' '), qTemp => Convert.ToInt32(qTemp))
            //    ;
            //    minimumBribes(q);
            //}
            //1 5 3
            //4 8 7
            //6 9 1
            /**/
            int[][] queries = new int[][] { 
            new int[] {29, 40, 787},
            new int[] {9, 26, 219},
            new int[] {21, 31, 214},
            new int[] {8, 22, 719},
            new int[] {15, 23, 102},
            new int[] {11, 24, 83},
            new int[] {14, 22, 321},
            new int[] {5, 22, 300},
            new int[] {11, 30, 832},
            new int[] {5, 25, 29},
            new int[] {16, 24, 577},
            new int[] {3, 10, 905},
            new int[] {15, 22, 335},
            new int[] {29, 35, 254},
            new int[] {9, 20, 20},
            new int[] {33, 34, 351},
            new int[] {30, 38, 564},
            new int[] {11, 31, 969},
            new int[] {3, 32, 11},
            new int[] {29, 35, 267},
            new int[] {4, 24, 531},
            new int[] {1, 38, 892},
            new int[] {12, 18, 825},
            new int[] {25, 32, 99},
            new int[] {3, 39, 107},
            new int[] {12, 37, 131},
            new int[] {3, 26, 640},
            new int[] {8, 39, 483},
            new int[] {8, 11, 194},
            new int[] {12, 37, 502}
                        };
            int len = 40;
            //int[][] queries = new int[][] {
            //new int[]{ 1, 2, 100 },
            //new int[]{ 2, 5, 100 },
            //new int[]{ 3, 4, 100 }
            //            };
            //int len = 10;     
            //long res = arrayManipulation(40, queries); // res = 10 !!!
            long res = arrayManipulation(len, queries); // res = 10 !!!
            Console.WriteLine(res);
            Console.ReadKey();

        }
        // Complete the hourglassSum function below.
        //static int hourglassSum(int[][] arr)
        //{
        //    int[] a = new int[10];
        //    int sum = Int32.MinValue;
        //    for (int i = 0; i < arr.Length - 2; i++)
        //    {
        //        for (int j = 0; j < arr[0].Length - 2; j++)
        //        {
        //            Console.Write(arr[i][j].ToString() + " ");
        //            int tmpSum = arr[i][j] + arr[i][j + 1] + arr[i][j + 2]
        //                + arr[i + 1][j + 1]
        //                + arr[i + 2][j] + arr[i + 2][j + 1] + arr[i + 2][j + 2];
        //            if (tmpSum > sum)
        //            {
        //                sum = tmpSum;
        //            }
        //        }
        //        Console.Write("\r\n");
        //    }
        //    return sum;
        //}

        //static void Main(string[] args)
        //{
        //    //TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        //    int[][] arr = new int[6][];

        //    for (int i = 0; i < 6; i++)
        //    {
        //        arr[i] = Array.ConvertAll(Console.ReadLine().Split(' '), arrTemp => Convert.ToInt32(arrTemp));
        //    }

        //    int result = hourglassSum(arr);
        //    Console.WriteLine("The HG sum is; " + result.ToString());
        //    Console.WriteLine("Press any key to exit...");

        //    Console.ReadKey();

        //textWriter.WriteLine(result);

        //textWriter.Flush();
        //textWriter.Close();
        public static string ReverseIntArray(int[] arr)
        {
            return string.Join(" ", arr.Select((val, idx) => new { Value = val, Index = idx }).OrderByDescending(el => el.Index).Select(item => item.Value));
        }
        public static int IndexOf(List<int> search, List<int> find)
        {
            foreach (int item in find)
            {
                for(int i = 0; i < search.Count; i++)
                {
                    for(int j = 0; j < find.Count && find.ElementAt(j) == search.ElementAt(j); j++)
                    {
                        if (j == find.Count - 1)
                            return i;
                    }

                }
            }
            return -1;
        }
        public static string ReverseStringIterative(string input)
        {
            if (string.IsNullOrEmpty(input))
                return input;
            char[] arr = input.ToCharArray();
            int iStart = 0, iEnd = input.Length - 1;
            while (iStart < iEnd)
            {
                // swap arr items
                char tmp = arr[iStart];
                arr[iStart] = arr[iEnd];
                arr[iEnd] = tmp;
                iStart++;
                iEnd--;
            }
            return new string(arr);
        }
        static long arrayManipulation(int n, int[][] queries)
        {
            int[,] arr = new int[queries.Length * 2, 2]; // [][idx, val]
            for (int i = 0; i < queries.Length; i++) // find max and min idx
            {
                int min = n + 1, minVal = 0, max = -1, maxVal = 0, qMin = n+1, qMax = -1;
                bool isMinRight = false, isMaxRight = false;
                for (int j = 0; j < queries.Length; j++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int qIdx = queries[j][k];
                        if (qIdx < 0)
                            continue;
                        if (k == 1)
                            qIdx++;
                        int val = queries[j][2] * (k == 0 ? 1 : -1);
                        if (qMin > qIdx)
                        {
                            qMin = qIdx;
                            min = j;
                            minVal = val;
                            isMinRight = k == 1;
                        }
                        if (qMax < qIdx)
                        {
                            qMax = qIdx;
                            max = j;
                            maxVal = val;
                            isMaxRight = k == 1;
                        }
                        if(qMin == qIdx && val < minVal)
                        {
                            min = j;
                            minVal = val;
                            isMinRight = k == 1;
                        }
                        if (qMax == qIdx && val > minVal)
                        {
                            max = j;
                            maxVal = val;
                            isMaxRight = k == 1;
                        }
                    }
                }
                // put i<-[min, minValue]
                int kIdx = isMinRight ? 1 : 0;
                arr[i, 0] = queries[min][kIdx] + kIdx;
                queries[min][kIdx] = -1;
                arr[i, 1] = minVal;
                int iLeft = i;
                while (iLeft - 1 >= 0
                && arr[i, 0] == arr[iLeft - 1, 0]
                && arr[i, 1] < 0 && arr[iLeft - 1, 1] >= 0)
                    iLeft--;
                if (iLeft < i)
                {
                    int tmp = arr[i, 1];
                    arr[i, 1] = arr[iLeft, 1];
                    arr[iLeft, 1] = tmp;
                }

                // put queries.Length - i<-[max, maxValue]
                kIdx = isMaxRight ? 1 : 0;
                int im = 2*queries.Length - i - 1;
                arr[im, 0] = queries[max][kIdx] + kIdx;
                queries[max][kIdx] = -1;
                arr[im, 1] = maxVal;
                int iRight = im;
                while (iRight + 1 < 2 * queries.Length
                && arr[im, 0] == arr[iRight + 1, 0]
                && arr[im, 1] >= 0 && arr[iRight + 1, 1] < 0)
                    iRight++;
                if (iRight > im)
                {
                    int tmp = arr[i, 1];
                    arr[i, 1] = arr[iRight, 1];
                    arr[iRight, 1] = tmp;
                }
            }
            long totMax = long.MinValue, total = 0;
            for (int i = 0; i < arr.Length/2; i++)
            {
                total += arr[i, 1];
                if (totMax < total)
                    totMax = total;
            }
            return totMax;
        }
    }

    class Person
    {
        protected string firstName;
        protected string lastName;
        protected int id;

        public Person() { }
        public Person(string firstName, string lastName, int identification)
        {
            this.firstName = firstName;
            this.lastName = lastName;
            this.id = identification;
        }
        public void printPerson()
        {
            Console.WriteLine("Name: " + lastName + ", " + firstName + "\nID: " + id);
        }
    }

    class Student : Person
    {
        readonly char[] symbols = "OEAPDT".ToCharArray();
        private int[] testScores;
        readonly int[,] rules = new int[,] {
            { 90, 101},
            { 80, 90},
            { 70, 80},
            { 55, 70},
            { 40, 55},
            { int.MinValue, 40}
        };
        public char Calculate()
        {
            double avg = testScores.Average();
            return symbols.Where((s,idx) => rules[idx,0] <= avg && avg < rules[idx, 1]).FirstOrDefault();
        }
    }
    class Difference
    {
        private int[] elements;
        public int maximumDifference;

        public int computeDifference(int[] a)
        {
            return a.Max() - a.Min();
        }

    }
}

