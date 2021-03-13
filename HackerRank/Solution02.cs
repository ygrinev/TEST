using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank
{
    partial class Solution
    {
        static string morganAndString(string a, string b)
        {
            int i = 0, j = 0, aLen = a.Length, bLen = b.Length;
            int aEq, bEq;
            StringBuilder sb = new StringBuilder();
            while (i < aLen && j < bLen)
            {
                if (a[i] != b[j]){ sb.Append(a[i] > b[j] ? b[j++] : a[i++]); }
                else
                {
                    aEq = i; bEq = j;
                    char ch = a[i];
                    while (i < aLen && j < bLen && a[i] == b[j] && ch <= a[i-1]) { i++; j++; }
                    if (i == aLen && j == bLen
                    || i == aLen && ch < b[j]
                    || j == bLen && ch < a[i]
                    || ch < a[i] && ch < b[j])
                    {   // flush both
                        SmartFlush(ref sb, a, b, ref aEq, ref bEq, i - aEq);
                        //sb.Append(a.Substring(aEq, i - aEq));
                        //sb.Append(a.Substring(aEq, i - aEq));
                    }
                    else if (i == aLen || j == bLen)
                    {
                        //sb.Append(a.Substring(aEq, i - aEq));
                        if (i == aLen)  // flush b first
                            SmartFlush(ref sb, b, a, ref bEq, ref aEq, i - aEq + 1);
                        else j = bEq; // flush a, reverse b
                            SmartFlush(ref sb, a, b, ref aEq, ref bEq, i - aEq + 1);
                    }
                    else
                    {
                        if (a[i] > b[j])  // flush b first
                            SmartFlush(ref sb, b, a, ref bEq, ref aEq, i - aEq + 1);
                        else j = bEq; // flush a first
                            SmartFlush(ref sb, a, b, ref aEq, ref bEq, i - aEq + 1);
                    }
                }
            }
            sb.Append(i < aLen ? a.Substring(i, aLen - i) : j < bLen ? b.Substring(j, bLen - j) : "");
            return sb.ToString();
        }

        private static void SmartFlush(ref StringBuilder sb, string a, string b, ref int aEq, ref int bEq, int v)
        {   // ************  TO DO  **************
            throw new NotImplementedException();
        }

        static string morganAndString2(string a, string b)
        {
            int i = 0, j = 0, aLen = a.Length, bLen = b.Length;
            StringBuilder sb = new StringBuilder();
            while (i < aLen && j < bLen)
            {
                if (a[i] != b[j])
                {
                    sb.Append(a[i] > b[j] ? b[j++] : a[i++]);
                }
                else
                {
                    int aEq = i, bEq = j, offs = -1;
                    char ch = a[i];
                    while (i < aLen && j < bLen && a[i] == b[j] && a[i] <= ch)
                    {
                        if (a[i] == ch)
                        {
                            if (i > aEq && a[i - 1] < ch)
                            {
                                offs = i - aEq;
                            }
                        }
                        else
                        {
                            if (offs > -1)
                            {
                                if (a[i] > a[i - offs])
                                {   // flush both for offs
                                    sb.Append(a.Substring(aEq, offs));
                                    sb.Append(a.Substring(aEq, offs));
                                    aEq += offs;
                                    bEq += offs;
                                    offs = -1;
                                }
                                else if (a[i] < a[i - offs])
                                {
                                    offs = -1;
                                }
                            }
                        }
                        i++; j++;
                    }
                    if (i == aLen && j == bLen
                    || i == aLen && ch < b[j]
                    || j == bLen && ch < a[i]
                    || ch < a[i] && ch < b[j])
                    {   // flush both
                        sb.Append(a.Substring(aEq, i - aEq));
                        sb.Append(a.Substring(aEq, i - aEq));
                    }
                    else if (i == aLen && ch >= b[j]
                        || j == bLen && ch >= a[i])
                    {
                        sb.Append(a.Substring(aEq, i - aEq));
                        if (i == aLen) i = aEq;  // flush b, reverse a
                        else j = bEq; // flush a, reverse b
                    }
                    else
                    {
                        sb.Append(a.Substring(aEq, i - aEq));
                        if (a[i] > b[j]) i = aEq;  // flush b, reverse a
                        else j = bEq; // flush a, reverse b
                    }
                }
            }
            sb.Append(i < aLen ? a.Substring(i, aLen - i) : j < bLen ? b.Substring(j, bLen - j) : "");
            return sb.ToString();
        }

        static string[] bigSorting(string[] unordered)
        {
            return unordered.Count() > 199000 ? unordered.OrderBy(s => BigInteger.Parse(s)).ToArray() : unordered.OrderBy(s => s.Length).ThenBy(s => s).ToArray();
        }

    }
}
