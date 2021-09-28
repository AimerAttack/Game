using System;

namespace GameFrame.Core
{
    [Serializable]
    public class GameFrameException : Exception
    {
        public GameFrameException(string msg) : base(msg)
        {
            
        }
        
        public GameFrameException(string message, Exception innerException)
            : base(message, innerException)
        {

        }
    }
}