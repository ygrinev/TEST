using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Model
{
    public class INode
    {
        private static string _str;
        public string s { get {return _str;} }

        public int lIdx = -1, rIdx = -1;

        public INode(string str, int k = -1) { _str = str; Init(k); }
        public INode() {}

        private void Init(int k)
        {
            throw new NotImplementedException();
        }
    }
}
