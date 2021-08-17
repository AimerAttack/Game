using System.Collections.Generic;

namespace GameFrame.Core
{
    public class FsmManager : GameFrameModule
    {
        private readonly Dictionary<string, FsmBase> m_Fsms = new Dictionary<string, FsmBase>();
        
        internal Fsm<T> CreateFsm<T>(T owner,params FsmState<T>[] states) where T : class
        {
            return CreateFsm(Utility.Text.GetFullName<T>(string.Empty), owner, states);
        }

        internal Fsm<T> CreateFsm<T>(string name, T owner, FsmState<T>[] states) where T : class
        {
            if (HasFsm<T>(name))
            {
                throw new GameFrameException(Utility.Text.Format("Already exist FSM '{0}'.", Utility.Text.GetFullName<T>(name)));
            }

            var fsm = new Fsm<T>(name, owner, states);
            
            return fsm;
        }

        private bool HasFsm<T>(string name) where T : class
        {
            return m_Fsms.ContainsKey(name);
        }
        
        
        internal override void ShutDown()
        {
        }
    }
}