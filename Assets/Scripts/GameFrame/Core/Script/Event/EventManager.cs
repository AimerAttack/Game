using System;
using System.Collections.Generic;

namespace GameFrame.Core
{
    public partial class EventManager : GameFrameModule
    {
        private Dictionary<int, LinkedList<EventHandler<GameEventArgs>>> eventPool;
        private readonly Queue<Event> m_Events;

        public EventManager()
        {
            eventPool = new Dictionary<int, LinkedList<EventHandler<GameEventArgs>>>();
            m_Events = new Queue<Event>();
        }
        
        internal override void ShutDown()
        {
            
        }

        public void AddListener(int id, EventHandler<GameEventArgs> handler)
        {
            if (handler == null)
                throw new GameFrameException("handler is invalid");

            LinkedList<EventHandler<GameEventArgs>> list = null;
            if (!eventPool.TryGetValue(id, out list))
            {
                list = new LinkedList<EventHandler<GameEventArgs>>();
                eventPool.Add(id,list);
                list.AddLast(handler);
            }
            else
            {
                list.AddLast(handler);
            }
        }

        public void RemoveListener(int id, EventHandler<GameEventArgs> handler)
        {
            if (handler == null)
                throw new GameFrameException("handler is invalid");
            LinkedList<EventHandler<GameEventArgs>> list = null;
            
        }

        public void Send(object sender,GameEventArgs e)
        {
            var evt = new Event();
            evt.sender = sender;
            evt.e = e;
            lock (m_Events)
            {
                m_Events.Enqueue(evt);
            }
        }

        public void SendNow(object sender, GameEventArgs e)
        {
            HandleEvent(sender,e);
        }

        internal bool Check(int id, EventHandler<GameEventArgs> handler)
        {
            LinkedList<EventHandler<GameEventArgs>> list = null;
            if (eventPool.TryGetValue(id, out list))
            {
                return list.Contains(handler);
            }
            return false;
        }

        void HandleEvent(object sender,GameEventArgs e)
        {
            int eventId = e.Id;

            LinkedList<EventHandler<GameEventArgs>> list = null;
            if (eventPool.TryGetValue(eventId, out list))
            {
                var current = list.First;
                while (current != null)
                {
                    current.Value(sender, e);
                    current = current.Next;
                }
            }
        }

        internal override void Update(float deltaTime, float realDeltaTime)
        {
            lock (m_Events)
            {
                while (m_Events.Count > 0)
                {
                    var e = m_Events.Dequeue();
                    HandleEvent(e.sender,e.e);
                }
            }
        }
    }
}