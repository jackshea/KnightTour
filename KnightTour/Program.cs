using System;

namespace KnightTour
{
    class Program
    {
        static void Main(string[] args)
        {
            var solver = new KnightTourSolver();

            //for (int k = 44; k < 100; k++)
            {
                int n = 32;
                solver.Solve(n);

                Console.Clear();
                Console.WriteLine($"n={n}");
                solver.Print();
                Console.WriteLine();
            }

            Console.ReadLine();
        }
    }
}
