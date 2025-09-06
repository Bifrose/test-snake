namespace SnakeGame
{


    public abstract class GridObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public abstract void Draw(int cellSize);
    }
}