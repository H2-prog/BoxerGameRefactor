namespace BoxerGamerRefactor
{
    public abstract class ConsoleGame : IConsoleGame
    {
        private bool _shouldStop = false;

        public void GameLoop()
        {
            Start();
            while(!_shouldStop)
            {
                Update();
            }
        }

        public void Exit()
        {
            _shouldStop = true;
        }

        public abstract void Start();
        public abstract void Update();
    }
}
