using HackerRank.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    class HackenbushHelper
    {
        //Computes the nimber for node x as the smallest non-negative integer
        //not equal to the nimbers of any nodes in x's adjacency list.
        //This can be done non-recursively using DFS with a stack.
        //Once you have the nimbers, just take the cumulative XOR of all the initial positions, and you get the answer.

        // Get list of PARENT nodes/keys: those appearing only first in pairs
        private List<int> GetRoots(List<List<int>> paths, List<int> query)
        {
            IEnumerable<int> fList = paths.GroupBy(p => p.First()).Select(g => g.ElementAt(0).First());
            IEnumerable<int> lList = paths.GroupBy(p => p.Last()).Select(g => g.ElementAt(0).Last());
            return fList.Where(p => !lList.Any(n => p == n)).ToList();
        }

        // Create GNode tree for every parent: new GNode(src, key)
        private List<GNode<int>> InitHackenbush(List<List<int>> paths, List<int> roots)
        {
            return roots.Aggregate(new List<GNode<int>>(), (lst, r) => { lst.Add(new GNode<int>(paths, r)); return lst; }).ToList();
        }

        // For every root-parent get get nimber, then nim-sum of them...
        public string bendersPlay(List<GNode<int>> hBush)
        {
            return hBush.Aggregate(0, (xor, r) => xor^r.traverse(hbTrvs)) == 0 ? "Iroh" : "Bumi";
        }
        int hbTrvs(int a, int b) { return a ^ b; }

        // *******************  C++  ************************************
        //#include <bits/stdc++.h>

        //using namespace std;

        //const int Nmax = 1e5 + 2;

        //    vector<int> G[Nmax];
        //    int postorder[Nmax];
        //    int grundy[Nmax];
        //    int vis[Nmax];
        //    int N, M, P, Q, K;

        //    void DFS(int nod)
        //    {
        //        vis[nod] = 1;

        //        for (auto x: G[nod])
        //        {
        //            if (!vis[x])
        //            {
        //                DFS(x);
        //            }
        //        }

        //        postorder[++P] = nod;
        //    }

        //    int mex(int nod)
        //    {
        //        if (grundy[nod] != -1)
        //            return grundy[nod];

        //        unordered_set<int> Set;

        //        for (auto x: G[nod])
        //            Set.insert(mex(x));

        //        grundy[nod] = 0;

        //        while (Set.count(grundy[nod]))
        //            grundy[nod]++;

        //        return grundy[nod];
        //    }

        //    int main()
        //    {
        //        ///ifstream cin("data.in");

        //        scanf("%d %d", &N, &M);

        //        for (int i = 1, a, b; i <= M; ++i) // Create graph
        //        {
        //            scanf("%d %d", &a, &b);
        //            G[a].push_back(b);
        //        }

        //        memset(grundy, -1, sizeof(grundy));

        //        for (int i = 1; i <= N; ++i)
        //            if (!vis[i])
        //                DFS(i);

        //        for (int i = P; i >= 1; i--)
        //            grundy[postorder[i]] = mex(postorder[i]);

        //        scanf("%d", &Q);

        //        while (Q--)
        //        {
        //            scanf("%d", &K);

        //            int xorsum = 0;

        //            for (int i = 1, nod; i <= K; ++i)
        //            {
        //                scanf("%d", &nod);
        //                xorsum ^= grundy[nod];
        //            }

        //            if (xorsum)
        //                cout << "Bumi\n";
        //            else
        //                cout << "Iroh\n";
        //        }

        //        return 0;
        //    }
    }
}
