using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
		private int curTargetProd = 1;
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
					if(n > 1) curTargetProd *= n;
					lst.Add(n);
					if(curTargetProd <= targetProd) 
						maxLen = checkMax(maxLen, curTargetProd, targetProd, lst);
					else
					{
						int idx = 0;
						while(idx < lst.Count && curTargetProd > targetProd)
							curTargetProd /= lst[idx++];
                        lst = lst.Skip(idx).ToList();
						maxLen = checkMax(maxLen, curTargetProd, targetProd, lst);
                    }
				}
				else
				{
					adding = false;
					lst = new List<int>();
					curTargetProd = 1;
				}
			}
			else
			{
				if(adding = isDivisor(n))
				{
                    lst.Add(n);
                    curTargetProd = n;
                    maxLen = checkMax(maxLen, curTargetProd, targetProd, lst);

                }
            }
            return maxLen;
		}

		private bool isDivisor(int n) => n != 0 && (n==1 || targetProd % n == 0);
		private int checkMax(int maxLen, int curTargetProd, int targetProd, List<int> lst) => curTargetProd == targetProd ? Math.Max(maxLen, lst.Count) : maxLen;

		public int maxMultiPartitionWithEqualProd(int[] arr)
		{
			if (arr.Contains(0)) return arr.Where(n=>n == 0).Count();
            totalProd = arr.Aggregate(new BigInteger(1), (prod, n) => prod * n);
			if (totalProd == 1) return arr.Length;	
            // 2 cases: 
            curProd = arr.Max();
			maxIndex = Array.FindIndex(arr, x => x == curProd);
			len = arr.Length;

            return calcPartitions(arr);
        }
		private List<int> idxArr = new List<int> { 0 };
        BigInteger totalProd = new BigInteger(1);
        BigInteger curProd = new BigInteger(1);
        int maxIndex = 0;
        int len = 0;
        private int calcPartitions(int[] arr)
        {
			int maxCount = 1;
			int pcbCount = 1;
            for (int i = maxIndex - 1; maxCount < 2 && i >= -1; i--)
			{
				if (i >= 0 && arr[i] == 1) continue;
                for (int j = maxIndex + 1; maxCount < 2 && j <= len; j++, curProd = getArrProduct(i+1, j-1, arr))
				{
					if (j < len && arr[j] == 1 || !checIfPartPossible(curProd, totalProd)) continue;

					int leftCnt = scoreSubArr(0, i, 0, arr);
					if (i >= 0 && leftCnt < 1) break;
					int rightCnt = scoreSubArr(j, len - 1, 0, arr);
					if (j < arr.Length && rightCnt < 1) continue;
                    maxCount = Math.Max(maxCount, 1 + leftCnt + rightCnt);
				}
			}
			return maxCount;
        }

        private int scoreSubArr(int sIdx, int eIdx, int cnt, int[] arr)
        {
			if (sIdx > eIdx || sIdx < 0 || eIdx > arr.Length -1) return cnt;
            BigInteger curTotal = getArrProduct(sIdx, eIdx, arr);
			if(!checIfPartPossible(curProd, curTotal))
				return 0;
			BigInteger tmpProd = 1;
			while (sIdx <= eIdx && (tmpProd *= arr[sIdx++]) < curProd)
				continue;
			if(tmpProd != curProd) return cnt;
			int cntPlus = scoreSubArr(sIdx, eIdx, cnt+1, arr);
			return cntPlus;
        }

        private BigInteger getArrProduct(int sIdx, int eIdx, int[] arr)
        {
            for(BigInteger curProd = 1; sIdx <= eIdx; sIdx++)
				curProd *= arr[sIdx];
			return curProd;
        }

        private bool checIfPartPossible(BigInteger curProd, BigInteger curTotal)
		{
			BigInteger rmd = curTotal;
			while ((rmd /= curProd) % curProd == 0)
				continue;
			return rmd == 1;
        }
    }
}
