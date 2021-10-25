using System;
using GameFrame.Core;

namespace GameFrame.Logic
{
    public partial class Entry
    {
        public static class Utility
        {
            public static void AddListener(int id,EventHandler<GameEventArgs> handler)
            {
                if(!Entry.Event.Check(id,handler))
                    return;
                Entry.Event.AddListener(id,handler);
            }

            public static void RemoveListener(int id, EventHandler<GameEventArgs> handler)
            {
                Entry.Event.RemoveListener(id,handler);
            }
        }
    }
}