namespace KnightTour
{
    public class IslandSolver
    {
        private int n;
        private bool[,] board;

        private int[] dirX = { -2, -2, -1, -1, 1, 1, 2, 2 };
        private int[] dirY = { 1, -1, 2, -2, 2, -2, 1, -1 };

        public IslandSolver()
        {
        }

        public IslandSolver(bool[,] board, int n)
        {
            this.n = n;
            this.board = board;
        }

        public void SetBoard(int[,] board, int n)
        {
            if (this.n != n)
            {
                this.n = n;
                this.board = new bool[n, n];
            }

            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    this.board[i, j] = board[i, j] == 0;
                }
            }
        }

        /// 检查是否存在不可达的孤岛
        public bool ExistIsland(int x, int y)
        {
            DFS(x, y);
            foreach (var b in board)
            {
                if (b)
                {
                    return true;
                }
            }

            return false;
        }

        private void DFS(int x, int y)
        {
            if (x < 0 || x >= n || y < 0 || y >= n)
            {
                return;
            }

            if (!board[x, y])
            {
                return;
            }

            board[x, y] = false;

            for (int i = 0; i < dirX.Length; i++)
            {
                int newX = x + dirX[i];
                int newY = y + dirY[i];
                DFS(newX, newY);
            }
        }
    }
}