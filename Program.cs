using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LabiryntCode
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormCode());
                       
        }
    }

    class MazeLogic
    {
        Random rnd = new Random();
        public Stack<int> mazePath = new Stack<int>();
        public List<Cell> MazeGrid;
        public List<int> randomList;

        int EndY;
        int StartY;
        Cell startCell;
        Cell endCell;

        int cols, rows;

        public Cell currentCell;
        public int endDirect;

        public MazeLogic(int col, int row)
        {
            randomList = new List<int>();
            endDirect = 0;

            randomList.Add(1);
            randomList.Add(2);
            randomList.Add(3);
            randomList.Add(4);

            currentCell = new Cell(0, 0);
            MazeGrid = new List<Cell>();

            cols = col;
            rows = row;

            for (int j = 0; j < rows; j++)
            {
                for (int i = 0; i < cols; i++)
                {
                    var cell = new Cell(i, j);
                    MazeGrid.Add(cell);
                }
            }

            StartY = rnd.Next(rows);
            startCell = MazeGrid.ElementAt(0 + StartY * cols);
            startCell.Start = true;

            EndY = rnd.Next(rows);
            endCell = MazeGrid.ElementAt((cols - 1) + EndY * cols);
            endCell.koniec = true;

            currentCell = startCell;
            currentCell.visited = true;

            MazeGrid[0 + StartY * cols] = currentCell;
            mazePath.Push(0 + StartY * cols);
        }
        public int index(int x, int y)//================================================================
        {
            if (x < 0 || y < 0 || x > cols - 1 || y > rows - 1)
            {
                return -1;
            }

            Cell next = MazeGrid[x + y * cols];

            if (next.visited == true)
            {
                return -1;
            }
            return x + y * cols;
        }

        public void checkNeighbours(Cell next)//========================================================
        {
            if (mazePath.Count==0)
            {
                return;
            }
            
            int posX = next.posX;
            int posY = next.posY;

            endDirect = 0;

            if (endCell.Equals(next))//=====natrafienie na koniec
            {
                endDirect = 4;
            }
            else
            {
                bool GoOn = true;

                for (int i = 0; GoOn == true && i < randomList.Count; i++)
                {
                    int randIndex = rnd.Next(0, 4 - i);
                    int randomValue = randomList.ElementAt(randIndex);

                    switch (randomValue)
                    {

                        case 1://top
                            GoOn = CheckNeighbourDirect(posX, posY - 1);
                            randomList.RemoveAt(randIndex);
                            randomList.Add(randomValue);
                            break;

                        case 2://right
                            GoOn = CheckNeighbourDirect(posX + 1, posY);
                            randomList.RemoveAt(randIndex);
                            randomList.Add(randomValue);
                            break;

                        case 3://down
                            GoOn = CheckNeighbourDirect(posX, posY + 1);
                            randomList.RemoveAt(randIndex);
                            randomList.Add(randomValue);
                            break;

                        case 4://left
                            GoOn = CheckNeighbourDirect(posX - 1, posY);
                            randomList.RemoveAt(randIndex);
                            randomList.Add(randomValue);
                            break;

                    }
                }
            }
          
            if (endDirect == 4)// ===============cofanie
            {

                currentCell.visited = true;
                currentCell.current = false;
                

                if (mazePath.Count == 0)
                {
                    return;
                }

                  mazePath.Pop();

                if (mazePath.Count == 0)
                {
                    return;                                         
                }

                currentCell = MazeGrid.ElementAt(mazePath.Peek());
                currentCell.current = true;
                                
            }
          
        }

        public bool CheckNeighbourDirect(int posX, int posY)//=======================================================
        {

            var next = index(posX, posY);
            if (next < 0)
            {
                endDirect += 1;
                return true;
            }

            //    Step 1

            int cellIndexBefore = MazeGrid.IndexOf(currentCell);
            var CellBefore = MazeGrid.ElementAt(cellIndexBefore);
            CellBefore.visited = true;
            CellBefore.current = false;

            //     Step 2 

            currentCell = MazeGrid.ElementAt(next);
            int cellIndexAfter = MazeGrid.IndexOf(currentCell);
            var cellAfter = MazeGrid.ElementAt(cellIndexAfter);
            cellAfter.current = true;
            mazePath.Push(next);

            // step 3

            removeWalls(cellIndexBefore, cellIndexAfter);
            return false;
        }
        public void removeWalls(int a, int b)//=======================================================
        {
            Cell CellBefore = MazeGrid.ElementAt(a);
            Cell CellAfter = MazeGrid.ElementAt(b);
           
            if ((a - b) == -1)
            {
                CellBefore.leftWall = false;
                CellAfter.rightWall = false;
            }
            else if ((a - b) == 1)
            {
                CellBefore.rightWall = false;
                CellAfter.leftWall = false;
            }
            if ((a - b) == cols)
            {
                CellBefore.topWall = false;
                CellAfter.downWall = false;
            }
            else if ((a - b) == -cols)
            {
                CellBefore.downWall = false;
                CellAfter.topWall = false;
            }
        }
       

    }
    class Cell//=====================================================================================
    {
        public Cell(int x, int y)
        {
            posX = x;
            posY = y;
        }

        public int posX;
        public int posY;

        public bool Start = false;
        public bool koniec = false;

        public bool topWall = true;
        public bool rightWall = true;
        public bool downWall = true;
        public bool leftWall = true;

        public bool visited = false;
        public bool current = false;

    }

}
