using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Balance
{
	internal class TestSOTI
	{
		/// <summary>
		/// Calculates the maximum length of a contiguous subarray where where product of its elements equal prod.
		/// </summary>
		/// <param name="arr">An array of integers to analyze.</param>
		/// <param name="prod">Target product value.</param>
		/// <returns>The length of the longest contiguous subarray where where product of its elements equal prod.</returns>
		private List<int> lst = new List<int>();
		private int targetProd = 1;
		private int curProd = 1;
		private bool adding = false;
		public int getMaxProdLen(int[] arr, int prod)
		{
			targetProd = prod;
			return arr?.Aggregate(0, (len, n) => getNextLen(len, n))??0;
		}

		private int getNextLen(int maxLen, int n)
		{
			if (adding)
			{
				if (isDivisor(n))
				{
					if(n > 1) curProd *= n;
					lst.Add(n);
					if(curProd <= targetProd) 
						maxLen = checkMax(maxLen, curProd, targetProd, lst);
					else
					{
						int idx = 0;
						while(idx < lst.Count && curProd > targetProd)
							curProd /= lst[idx++];
                        lst = lst.Skip(idx).ToList();
						maxLen = checkMax(maxLen, curProd, targetProd, lst);
                    }
				}
				else
				{
					adding = false;
					lst = new List<int>();
					curProd = 1;
				}
			}
			else
			{
				if(adding = isDivisor(n))
				{
                    lst.Add(n);
                    curProd = n;
                    maxLen = checkMax(maxLen, curProd, targetProd, lst);

                }
            }
            return maxLen;
		}

		private bool isDivisor(int n) => n != 0 && (n==1 || targetProd % n == 0);
		private int checkMax(int maxLen, int curProd, int targetProd, List<int> lst) => curProd == targetProd ? Math.Max(maxLen, lst.Count) : maxLen;
	}
}
