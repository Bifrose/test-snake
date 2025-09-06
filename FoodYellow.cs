using Raylib_cs;

namespace SnakeGame
{
    public class FoodYellow : Food
    {
        public override void Draw(int cellSize)
        {
            Raylib.DrawRectangle(X * cellSize, Y * cellSize, cellSize, cellSize, Color.Yellow);
        }
    }
}