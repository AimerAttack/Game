using System;
using System.Collections.Generic;

namespace GameFrame.Core
{
    public class Fsm<T> : FsmBase,IFsm<T> where T : class
    {
        public FsmState<T> curState { get; private set; }
        private readonly T owner;
        private readonly Dictionary<string, FsmState<T>> states;
        private float currentStateTime;

        public Fsm(string _name,T _owner,FsmState<T>[] _state) : base(_name)
        {
            if (_owner == null)
            {
                throw new GameFrameException("FSM owner is invalid.");
            }

            if (_state == null || _state.Length < 1)
            {
                throw new GameFrameException("FSM states is invalid.");
            }
            
            owner = _owner;
            states = new Dictionary<string, FsmState<T>>();
            
            foreach (FsmState<T> state in _state)
            {
                if (state == null)
                {
                    throw new GameFrameException("FSM states is invalid.");
                }

                string stateName = state.GetType().FullName;
                if (states.ContainsKey(stateName))
                {
                    throw new GameFrameException(Utility.Text.Format("FSM '{0}' state '{1}' is already exist.", Utility.Text.GetFullName<T>(_name), stateName));
                }

                states.Add(stateName, state);
                state.SetFsm(this);
            }

            currentStateTime = 0f;
            curState = null;
        }

        public bool IsRunning => curState != null;

        public void Start(Type type)
        {
            if (IsRunning)
            {
                throw new GameFrameException("FSM is running, can not start again.");
            }

            if (type == null)
            {
                throw new GameFrameException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(type))
            {
                throw new GameFrameException(Utility.Text.Format("State type '{0}' is invalid.", type.FullName));
            }

            FsmState<T> state = GetState(type);
            if (state == null)
            {
                throw new GameFrameException(Utility.Text.Format("FSM '{0}' can not start state '{1}' which is not exist.", Utility.Text.GetFullName<T>(Name), type.FullName));
            }

            currentStateTime = 0;
            curState = state;
            curState.Enter();
        }

        public void Start<TState>() where TState : FsmState<T>
        {
            if (IsRunning)
            {
                throw new GameFrameException("FSM is running, can not start again.");
            }

            var state = GetState<TState>();
            if(state == null)
                throw new GameFrameException(Utility.Text.Format("FSM '{0}' can not start state to '{1}' which is not exist.", Utility.Text.GetFullName<T>(Name), typeof(TState).FullName));

            currentStateTime = 0;
            curState = state;
            curState.Enter();
        }

        public void ChangeState<TState>() where TState : FsmState<T>
        {
            ChangeState(typeof(TState));
        }

        public void ChangeState(Type type)
        {
            if (curState == null)
                throw new GameFrameException("curState is invalid");

            var state = GetState(type);
            if (state == null)
                throw new GameFrameException(Utility.Text.Format("FSM '{0}' can not change state to '{1}' which is not exist.", Utility.Text.GetFullName<T>(Name), type.FullName));

            curState.Leave();
            currentStateTime = 0;
            curState = state;
            state.Enter();
        }

        private TState GetState<TState>() where TState : FsmState<T>
        {
            FsmState<T> state = null;
            if (states.TryGetValue(typeof(TState).FullName, out state))
            {
                return (TState)state;
            }

            return null;
        }

        private FsmState<T> GetState(Type type)
        {
            if (type == null)
            {
                throw new GameFrameException("State type is invalid.");
            }

            if (!typeof(FsmState<T>).IsAssignableFrom(type))
            {
                throw new GameFrameException(Utility.Text.Format("State type '{0}' is invalid.", type.FullName));
            }

            FsmState<T> state = null;
            if (states.TryGetValue(type.FullName, out state))
            {
                return state;
            }

            return null;
        }

        internal override void Update(float elapseSeconds, float realElapseSeconds)
        {
            if (curState == null)
            {
                return;
            }

            currentStateTime += elapseSeconds;
            curState.Update(elapseSeconds, realElapseSeconds);
        }
    }
}