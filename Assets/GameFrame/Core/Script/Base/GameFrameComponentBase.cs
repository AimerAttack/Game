using System;
using UnityEngine;

namespace GameFrame.Core
{
    public abstract class GameFrameComponentBase : MonoBehaviour
    {
        private void Awake()
        {
            CoreEntry.RegistComponent(this);
            OnAwake();
        }

        protected virtual void OnAwake()
        {
            
        }
    }
}