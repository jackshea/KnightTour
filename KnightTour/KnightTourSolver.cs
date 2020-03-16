#define ShowStep
using System;
using System.Collections.Generic;

namespace KnightTour
{
    public class KnightTourSolver
    {
        private int[] dirX = { -2, -2, -1, -1, 1, 1, 2, 2 };
        private int[] dirY = { 1, -1, 2, -2, 2, -2, 1, -1 };
        private int[,] chessBoard;
        private int n;
        private IslandSolver islandSolver;
        private Export export;

        public int[,] Solve(int n)
        {
            chessBoard = new int[n, n];
            this.n = n;
            islandSolver = new IslandSolver();
            export = new Export(n);
            DFS(0, 0, 1);
            return chessBoard;
        }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{chessBoard[i, j],4} ");
                }
                Console.WriteLine();
            }
        }

        private bool DFS(int x, int y, int step)
        {
            if (x < 0 || x >= n || y < 0 || y >= n)
            {
                return false;
            }

            if (chessBoard[x, y] != 0)
            {
                return false;
            }

            if (!export.Valid(x, y) && step < chessBoard.Length - 1)
            {
#if ShowStep
                Console.SetCursorPosition(0, 0);
                Print();
                Console.WriteLine($"无法遍历 ({x},{y})=0\t\t\t");
#endif
                return false;
            }

            islandSolver.SetBoard(chessBoard, n);
            if (islandSolver.ExistIsland(x, y))
            {
#if ShowStep
                Console.SetCursorPosition(0, 0);
                Print();
                Console.WriteLine($"存在孤岛 ({x},{y})=0\t\t\t");
#endif
                return false;
            }

            chessBoard[x, y] = step;
            export.Put(x, y);
#if ShowStep
            Console.SetCursorPosition(0, 0);
            Print();
            Console.WriteLine($"({x},{y})={step}\t\t\t");
#endif

            if (step >= chessBoard.Length)
            {
                return true;
            }

            var exportInfos = export.GetExportInfos(x, y, step >= chessBoard.Length - 1);
            foreach (var exportInfo in exportInfos)
            {
                if (DFS(x + dirX[exportInfo.ExportIndex], y + dirY[exportInfo.ExportIndex], step + 1))
                {
                    return true;
                }
            }

            chessBoard[x, y] = 0;
            export.Revert(chessBoard, x, y);
#if ShowStep
            Console.SetCursorPosition(0, 0);
            Print();
            Console.WriteLine($"死胡同 ({x},{y})=0\t\t\t");
#endif
            return false;
        }
    }
}