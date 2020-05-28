using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorityTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Program processor = new Program();
            List<string> list = processor.GetFileContent("TripData.txt");
            Dictionary<int, Dictionary<int, double>> dict = processor.GetPaidTotals(list);
            List<string> expList = processor.ExportBalances(dict);
            File.WriteAllLines("TripData.txt", expList);
            foreach(var s in expList)
            {
                Console.WriteLine(s);
            }
            Console.ReadLine();
        }

        public List<string> GetFileContent(string file)
        {
            List<string> input = new List<string>();
            System.IO.StreamReader f = null;
            try
            {
                using (f = new System.IO.StreamReader(file)) //"TripData.txt"
                {
                    string l = null;
                    while ((l = f.ReadLine()) != null)
                    {
                        input.Add(l);
                    }
                }
            }
            catch(IOException)
            {
                Console.WriteLine($"Error reading file '{file}'");
            }
            catch(Exception)
            {
                throw new FileLoadException($"Error loading file '{file}'");
            }
            finally
            {
                if (f != null)
                    f.Close();
            }
            return input;
        }

        public List<string> ExportBalances(Dictionary<int, Dictionary<int, double>> dict)
        {
            List<string> list = new List<string>();
            foreach(int tripIdx in dict.Keys.OrderBy(k=>k))
            {
                if(dict[tripIdx].Keys.Count == 0)
                {
                    throw new DataMisalignedException($"Empty trip data fot the trip # {tripIdx +1}");
                }
                double avg = AvarageTotalPerTrip(dict, tripIdx);
                foreach(int personIdx in dict[tripIdx].Keys.OrderBy(k=>k))
                {
                    double bal = avg - dict[tripIdx][personIdx];
                    bool neg = bal < 0;
                    list.Add($"{(neg?"(":"")}{Math.Abs(bal).ToString("C", CultureInfo.GetCultureInfo("en-us"))}{(neg?")":"")}");
                }
                list.Add("\r\n");
            }
            return list;
        }

        public Dictionary<int, Dictionary<int, double>> GetPaidTotals(List<string> list)
        {
            Dictionary<int, Dictionary<int, double>> dictPaidTotals = new Dictionary<int, Dictionary<int, double>>();
            int intCount = 0, tripIdx = -1, persNum = 0, pmtNum = 0, persIdx = 0, line = 0; ;

            foreach (string s in list)
            {
                line++;
                string sNum = (s ?? "").Trim();
                if(string.IsNullOrEmpty(sNum) || sNum == "0")
                {
                    continue;
                }
                bool isMoney = sNum.IndexOf('.') > -1;
                if(!isMoney)
                {
                    int num = int.TryParse(sNum, out num) ? num : throw new ArithmeticException($"Invalid int data at line # {line} - '{sNum}'");
                    intCount++;
                    if(intCount > 2)
                    {
                        throw new FormatException("Too many cosequent integer lines, max - 2");
                    }
                    if(intCount > 1) // new trip, so pmtNum is actually persNum
                    {
                        dictPaidTotals[++tripIdx] = new Dictionary<int, double>();
                        persIdx = 0;
                        persNum = pmtNum;
                        pmtNum = num;
                    }
                    else // first int value after money - assume pmtNum for the next person
                    {
                        if(pmtNum != 0)
                        {
                            throw new ArgumentOutOfRangeException($"Invalid number of payments provided ending at line # {line-1}");
                        }
                        if(persNum <= 0 && tripIdx > -1)
                        {
                            throw new ArgumentOutOfRangeException($"Invalid number of persons provided in the trip containing line # {line}");
                        }
                        pmtNum = num;
                        persIdx++;
                        persNum--;
                    }
                }
                else
                {
                    double dNum = double.TryParse(sNum, out dNum) ? dNum : throw new ArithmeticException($"Invalid double data at line # {line} - '{sNum}'");
                    pmtNum--;

                    dictPaidTotals[tripIdx][persIdx] = (dictPaidTotals[tripIdx].ContainsKey(persIdx) ? dictPaidTotals[tripIdx][persIdx] : 0) + dNum;
                    intCount = 0;
                }
            }
            return dictPaidTotals;
        }

        double AvarageTotalPerTrip(Dictionary<int, Dictionary<int, double>> dict, int tripIdx)
        {
            return dict.ContainsKey(tripIdx) && dict[tripIdx].Keys.Count > 0 
                    ? dict[tripIdx].Keys.Select(k => dict[tripIdx][k]).Average()
                    : throw new AggregateException();
        }
    }
}
