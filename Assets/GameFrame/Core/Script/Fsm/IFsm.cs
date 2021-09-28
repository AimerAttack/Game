using System;

namespace GameFrame.Core
{
    public interface IFsm<T> where T : class
    {
        FsmState<T> curState
        {
            get;
        }

        void Start(Type type);
        void Start<TState>() where TState : FsmState<T>;

        void ChangeState<TState>() where TState : FsmState<T>;

        void ChangeState(Type type);
    }
}