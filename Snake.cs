using System.Collections.Generic;
using Raylib_cs;
using System.Linq;
using SnakeGame;

namespace SnakeGame
{
    public class Snake
    {
        private readonly List<SnakePart> parts = new List<SnakePart>();
        private int dirX = 1, dirY = 0;
        private float timer = 0;
        public float Speed { get; private set; } = 0.2f;
        // Ajoutez un accesseur set public à la propriété IsGameOver
        public bool IsGameOver { get; set; }
        private bool canChangeDirection = true;

        public Snake()
        {
            parts.Add(new SnakePart { X = 5, Y = 5 });
            parts.Add(new SnakePart { X = 4, Y = 5 });
            parts.Add(new SnakePart { X = 3, Y = 5 });
        }

        public void UpdateDirection()
        {
            if (!canChangeDirection) return;

            if (Raylib.IsKeyPressed(KeyboardKey.Up) && dirY != 1) { dirX = 0; dirY = -1; canChangeDirection = false; }
            else if (Raylib.IsKeyPressed(KeyboardKey.Down) && dirY != -1) { dirX = 0; dirY = 1; canChangeDirection = false; }
            else if (Raylib.IsKeyPressed(KeyboardKey.Left) && dirX != 1) { dirX = -1; dirY = 0; canChangeDirection = false; }
            else if (Raylib.IsKeyPressed(KeyboardKey.Right) && dirX != -1) { dirX = 1; dirY = 0; canChangeDirection = false; }
        }       

        public bool Move(GameGrid grid, params Food[] foods)
        {
            if (IsGameOver) return false;

            timer += Raylib.GetFrameTime();
            if (timer < Speed) return false;
            timer = 0;

            canChangeDirection = true; 

            int nextDirX = dirX, nextDirY = dirY;
            dirX = nextDirX;
            dirY = nextDirY;

            int newX = parts[0].X + dirX;
            int newY = parts[0].Y + dirY;

            if (newX < 0 || newX >= grid.Cols || newY < 0 || newY >= grid.Rows)
            {
                IsGameOver = true;
                return false;
            }

            if (IsOnSnake(newX, newY))
            {
                IsGameOver = true;
                return false;
            }

            parts.Insert(0, new SnakePart { X = newX, Y = newY });

           
            bool ateFood = false;
            foreach (var food in foods)
            {
                if (food != null && newX == food.X && newY == food.Y)
                {
                    ateFood = true;
                    if (food.State == 1)
                    {
                        if (parts.Count > 1)
                        {
                            parts.RemoveAt(parts.Count - 1);
                            if (parts.Count == 1)
                            {
                                IsGameOver = true;
                                return false;
                            }
                        }
                        else
                        {
                            IsGameOver = true;
                            return false;
                        }
                    }
                    break;
                }
            }

            if (!ateFood && parts.Count > 1)
                parts.RemoveAt(parts.Count - 1);

            return ateFood;
        }

        public void Draw(int cellSize)
        {
            UpdateSpriteTypes();
            foreach (var part in parts)
                part.Draw(cellSize);
        }
        public int Length => parts.Count;
        public bool IsOnSnake(int x, int y)
        {
            foreach (var part in parts)
                if (part.X == x && part.Y == y)
                    return true;
            return false;
        }

        internal void IncreaseSpeed()
        {
            Speed = Math.Max(0.09f, Speed - 0.01f); 
        }

        public void Reset()
        {
            parts.Clear();
            parts.Add(new SnakePart { X = 5, Y = 5 });
            parts.Add(new SnakePart { X = 4, Y = 5 });
            parts.Add(new SnakePart { X = 3, Y = 5 });
            dirX = 1;
            dirY = 0;
            timer = 0;
            Speed = 0.2f;
            IsGameOver = false;
        }

        public int BodyLength => parts.Count;

        public void RemoveTail()
        {
            if (parts.Count > 1)
                parts.RemoveAt(parts.Count - 1);
        }

        public SnakePart GetHead()
        {
            return parts[0];
        }

        public void UpdateSpriteTypes()
        {
            for (int i = 0; i < parts.Count; i++)
            {
                if (i == 0)
                {
                    parts[i].SpriteType = SnakeSpriteType.Head;
                    if (parts.Count > 1)
                        parts[i].PartDirection = GetDirection(parts[i], parts[i + 1]);
                }
                else if (i == parts.Count - 1)
                {
                    parts[i].SpriteType = SnakeSpriteType.Tail;
                    parts[i].PartDirection = GetDirection(parts[i - 1], parts[i]);
                }
                else
                {
                    var prev = parts[i - 1];
                    var next = parts[i + 1];
                    if ((prev.X != next.X) && (prev.Y != next.Y))
                        parts[i].SpriteType = SnakeSpriteType.Turn;
                    else
                        parts[i].SpriteType = SnakeSpriteType.Body;

                    parts[i].PartDirection = GetDirection(prev, parts[i]);
                }
            }
        }

        private Direction GetDirection(SnakePart from, SnakePart to)
        {
            if (from.X < to.X) return Direction.Right;
            if (from.X > to.X) return Direction.Left;
            if (from.Y < to.Y) return Direction.Down;
            return Direction.Up;
        }
    }
}