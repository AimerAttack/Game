namespace GameFrame.Logic
{
    public abstract class UIFrame<T> : UIBase where T : UIObjHolder
    {
        private T _holder;
        
        protected T holder
        {
            get
            {
                if (_holder == null)
                    _holder = GetHolder<T>();
                return _holder;
            }
        }
    }
}