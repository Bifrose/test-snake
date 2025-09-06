namespace SnakeGame
{
    public class GameStateManager
    {
        public bool IsGameOver { get; private set; } = false;
        public bool IsPaused { get; private set; } = false;

        public void SetGameOver()
        {
            IsGameOver = true;
            IsPaused = false;
        }

        public void Reset()
        {
            IsGameOver = false;
            IsPaused = false;
        }

        public void TogglePause()
        {
            if (!IsGameOver)
                IsPaused = !IsPaused;
        }
    }
}