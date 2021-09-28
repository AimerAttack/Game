
using System;

namespace GameFrame.Core
{
    public class EventComponent : GameFrameComponentBase
    {
        private EventManager manager;
        
        protected override void OnAwake()
        {
            manager = CoreEntry.AddModule<EventManager>();
        }

        public void AddListener(int id, EventHandler<GameEventArgs> handler)
        {
            manager.AddListener(id,handler);
        }

        public void RemoveListener(int id, EventHandler<GameEventArgs> handler)
        {
            manager.RemoveListener(id,handler);
        }

        public void Send(object sender,GameEventArgs e)
        {
            manager.Send(sender,e);
        }

        public void SendNow(object sender, GameEventArgs e)
        {
            manager.SendNow(sender,e);
        }

        public bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            return manager.Check(id, handler);
        }
    }
}