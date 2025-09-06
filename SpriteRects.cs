using Raylib_cs;

namespace SnakeGame
{
    public static class SpriteRects
    {
        public static readonly Rectangle Head = new Rectangle(96, 0, 32, 32);
        public static readonly Rectangle Body = new Rectangle(96, 32, 32, 32);
        public static readonly Rectangle Turn = new Rectangle(96, 64, 32, 32);
        public static readonly Rectangle Tail = new Rectangle(96, 96, 32, 32);
    }
}