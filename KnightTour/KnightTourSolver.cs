#define ShowStep
using System;
using System.Collections.Generic;

namespace KnightTour
{
    public class KnightTourSolver
    {
        private int[] dirX = { -2, -2, -1, -1, 1, 1, 2, 2 };
        private int[] dirY = { 1, -1, 2, -2, 2, -2, 1, -1 };
        private int[,] board;
        private int n;

        public int[,] Solve(int n)
        {
            board = new int[n, n];
            this.n = n;
            DFS(0, 0, 1);
            return board;
        }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{board[i, j],4} ");
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

            if (board[x, y] != 0)
            {
                return false;
            }

            board[x, y] = step;
#if ShowStep
            Console.SetCursorPosition(0, 0);
            Print();
            Console.WriteLine($"({x},{y})={step}\t\t\t");
#endif

            if (step >= board.Length)
            {
                return true;
            }

            List<ExportInfo> exportInfos = new List<ExportInfo>();

            for (int k = 0; k < dirX.Length; k++)
            {
                var newX = x + dirX[k];
                var newY = y + dirY[k];

                if (newX < 0 || newX >= n || newY < 0 || newY >= n)
                {
                    continue;
                }

                if (board[newX, newY] != 0)
                {
                    continue;
                }

                exportInfos.Add(new ExportInfo
                {
                    ExportCount = Export(newX, newY),
                    ExportIndex = k
                });
            }

            exportInfos.Sort((a, b) => a.ExportCount.CompareTo(b.ExportCount));

            foreach (var exportInfo in exportInfos)
            {
                if (exportInfo.ExportCount == 0 && step < board.Length - 1)// 倒数第二步时不用判断，因为必然存为0出口的点，即最后一个点。
                {
                    board[x, y] = 0;
#if ShowStep
                    Console.SetCursorPosition(0, 0);
                    Print();
                    Console.WriteLine($"存在黑洞 ({x},{y})=0\t\t\t");
#endif
                    return false;
                }

                if (DFS(x + dirX[exportInfo.ExportIndex], y + dirY[exportInfo.ExportIndex], step + 1))
                {
                    return true;
                }
            }

            board[x, y] = 0;
#if ShowStep
            Console.SetCursorPosition(0, 0);
            Print();
            Console.WriteLine($"死胡同 ({x},{y})=0\t\t\t");
#endif
            return false;
        }

        /// 返回值出口数
        private int Export(int x, int y)
        {
            int ans = 0;
            for (int k = 0; k < dirX.Length; k++)
            {
                var newX = x + dirX[k];
                var newY = y + dirY[k];

                if (newX < 0 || newX >= n || newY < 0 || newY >= n)
                {
                    continue;
                }

                if (board[newX, newY] == 0)
                {
                    ans++;
                }
            }

            return ans;
        }
    }

    public struct ExportInfo
    {
        public int ExportCount;
        public int ExportIndex;
    }
}