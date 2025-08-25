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
			arr = arr.Where(n => n > 1).ToArray();
 			maxIndex = Array.FindIndex(arr, x => x == curProd);
			leftProd = getArrProduct(0, maxIndex - 1, arr);
			rightProd = getArrProduct(maxIndex + 1, arr.Length - 1, arr);
			leftTmpIdx = 0;
			rightTmpIdx = arr.Length - 1;
            totalProd = arr.Aggregate(new BigInteger(1), (prod, n) => prod * n);
            // 2 cases: 
			len = arr.Length;

            return calcPartitions(arr);
        }
        BigInteger totalProd = new BigInteger(1);
        BigInteger curProd, leftProd, rightProd, leftTmpProd, rightTmpProd = new BigInteger(1);
        int maxIndex = 0;
        int leftTmpIdx = 0, rightTmpIdx = 0;
        int len = 0;
        private int calcPartitions(int[] arr)
        {
			int maxCount = 1; // at least 1 partition is possible - the whole array
			bool found = false;
            for (int i = maxIndex - 1, j = maxIndex + 1; !found && i >= -1 && j <= len; doNextStep(ref i, ref j, arr))
			{
				if (!checIfPartPossible(curProd, totalProd)) continue;

				int leftCnt = scoreSubArr(0, i, true, 0, arr);
				if (i >= 0 && leftCnt < 1) break;
				int rightCnt = scoreSubArr(j, len - 1, false, 0, arr);
				if (j < arr.Length && rightCnt < 1) continue;
				found = true;
                maxCount = Math.Max(maxCount, 1 + leftCnt + rightCnt);
			}
			return maxCount;
        }

        private void doNextStep(ref int i, ref int j, int[] arr)
        {
			if (i > -1 && (j >= len - 1 || arr[i] <= arr[j]))
			{
				curProd *= arr[i];
				leftProd /= arr[i];
                i--;
			}
			else if (j < len && (i < 0 || arr[i] >= arr[j]))
			{
				curProd *= arr[j];
				rightProd /= arr[j];
				j++;        }
			}

        private int scoreSubArr(int sIdx, int eIdx, bool incr, int cnt, int[] arr)
        {
			if (sIdx > eIdx || sIdx < 0 || eIdx > arr.Length -1) return cnt;
            BigInteger curTotal = incr ? leftProd : rightProd;
			if(!checIfPartPossible(curProd, curTotal))
				return 0;
			while ((incr && leftTmpIdx <= eIdx || rightTmpIdx >= sIdx) && (incr ? (leftTmpProd *= arr[leftTmpIdx++]) : (rightTmpProd *= arr[rightTmpIdx--])) < curProd)
				continue;
			if((incr ? leftTmpProd : rightTmpProd) != curProd) return cnt;
			int cntPlus = scoreSubArr((incr ? leftTmpIdx : sIdx), (incr ? eIdx : rightTmpIdx), incr, cnt+1, arr);
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
