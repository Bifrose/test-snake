using Raylib_cs;
using System;
namespace SnakeGame
{
    public class GameGrid
    {
        public int Rows { get; private set; }
        public int Cols { get; private set; }
        public int CellSize { get; private set; }

        public void UpdateGrid(int windowWidth, int windowHeight)
        {
            CellSize = Math.Min(windowWidth / 20, windowHeight / 20);
            Cols = windowWidth / CellSize;
            Rows = windowHeight / CellSize;
        }

        public void Draw()
        {
            for (int i = 0; i < Cols; i++)
                for (int j = 0; j < Rows; j++)
                    Raylib.DrawRectangleLines(i * CellSize, j * CellSize, CellSize, CellSize, Color.Gray);
        }
    }
}