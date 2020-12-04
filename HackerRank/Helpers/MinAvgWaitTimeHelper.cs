using System;

namespace HackerRank
{
    class Cust : IComparable
	{
		public long arrTime;
		public long cookTime;

        public virtual int CompareTo(object obj)
        {
			var o = (Cust)obj;
            return arrTime < o.arrTime ? -1 : 1;
        }
    };
	class Cust1 : Cust
	{
		public Cust1(Cust c) { arrTime = c.arrTime; cookTime = c.cookTime; }
        public override int CompareTo(object obj)
        {
			var o = (Cust1)obj;
            return cookTime < o.cookTime ? -1 : 1;
        }
    };
	//*****************************************************************************
	//*****************************************************************************
	//*****************************************************************************
	class MinAvgWaitTimeHelper
    {
		public static long MinWaitTime(string[] queries)
		{
			long n = queries.Length;
			long sum = 0;
			var arrvList = new MinHeap<Cust>(); // ordered in arrTime
			var waitList = new MinHeap<Cust1>(); // ordered on cookTime

			foreach(var q in queries)
            {
				string[] s = q.Split(' ');
				arrvList.Add(new Cust { arrTime = long.Parse(s[0]), cookTime = long.Parse(s[1]) });
            }
			var c0 = arrvList.PopMin();
			waitList.Add(new Cust1(c0));
			long time = c0.arrTime;
			while (waitList.GetSize() > 0)
			{
				var curr = waitList.PopMin();
				time = time + curr.cookTime;

				while (arrvList.GetSize() > 0 && arrvList.GetMin().arrTime <= time)
				{
					waitList.Add(new Cust1(arrvList.PopMin()));
				}
				sum += (time - curr.arrTime);
				if (waitList.GetSize() < 1 && arrvList.GetSize() > 0)
				{
					waitList.Add(new Cust1(arrvList.PopMin()));
				}
			}
			return sum / n;
		}
	}
}
