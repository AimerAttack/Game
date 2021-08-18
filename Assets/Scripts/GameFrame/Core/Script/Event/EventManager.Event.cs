namespace GameFrame.Core
{
    public partial class EventManager
    {
        private struct Event
        {
            public object sender;
            public GameEventArgs e;
        }
    }
}