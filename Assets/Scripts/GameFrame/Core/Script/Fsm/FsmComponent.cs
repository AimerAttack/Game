using System;

namespace GameFrame.Core
{
    public class FsmComponent : GameFrameComponentBase
    {
        private FsmManager manager;

        protected override void OnAwake()
        {
            manager = CoreEntry.AddModule<FsmManager>();
        }

        private void Update()
        {
            
        }
    }
}