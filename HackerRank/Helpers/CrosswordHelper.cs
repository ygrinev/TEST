using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HackerRank.Helpers
{
    public class CrosswordHelper
    {
        class crsWord
        {
            public int x, y; //, len;
            //public bool dir;
        }
        static string[] crosswordPuzzle(string[] crossword, string words) // fill out crswrd placeholders with given words
        {   /// organise words in a dict
            Dictionary<int, List<string>> store = words.Split(';').Aggregate(new Dictionary<int, List<string>>(), (dct, cur) =>
            {
                int len = cur.Length;
                if (dct.ContainsKey(len))
                {
                    dct[len].Add(cur);
                }
                else
                {
                    dct[len] = new List<string> { cur };
                }
                return dct;
            });
            // find first wordPlcholder coords, len and direction
            crsWord r = new crsWord();
            bool found = false;
            while (!found && r.x < crossword.Length)
            {
                while (!found && r.y < crossword.Length)
                {
                    if (crossword[r.y][r.x] != '-')
                    {
                        r.y++;
                    }
                    else found = true;
                }
                r.x++;
            }
            // Create list of trees for placeholders
            bool[,] used = crossword.Aggregate(new bool[crossword.Length, crossword.Length], (usd, cur) =>
            {

                return usd;
            });

            // for every tree consequently/recursively solve words fit


            return new string[0];
        }
    }
}
