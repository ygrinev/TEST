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
		public int getMaxProdLen01(int[] arr, int prod) // optimized !!
		{
			if ((arr?.Length ?? 0) == 0) return 0;
			if (prod == 0) return arr.Contains(0) ? arr.Length : 0;
			var cProd = 1;
			var len = 0;
			arr?.Aggregate(new int[2]{0,0}, (ii, n) =>
			{
				if(n == 0 || prod%n > 0)
				{
					cProd = 1;
					ii[0] = ii[1] + 1;
                }
				else
				{
					cProd *= n;
					while (cProd > prod)
						cProd /= arr[ii[0]++];
					if(cProd == prod && len < ii[1] - ii[0] + 1)
						len = ii[1] - ii[0] + 1;
                }
                ii[1]++;
				return ii;
			});
			return len;
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

		/// <summary>
		/// Calculates the maximum number of partitions of the input array where each partition has an equal product.
		/// </summary>
		/// <remarks>If the array contains zeros, the method returns the count of zeros in the array, as partitions
		/// with zero product are trivial. If max == min, the method returns the length of the array,
		/// as each element can form its own partition.</remarks>
		/// <param name="arr">An array of integers to be partitioned. The array must not contain negative values.</param>
		/// <returns>The maximum number of partitions where the product of elements in each partition is equal.</returns>
		public int maxMultiPartitionWithEqualProd(int[] arr)
		{
			if (arr.Contains(0)) return arr.Where(n=>n == 0).Count();
            curProd = arr.Max();
			BigInteger curMin = arr.Min();
			if (curProd == curMin) return arr.Length;
			if (arr.Contains(1)) arr = arr.Where(n => n > 1).ToArray();
			len = arr.Length;
 			maxIndex = Array.FindIndex(arr, x => x == curProd);
			leftProd = getArrProduct(0, maxIndex - 1, arr);
			rightProd = getArrProduct(maxIndex + 1, len - 1, arr);
			leftTmpIdx = 0;
			rightTmpIdx = len - 1;
			curMinLen = 0;
            totalProd = arr.Aggregate(new BigInteger(1), (prod, n) => prod * n);
            // 2 cases: 

            return calcPartitions(arr);
        }
        BigInteger totalProd = new BigInteger(1);
        BigInteger minCururProd = new BigInteger(1);
        BigInteger curProd, leftProd, rightProd, leftTmpProd = new BigInteger(1), rightTmpProd = new BigInteger(1);
        int maxIndex = 0;
        int leftTmpIdx = 0, rightTmpIdx = 0;
        int len = 0;
        int curMinLen = 1; // verified interval len (interval inclides maxIdx)
        private int calcPartitions(int[] arr)
        {
			int maxCount = 1; // at least 1 partition is possible - the whole array
			bool found = false;
            while (!found && curMinLen < len && BigInteger.Pow(minCururProd, 2) <= totalProd)
			{
				BigInteger minCururProd = new BigInteger(1);
				curMinLen++;
                for (int i = Math.Max(maxIndex - curMinLen, 0), j = i + curMinLen + 1; j <= Math.Min(len, maxIndex + curMinLen); i++,j++)
				{
					curProd = getArrProduct(i+1, i + curMinLen, arr);
					if(minCururProd == 1 || curProd < minCururProd)
						minCururProd = curProd;

                    leftProd = getArrProduct(0, i, arr);
					rightProd = getArrProduct(j, len-1, arr);
					if (!checIfPartPossible(curProd, totalProd)) continue;

					int leftCnt = scoreSubArr(0, i, true, 0, arr);
					if (i >= 0 && leftCnt < 1) continue;
					int rightCnt = scoreSubArr(j, len - 1, false, 0, arr);
					if (j < arr.Length && rightCnt < 1) continue;
					found = true;
					maxCount = Math.Max(maxCount, 1 + leftCnt + rightCnt);
				}
			}
			return maxCount;
        }
        private int scoreSubArr(int sIdx, int eIdx, bool incr, int cnt, int[] arr)
        {
			if (sIdx > eIdx || sIdx < 0 || eIdx > arr.Length -1) return cnt;
            BigInteger curTotal = incr ? leftProd : rightProd;
			if(!checIfPartPossible(curProd, curTotal))
				return 0;
			while ((incr && leftTmpIdx <= eIdx || !incr && rightTmpIdx >= sIdx) && (incr ? (leftTmpProd *= arr[leftTmpIdx++]) : (rightTmpProd *= arr[rightTmpIdx--])) < curProd)
				continue;
			if((incr ? leftTmpProd : rightTmpProd) != curProd) return 0;
			if (incr)
			{
				leftProd /= curProd;
				leftTmpProd = 1;
			}
			else
			{
				rightProd /= curProd;
				rightTmpProd = 1;
			}
            int cntPlus = scoreSubArr((incr ? leftTmpIdx : sIdx), (incr ? eIdx : rightTmpIdx), incr, cnt+1, arr);
			return cntPlus;
        }

        private BigInteger getArrProduct(int sIdx, int eIdx, int[] arr)
        {
			BigInteger prod = 1;
            while(sIdx <= eIdx)
				prod *= arr[sIdx++];
			return prod;
        }

        private bool checIfPartPossible(BigInteger curProd, BigInteger curTotal)
		{
			if (BigInteger.Pow(curProd, 2) > curTotal) return false;
			BigInteger rmd = curTotal;
			while ((rmd /= curProd) % curProd == 0)
				continue;
			return rmd == 1;
        }
    }
}
