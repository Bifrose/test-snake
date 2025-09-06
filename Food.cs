using Raylib_cs;
using System;

namespace SnakeGame
{
    public class Food : GridObject
    {
        private static readonly Random rnd = new Random();

        private float stateTimer = 0f;
        private float stateDuration = 2f; 
        public int State { get; private set; } = 0; // 0 = normal (rouge), 1 = spécial (violet)

        private float lifeTimer = 0f;
        private const float LifeDuration = 10f; 

        public Food() { }

        public void Update(float deltaTime, GameGrid grid, Snake snake)
        {
           
            stateTimer += deltaTime;
            if (stateTimer >= stateDuration)
            {
                State = 1 - State;
                stateTimer = 0f;
                stateDuration = (float)rnd.NextDouble() * 3f + 1f; 
            }

          
            lifeTimer += deltaTime;
            if (lifeTimer >= LifeDuration)
            {
                PlaceRandom(grid, snake);
                lifeTimer = 0f;
                stateTimer = 0f;
                stateDuration = (float)rnd.NextDouble() * 3f + 1f;
                State = 0;
            }
        }

        public override void Draw(int cellSize)
        {
            Color color = State == 0 ? Color.Red : Color.Purple;
            Raylib.DrawRectangle(X * cellSize, Y * cellSize, cellSize, cellSize, color);

            
            string timerText = ((int)(LifeDuration - lifeTimer)).ToString();
            Raylib.DrawText(timerText, X * cellSize + cellSize / 4, Y * cellSize + cellSize / 4, cellSize / 2, Color.Black);
        }

        public void PlaceRandom(GameGrid grid, Snake snake)
        {
            int maxTry = grid.Cols * grid.Rows;
            int tryCount = 0;
            do
            {
                X = rnd.Next(0, grid.Cols);
                Y = rnd.Next(0, grid.Rows);
                tryCount++;
            } while (snake.IsOnSnake(X, Y) && tryCount < maxTry);
        }

        public void Respawn(GameGrid grid, Snake snake)
        {
            PlaceRandom(grid, snake);
            lifeTimer = 0f;
            stateTimer = 0f;
            stateDuration = (float)rnd.NextDouble() * 3f + 1f;
            State = 0;
        }
    }
}
