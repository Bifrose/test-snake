namespace SnakeGame
{
    public class Timer
    {
        private readonly float duration;
        private float elapsed;

        public bool IsFinished => elapsed >= duration;

        public Timer(float duration)
        {
            this.duration = duration;
            elapsed = 0;
        }

        public void Update(float deltaTime)
        {
            elapsed += deltaTime;
        }

        public void Reset()
        {
            elapsed = 0;
        }
    }
}