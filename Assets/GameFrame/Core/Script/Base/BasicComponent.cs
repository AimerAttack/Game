using System;
using UnityEngine;

namespace GameFrame.Core
{
    public class BasicComponent : GameFrameComponentBase
    {
        protected override void OnAwake()
        {
            base.OnAwake();
            
            GameFrameLog.SetLogHelper(new DefaultLogHelper());
        }

        private void Update()
        {
            CoreEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }
    }
}