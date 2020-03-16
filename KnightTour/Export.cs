using System;
using System.Collections.Generic;

namespace KnightTour
{
    /// 棋盘每个格子的出口数
    public class Export
    {
        private int[,] exportBoard;
        private int n;
        private int[] dx = { -2, -2, -1, -1, 1, 1, 2, 2 };
        private int[] dy = { 1, -1, 2, -2, 2, -2, 1, -1 };
        public Export(int n)
        {
            exportBoard = new int[n, n];
            this.n = n;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    int export = 8;
                    if (i == 0 || i == n - 1)
                    {
                        export -= 4;
                    }
                    if (i == 1 || i == n - 2)
                    {
                        export -= 2;
                    }
                    if (j == 0 || j == n - 1)
                    {
                        export -= 4;
                        if (i == 0 || i == n - 1)
                        {
                            export = 2;
                        }
                        if (i == 1 || i == n - 2)
                        {
                            export = 3;
                        }
                    }
                    if (j == 1 || j == n - 2)
                    {
                        export -= 2;
                        if (i == 0 || i == n - 1)
                        {
                            export = 3;
                        }
                        if (i == 1 || i == n - 2)
                        {
                            export = 4;
                        }
                    }

                    exportBoard[i, j] = export;
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    Console.Write($"{exportBoard[i, j],4} ");
                }
                Console.WriteLine();
            }
        }

        public void Put(int x, int y)
        {
            exportBoard[x, y] = -1;
            for (int i = 0; i < dx.Length; i++)
            {
                int xx = x + dx[i];
                int yy = y + dy[i];
                if (xx >= 0 && xx < n && yy >= 0 && yy < n && exportBoard[xx, yy] > 0)
                {
                    exportBoard[xx, yy]--;
                }
            }
        }

        public void Revert(int[,] chessBoard, int x, int y)
        {
            exportBoard[x, y] = 0;
            for (int i = 0; i < dx.Length; i++)
            {
                int xx = x + dx[i];
                int yy = y + dy[i];
                if (xx < 0 || xx >= n || yy < 0 || yy >= n)
                    continue;

                if (chessBoard[xx, yy] != 0)
                {
                    continue;
                }

                exportBoard[x, y]++;
                exportBoard[xx, yy]++;
            }
        }

        /// 验证是否能遍历
        public bool Valid(int x, int y)
        {
            int count = 0;

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    //if (i == x && j == y)
                    //{
                    //    continue;
                    //}

                    var export = exportBoard[i, j];
                    if (export == 0)
                    {
                        return false;
                    }

                    if (export == 1)
                    {
                        count++;
                    }

                    if (count > 2)
                    {
                        return false;
                    }
                }
            }

            return true;
        }

        public List<ExportInfo> GetExportInfos(int x, int y, bool includeZero = false)
        {
            List<ExportInfo> exportInfos = new List<ExportInfo>();
            for (int i = 0; i < dx.Length; i++)
            {
                int xx = x + dx[i];
                int yy = y + dy[i];
                if (xx >= 0 && xx < n && yy >= 0 && yy < n)
                {
                    if (!includeZero && exportBoard[xx, yy] <= 0)
                    {
                        continue;
                    }

                    int m = n / 2;
                    exportInfos.Add(new ExportInfo
                    {
                        ExportCount = exportBoard[xx, yy],
                        ExportIndex = i,
                        Distance = (xx - m) * (xx - m) + (yy - m) * (yy - m)
                    });

                }
            }

            exportInfos.Sort();
            return exportInfos;
        }
    }


    public struct ExportInfo : IComparable<ExportInfo>
    {
        public int ExportCount;
        public int ExportIndex;
        public int Distance;

        public int CompareTo(ExportInfo other)
        {
            if (ExportCount != other.ExportCount)
            {
                return ExportCount.CompareTo(other.ExportCount);
            }

            return -Distance.CompareTo(other.Distance);
        }
    }
}