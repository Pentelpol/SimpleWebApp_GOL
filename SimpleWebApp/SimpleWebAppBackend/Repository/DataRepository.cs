namespace SimpleWebAppBackend.Repository
{
    public class DataRepository : IDataRepository
    {
        private readonly ILogger<DataRepository> _logger;

        public DataRepository(ILogger<DataRepository> logger)
        {
            _logger = logger;
        }

        public int[][]? RetriveNextGenerationFromInput(int[][] input, int col, int row)
        {
            try
            {
                if (input == null) { return null; }
                if (col == 0) { return null; }
                if (row == 0) { return null; }

                int[][] nextGen = MakeJaggedArray(col, row);

                for (int i = 0; i < col; i++)
                {
                    for (int j = 0; j < row; j++)
                    {
                        // Count live neighbors
                        int neighbors = CountNeighbors(i, j, col, row, input);
                        int state = input[i][j];

                        if (state == 0 && neighbors == 3)
                        {
                            nextGen[i][j] = 1;
                        }
                        else if (state == 1 && (neighbors < 2 || neighbors > 3))
                        {
                            nextGen[i][j] = 0;
                        }
                        else
                        {
                            nextGen[i][j] = state;
                        }
                    }
                }
                return nextGen;
            }
            catch (Exception ex)
            {
                _logger.LogError("Error from RetriveNextGenerationFromInput : Error: {error}", ex);
                return null;
            }
        }

        private static int CountNeighbors(int x, int y, int col, int row, int[][] data)
        {
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int column = (x + i + col) % col;
                    int rows = (y + j + row) % row;

                    sum += data[column][rows];
                }
            }
            sum -= data[x][y];
            return sum;
        }

        private static int[][] MakeJaggedArray(int col, int row)
        {
            int[][] jaggedArray = new int[col][];
            for (int i = 0; i < jaggedArray.Length; i++)
            {
                jaggedArray[i] = new int[row];
            }
            return jaggedArray;
        }
    }
}
