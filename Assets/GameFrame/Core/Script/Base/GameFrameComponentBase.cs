using System;
using UnityEngine;

namespace GameFrame.Core
{
    public abstract class GameFrameComponentBase
    {
        public GameFrameComponentBase()
        {
            CoreEntry.RegistComponent(this);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}