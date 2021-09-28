namespace GameFrame.Core
{
    public class FsmState<T> where T : class
    {
        private IFsm<T> fsm;

        internal void SetFsm(IFsm<T> _fsm)
        {
            fsm = _fsm;
        }

        internal void Enter()
        {
            Log.Info("{0}进入{1}阶段", fsm.GetType(), Utility.Text.GetFullName(GetType(), string.Empty));
            OnEnter();
        }

        protected virtual void OnEnter()
        {
        }

        internal void Leave()
        {
            Log.Info("{0}离开{1}阶段", fsm.GetType(), Utility.Text.GetFullName(GetType(), string.Empty));
            OnLeave();
        }

        protected virtual void OnLeave()
        {
        }

        internal void Update(float elapseSeconds, float realElapseSeconds)
        {
            OnUpdate(elapseSeconds, realElapseSeconds);
        }

        protected virtual void OnUpdate(float elapseSeconds, float realElapseSeconds)
        {
        }

        protected void ChangeState<TState>() where TState : FsmState<T>
        {
            fsm.ChangeState<TState>();
        }
    }
}