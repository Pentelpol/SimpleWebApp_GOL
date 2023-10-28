using System.Collections;

namespace SimpleWebAppBackend.Model
{
    public class DataModel
    {
        public int[,]? Data { get; set; }
        public int Colunms { get; set; }
        public int Rows { get; set; }

        public  DataModel(int col, int row) {
            Colunms = col;
            Rows = row;
            Data = new int[Colunms, Rows];
        }

        private int CountNeighbors(int x, int y)
        {
            int sum = 0;
            for (int i = -1; i < 2; i++)
            {
                for (int j = -1; j < 2; j++)
                {
                    int col = (x + i + Colunms) % Colunms;
                    int row = (y + j + Rows) % Rows;

                    sum += Data[col, row];
                }
            }
            sum -= Data[x, y];
            return sum;
        }

        public bool DeleteData()
        {
            try
            {
                Array.Clear(Data, 0, Data.Length); 
                Data = null;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool CreateData(int col, int row)
        {
            try
            {
                Data = new int[col, row];
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
        public void SetNextGeneration()
        {
            int[,] nextGen = new int[Colunms, Rows];

            for (int i = 0; i < Colunms; i++)
            {
                for (int j = 0; j < Rows; j++)
                {
                    // Count live neighbors
                    int neighbors = CountNeighbors(i, j);
                    int state = Data[i, j];

                    if (state == 0 && neighbors == 3)
                    {
                        nextGen[i, j] = 1;
                    }
                    else if (state == 1 && (neighbors < 2 || neighbors > 3))
                    {
                        nextGen[i, j] = 0;
                    }
                    else
                    {
                        nextGen[i, j] = state;
                    }
                }
            }
            Data = nextGen;
        }

        public ArrayList convertToListArray()
        {
            var arList = new ArrayList();
            for (int i = 0; i < Colunms; i++)
            {
                List<int> item = new List<int>(Rows);
                for (int j = 0; j < Rows; j++)
                {
                    item.Add(Data[i, j]);

                    //Data[i, j] = new Random().Next(2);
                }
                arList.Add(item);
            }
            return arList;
        }
    }
}
