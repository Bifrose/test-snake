using Raylib_cs;
using System;
using System.Collections.Generic;

namespace SnakeGame
{
    public class Program
    {
        public static void Main()
        {
            Raylib.InitWindow(800, 600, "Snake Raylib");
            SpriteManager.LoadTexture("snake", @"D:\Projet C#\test snake\asset\BattleSnake\snakes\snakesSets\snake011.png");
            Raylib.SetTargetFPS(60);

            GameGrid grid = new GameGrid();
            Snake snake = new Snake();
            Food food1 = new Food();
            Food? food2 = null;
            GameStateManager gameState = new GameStateManager();
            ScoreManager scoreManager = new ScoreManager();

            List<FoodYellow> foodYellows = new List<FoodYellow>();
            int redStreak = 0;
            float food2Delay = 2f;
            float food2Timer = 0f;

            food1.PlaceRandom(grid, snake);

            while (!Raylib.WindowShouldClose())
            {
                grid.UpdateGrid(Raylib.GetScreenWidth(), Raylib.GetScreenHeight() - 100);

                if (Raylib.IsKeyPressed(KeyboardKey.P))
                    gameState.TogglePause();

                if (gameState.IsGameOver && Raylib.IsKeyPressed(KeyboardKey.Space))
                {
                    snake.Reset();
                    food1.PlaceRandom(grid, snake);
                    food2 = null;
                    food2Timer = 0f;
                    gameState.Reset();
                    scoreManager.Reset();
                    foodYellows.Clear();
                    redStreak = 0;
                }

                if (!gameState.IsGameOver && !gameState.IsPaused)
                {
                    food1.Update(Raylib.GetFrameTime(), grid, snake);
                    if (food2 == null)
                    {
                        food2Timer += Raylib.GetFrameTime();
                        if (food2Timer >= food2Delay)
                        {
                            food2 = new Food();
                            food2.PlaceRandom(grid, snake);
                        }
                    }
                    else
                    {
                        food2.Update(Raylib.GetFrameTime(), grid, snake);
                    }

                    snake.UpdateDirection();


                    bool hasMoved = snake.Move(grid, food1, food2);
                    var head = snake.GetHead();


                    if (food1 != null && head.X == food1.X && head.Y == food1.Y)
                    {
                        snake.IncreaseSpeed();
                        if (food1.State == 0)
                        {
                            scoreManager.Add(100);
                            redStreak++;
                            if (redStreak == 3)
                            {
                                var newYellow = new FoodYellow();
                                newYellow.PlaceRandom(grid, snake);
                                foodYellows.Add(newYellow);
                                redStreak = 0;
                            }
                        }
                        else
                        {
                            redStreak = 0;
                            if (snake.Length > 1)
                                snake.RemoveTail();
                            else
                                snake.IsGameOver = true;
                        }
                        food1.Respawn(grid, snake);
                    }


                    if (food2 != null && head.X == food2.X && head.Y == food2.Y)
                    {
                        snake.IncreaseSpeed();
                        if (food2.State == 0)
                        {
                            scoreManager.Add(100);
                            redStreak++;
                            if (redStreak == 3)
                            {
                                var newYellow = new FoodYellow();
                                newYellow.PlaceRandom(grid, snake);
                                foodYellows.Add(newYellow);
                                redStreak = 0;
                            }
                        }
                        else
                        {
                            redStreak = 0;
                            if (snake.Length > 1)
                                snake.RemoveTail();
                            else
                                snake.IsGameOver = true;
                        }
                        food2.Respawn(grid, snake);
                    }

                    for (int i = foodYellows.Count - 1; i >= 0; i--)
                    {
                        var fy = foodYellows[i];
                        if (head.X == fy.X && head.Y == fy.Y)
                        {
                            scoreManager.Add(1000);
                            foodYellows.RemoveAt(i);
                        }
                    }

                    if (snake.IsGameOver)
                        gameState.SetGameOver();
                }

                Raylib.BeginDrawing();
                Raylib.ClearBackground(Color.White);

                grid.Draw();
                food1.Draw(grid.CellSize);
                if (food2 != null)
                    food2.Draw(grid.CellSize);
                snake.Draw(grid.CellSize);

                foreach (var fy in foodYellows)
                    fy.Draw(grid.CellSize);

                scoreManager.Draw(Raylib.GetScreenWidth());

                if (gameState.IsPaused && !gameState.IsGameOver)
                {
                    string pauseMsg = "PAUSE (P pour reprendre)";
                    int pauseWidth = Raylib.MeasureText(pauseMsg, 30);
                    Raylib.DrawText(pauseMsg, Raylib.GetScreenWidth() / 2 - pauseWidth / 2, Raylib.GetScreenHeight() / 2 - 15, 30, Color.Blue);
                }

                if (gameState.IsGameOver)
                {
                    string msg = "GAME OVER";
                    int textWidth = Raylib.MeasureText(msg, 50);
                    Raylib.DrawText(msg, Raylib.GetScreenWidth() / 2 - textWidth / 2, Raylib.GetScreenHeight() / 2 - 25, 50, Color.Red);

                    string restartMsg = "Appuie sur ESPACE pour rejouer";
                    int restartWidth = Raylib.MeasureText(restartMsg, 20);
                    Raylib.DrawText(restartMsg, Raylib.GetScreenWidth() / 2 - restartWidth / 2, Raylib.GetScreenHeight() / 2 + 40, 20, Color.DarkGray);
                }

                Raylib.EndDrawing();
            }
            SpriteManager.UnloadAll();
            Raylib.CloseWindow();
        }
    }
}
