using Raylib_cs;

namespace SnakeGame
{
    public enum Direction
    {
        Right = 0,
        Down = 1,
        Left = 2,
        Up = 3
    }

    public class SnakePart : GridObject
    {
        public SnakeSpriteType SpriteType { get; set; } = SnakeSpriteType.Body;
        public Direction PartDirection { get; set; } = Direction.Right;

        public override void Draw(int cellSize)
        {
            var texture = SpriteManager.GetTexture("snake");
            if (texture.Width != 0 && texture.Height != 0)
            {
                Rectangle source = SpriteRects.Body;
                switch (SpriteType)
                {
                    case SnakeSpriteType.Head: source = SpriteRects.Head; break;
                    case SnakeSpriteType.Body: source = SpriteRects.Body; break;
                    case SnakeSpriteType.Turn: source = SpriteRects.Turn; break;
                    case SnakeSpriteType.Tail: source = SpriteRects.Tail; break;
                }

                float rotation = 0f;
                switch (PartDirection)
                {
                    case Direction.Right: rotation = (float)(Math.PI); ; break;
                    case Direction.Down: rotation = (float)(-Math.PI / 2); break;
                    case Direction.Left: rotation = 0f; break;
                    case Direction.Up: rotation = (float)(Math.PI / 2); break;
                }

                
                var origin = new System.Numerics.Vector2(source.Width / 2, source.Height / 2);

                
                var position = new System.Numerics.Vector2(
                    X * cellSize + cellSize / 2,
                    Y * cellSize + cellSize / 2);

                Raylib.DrawTexturePro(
                    texture,
                    source,
                    new Rectangle(position.X, position.Y, cellSize, cellSize),
                    origin,
                    rotation * 180f / (float)Math.PI, 
                    Color.White);
            }
            else
            {
                Raylib.DrawRectangle(X * cellSize, Y * cellSize, cellSize, cellSize, Color.Green);
            }
        }
    }
}