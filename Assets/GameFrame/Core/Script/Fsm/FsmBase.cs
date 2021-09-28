namespace GameFrame.Core
{
    public abstract class FsmBase
    {
        
        private string m_Name;
        
        public FsmBase(string name)
        {
            m_Name = name ?? string.Empty;
        }

        public string Name
        {
            get { return m_Name; }
        }
        internal abstract void Update(float elapseSeconds, float realElapseSeconds);
    }
}