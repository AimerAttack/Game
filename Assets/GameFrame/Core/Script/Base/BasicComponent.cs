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

    }
}