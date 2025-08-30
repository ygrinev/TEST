// See https://aka.ms/new-console-template for more information

using Test_Balance;
do
{
    //Console.WriteLine($"Max partitions with equal product = {new TestSOTI().maxMultiPartitionWithEqualProd([3,2,3,1,3,6,9,3,2,9,6,1,9])}");
    //Console.WriteLine($"Max partitions with equal product = {new TestSOTI().maxMultiPartitionWithEqualProd([1, 2, 1, 1, 1, 8, 1, 1, 2, 1, 1, 8, 1, 1, 2, 2, 1, 1, 2, 7])}");
    //Console.WriteLine($"Max partitions with equal product = {new TestSOTI().maxMultiPartitionWithEqualProd([1, 2, 1, 1, 1, 8, 1, 1, 1, 1, 1, 8, 2, 1, 2, 4, 1, 1, 2])}");
    Console.WriteLine($"Max subset length = {new TestSOTI().getMaxProdLen01([1, 2, 1, 1, 1, 8, 1, 1, 2, 1, 1, 8, 1, 1, 2, 2, 1, 1, 2, 7], 8)}"); // 7
    Console.WriteLine($"Max subset length = {new TestSOTI().getMaxProdLen01([1, 2, 1, 1, 1, 8, 1, 1, 1, 1, 1, 8, 1, 1, 2, 2, 1, 1, 2, 7], 8)}"); // 9
    Console.WriteLine($"Max subset length = {new TestSOTI().getMaxProdLen01([1, 2, 1, 1, 1, 8, 1, 1, 2, 1, 1, 8, 1, 1, 0, 2, 1, 1, 2, 7], 8)}"); // 6
    Console.WriteLine($"Max subset length = {new TestSOTI().getMaxProdLen01([1, 2, 1, 1, 1, 8, 1, 1, 1, 1, 1, 8, 1, 1, 2, 2, 1, 1, 2, 7], 7)}"); // 1
    //Console.WriteLine($"Balance = {new BalCalculator().CalculateBalance([1,2,8,7])}");
} while (Console.ReadKey().Key != ConsoleKey.Escape);
