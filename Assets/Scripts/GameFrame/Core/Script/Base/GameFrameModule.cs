namespace GameFrame.Core
{
    public abstract class GameFrameModule
    {
        internal virtual int Priority
        {
            get
            {
                return 0;
            }
        }

        internal virtual void Update(float deltaTime, float realDeltaTime)
        {
            
        }

        internal abstract void ShutDown();
    }
}