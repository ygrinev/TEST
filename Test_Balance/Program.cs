// See https://aka.ms/new-console-template for more information

using Test_Balance;
do
{
    Console.WriteLine($"Max subset length = {new TestSOTI().getMaxProdLen([1, 2, 8, 1, 8, 1, 1, 2, 2, 1, 1, 2, 7], 8)}");
    //Console.WriteLine($"Balance = {new BalCalculator().CalculateBalance([1,2,8,7])}");
} while (Console.ReadKey().Key != ConsoleKey.Escape);
