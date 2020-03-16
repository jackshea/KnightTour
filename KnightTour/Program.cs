using System;

namespace KnightTour
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new KnightTourSolver();

            //for (int k = 5; k <= 71; k++)
            {
                int n = 32;
                solver.Solve(n);
                Console.Clear();
                Console.WriteLine($"n={n}");
                solver.Print();
                Console.WriteLine();
            }
            //new Export(5).Print();

            Console.ReadLine();
        }
    }
}
