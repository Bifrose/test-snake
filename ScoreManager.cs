using Raylib_cs;
namespace SnakeGame
{
    public class ScoreManager
    {
        public int Score { get; private set; } = 0;

        public void Add(int points)
        {
            Score += points;
        }

        public void Subtract(int points)
        {
            Score -= points;
            if (Score < 0) Score -= points;
        }

        public void Reset()
        {
            Score = 0;
        }

        public void Draw(int windowWidth)
        {
            string scoreText = $"Score : {Score}";
            int textWidth = Raylib.MeasureText(scoreText, 30);
            Raylib.DrawText(scoreText, windowWidth / 2 - textWidth / 2, 550, 30, Color.DarkGreen);
        }
    }
}