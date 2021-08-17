using System;
using System.Collections;
using UnityEngine;

namespace GameFrame.Core
{
    public class ProcedureComponent : GameFrameComponentBase
    {
        [SerializeField]
        private string[] m_AvailableProcedureTypeNames = null;
        [SerializeField]
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

        private IEnumerator Start()
        {
            GameFrameProcedureBase[] procedures = new GameFrameProcedureBase[m_AvailableProcedureTypeNames.Length];
            for (int i = 0; i < m_AvailableProcedureTypeNames.Length; i++)
            {
                Type procedureType = Utility.Assembly.GetType(m_AvailableProcedureTypeNames[i]);
                if (procedureType == null)
                {
                    Log.Error("Can not find procedure type '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                procedures[i] = (GameFrameProcedureBase)Activator.CreateInstance(procedureType);
                if (procedures[i] == null)
                {
                    Log.Error("Can not create procedure instance '{0}'.", m_AvailableProcedureTypeNames[i]);
                    yield break;
                }

                if (m_EntranceProcedureTypeName == m_AvailableProcedureTypeNames[i])
                {
                    m_EntranceProcedure = procedures[i];
                }
            }

            if (m_EntranceProcedure == null)
            {
                Log.Error("Entrance procedure is invalid.");
                yield break;
            }
            
            manager.Initialize(CoreEntry.GetModule<FsmManager>(),procedures);
            
            yield return new WaitForEndOfFrame();
            
            manager.StartProcedure(m_EntranceProcedure.GetType());
        }
    }
}