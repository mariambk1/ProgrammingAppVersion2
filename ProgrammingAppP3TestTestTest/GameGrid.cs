using System;
using System.Drawing;

namespace ProgrammingApp
{
    public class GameGrid
    {
        public const int Rows = 10;
        public const int Columns = 10;
        private Cell[,] cells;
        private int cellSize = 45; 

        public GameGrid()
        {
            cells = new Cell[Rows, Columns];
            InitializeGrid();
        }

        private void InitializeGrid()
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    cells[row, col] = new Cell(row, col);
                }
            }
        }

        public Cell GetCell(int row, int col)
        {
            return cells[row, col];
        }

        public void SetCellState(int row, int col, bool isOccupied)
        {
            cells[row, col].IsOccupied = isOccupied;
        }

        public void Draw(Graphics g, int startX, int startY)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Brush brush = cells[row, col].IsOccupied ? Brushes.Red : Brushes.White;

                    g.FillRectangle(brush, startX + col * cellSize, startY + row * cellSize, cellSize, cellSize);
                    g.DrawRectangle(Pens.Black, startX + col * cellSize, startY + row * cellSize, cellSize, cellSize);
                }
            }
        }

        public int CellSize
        {
            get => cellSize;
            set => cellSize = value; 
        }
    }

    public class Cell
    {
        public int Row { get; }
        public int Column { get; }
        public bool IsOccupied { get; set; }

        public Cell(int row, int column)
        {
            Row = row;
            Column = column;
            IsOccupied = false;
        }
    }
}
