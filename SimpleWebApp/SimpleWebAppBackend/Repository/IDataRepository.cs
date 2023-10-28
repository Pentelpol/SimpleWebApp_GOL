namespace SimpleWebAppBackend.Repository
{
    public interface IDataRepository
    {
        int[][]? RetriveNextGenerationFromInput(int[][] input, int col, int row);
    }
}
