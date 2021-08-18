using System;

namespace GameFrame.Core
{
    public abstract class GameEventArgs : EventArgs
    {
        public abstract int Id
        {
            get;
        }

        public abstract void Clear();
    }
}