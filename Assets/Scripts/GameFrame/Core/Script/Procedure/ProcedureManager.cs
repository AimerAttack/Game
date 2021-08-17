using System;
using UnityEngine;

namespace GameFrame.Core
{
    public class ProcedureManager : GameFrameModule
    {
        public GameFrameProcedureBase CurrentProcedure
        {
            get
            {
                return fsm.curState as GameFrameProcedureBase;
            }
        }

        private FsmManager fsmManager;
        private IFsm<ProcedureManager> fsm;

        internal void Initialize(FsmManager _fsmManager,params GameFrameProcedureBase[] _procedures)
        {
            if(_fsmManager == null)
                throw new GameFrameException("FSM manager is invalid.");
            fsmManager = _fsmManager;
            fsm = fsmManager.CreateFsm(this, _procedures);
        }

        internal void StartProcedure(Type type)
        {
            if (fsm == null)
            {
                throw new GameFrameException("You must initialize procedure first.");
            }
            
            fsm.Start(type);
        }

        internal override void Update(float deltaTime, float realDeltaTime)
        {
            
        }

        internal override void ShutDown()
        {
        }
    }
}