using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Core
{
    public class ProcedureComponent : GameFrameComponentBase
    {
        private string[] m_AvailableProcedureTypeNames = null;
        private string m_EntranceProcedureTypeName = null;
        
        private GameFrameProcedureBase m_EntranceProcedure = null;
        
        private ProcedureManager manager;
        
        public GameFrameProcedureBase CurrentProcedure
        {
            get
            {
                return manager.CurrentProcedure;
            }
        }
        
        protected override void OnAwake()
        {
            manager = CoreEntry.AddModule<ProcedureManager>();
        }

        public void StartLogic(List<GameFrameProcedureBase> procedures)
        {

            if (procedures.Count == 0)
            {
                Log.Error("Procedure number is 0");
                return;
            }
            
            m_EntranceProcedure = procedures[0];
            if (m_EntranceProcedure == null)
            {
                Log.Error("Entrance procedure is invalid.");
                return;
            }

            CoreEntry.Coroutine.StartCoroutine(DelayStart(procedures));
        }

        private IEnumerator DelayStart(List<GameFrameProcedureBase> procedures)
        {
            manager.Initialize(CoreEntry.GetModule<FsmManager>(),procedures.ToArray());
            
            yield return new WaitForEndOfFrame();
            
            manager.StartProcedure(m_EntranceProcedure.GetType());
        }
    }
}