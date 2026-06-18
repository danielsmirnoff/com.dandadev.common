using CommonDan;
using UnityEngine;

namespace CommonDan
{
    public abstract class BaseStateManager<T> where T : BaseStateManager<T>
    {
        private const bool debugMode = false;

        protected BaseState<T> currentState;

        public void SwitchState(BaseState<T> newState)
        {
            if (debugMode) Debug.Log("Switching to " + newState.GetType().Name);
            currentState?.ExitState();
            currentState = newState;
            currentState?.EnterState();
        }

        public void Update()
        {
            currentState?.UpdateState();
        }

        public BaseState<T> GetCurrentState()
        {
            return currentState;
        }
    }

    public abstract class BaseState<T> where T : BaseStateManager<T>
    {
        protected T manager;

        public BaseState(T manager)
        {
            this.manager = manager;
        }

        public abstract void EnterState();
        public abstract void UpdateState();
        public abstract void ExitState();
    }
}