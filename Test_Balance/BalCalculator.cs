using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Test_Balance
{
	internal class BalCalculator
	{
		private int bal = 0;
		public int getBalance() => bal;
		public BalCalculator() { }
		/// <summary>
		/// Calculates the MAX absolute difference between adjacent sorted int array members.
		/// Every RADIX iteration can bring the final result, so, the workflow keeps an eye on that.
		/// </summary>
		/// <remarks>This method uses radix/backet sort principles.</remarks>
		/// <param name="arr">An array of integers from 0 to 2^32-1.</param>
		/// <returns>The Max Abs difference of adjacent members of the array after sorting it.</returns>
		public int CalculateBalance(int[] arr)
		{
			bal = 0;
			if ((arr?.Length ?? 0) < 2) return 0;
			int curBase = 1000000000;
			List<List<int>> buckets = new List<List<int>> { arr != null ? arr.ToList() : new List<int>() };
			while (curBase > 0)
			{
				buckets = getNextBucketsAndBal(curBase, buckets);
				if (bal > 0) break;
				curBase /= 10;
			}
			return bal;
		}

		private List<List<int>> getNextBucketsAndBal(int curBase, List<List<int>> buckets) =>
			buckets.Aggregate(new List<List<int>>(), (lst, el) => getLowerBucketSetBal(curBase, lst, el));

		private List<List<int>> getLowerBucketSetBal(int curBase, List<List<int>> lst, List<int> el)
		{
			var seed = new List<List<int>>[10].Select(el=>new List<int>()).ToList();
			var newBackets = el.Aggregate(seed, (lst, n) => { var idx = (n / curBase) % 10; (lst[idx]??(lst[idx] = new List<int>())).Add(n); return lst; }).Where(el=>(el?.Count??0) > 0);
			checkCurBalance(curBase, newBackets);
			lst.AddRange(newBackets);

			return lst;
		}

		private void checkCurBalance(int curBase, IEnumerable<List<int>> newBackets)
		{
			bool found = false;
			int curNum = -1;
			foreach(var ls in newBackets)
			{
				if (ls.Count == 1)
				{
					if(found) bal = Math.Max(bal, Math.Abs(ls[0] - curNum));
					else found = true;
					curNum = ls[0];
				}
				else
					found = false;
			}
		}

		private void test001()
		{
			var ll = new List<List<int>>[10];
			var l = ll[0];
		}
	}
}
