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

        private Bitmap startImage;
        private int startImageRow = 0;
        private int startImageCol = 0;
        private Person person;

        public GameGrid(Person person)
        {
            cells = new Cell[Rows, Columns];
            InitializeGrid();

            this.person = person;
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

        public Bitmap GetStartImage()
        {
            return startImage;
        }

        public int StartImageRow
        {
            get => startImageRow;
            set => startImageRow = value;
        }

        public int StartImageCol
        {
            get => startImageCol;
            set => startImageCol = value;
        }

        public void Draw(Graphics g, int startX, int startY)
        {
            for (int row = 0; row < Rows; row++)
            {
                for (int col = 0; col < Columns; col++)
                {
                    Brush brush = Brushes.White;
                    g.FillRectangle(brush, startX + col * cellSize, startY + row * cellSize, cellSize, cellSize);
                    g.DrawRectangle(Pens.Black, startX + col * cellSize, startY + row * cellSize, cellSize, cellSize);
                }
            }

            Bitmap currentImage = person.GetCharacterImage(); 
            if (currentImage != null)
            {
                int imageX = startX + person.PlaceX * cellSize;
                int imageY = startY + person.PlaceY * cellSize;

                g.DrawImage(currentImage, imageX, imageY, cellSize, cellSize);
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
