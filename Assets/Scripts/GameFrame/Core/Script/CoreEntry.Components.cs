using System;
using System.Collections.Generic;

namespace GameFrame.Core
{
    public partial class CoreEntry
    {
        private static readonly Dictionary<Type, GameFrameComponentBase> components =
            new Dictionary<Type, GameFrameComponentBase>();

        public static BasicComponent Basic
        {
            get;
            private set;
        }
        
        public static FsmComponent Fsm
        {
            get;
            private set;
        }

        public static ProcedureComponent Procedure
        {
            get;
            private set;
        }

        public static ResourceComponent Res
        {
            get;
            private set;
        }

        public static DownloadComponent Download
        {
            get;
            private set;
        }

        public static EventComponent Event
        {
            get;
            private set;
        }

        public static NetworkComponent Network
        {
            get;
            private set;
        }

        public static SceneComponent Scene
        {
            get;
            private set;
        }

        public static SoundComponent Sound
        {
            get;
            private set;
        }

        public static WebRequestComponent WebRequest
        {
            get;
            private set;
        }
        
        public static void InitBuildInComponents()
        {
            Basic = GetComponent<BasicComponent>();
            Fsm = GetComponent<FsmComponent>();
            Procedure = GetComponent<ProcedureComponent>();
            Res = GetComponent<ResourceComponent>();
            Download = GetComponent<DownloadComponent>();
            Event = GetComponent<EventComponent>();
            Network = GetComponent<NetworkComponent>();
            Scene = GetComponent<SceneComponent>();
            Sound = GetComponent<SoundComponent>();
            WebRequest = GetComponent<WebRequestComponent>();
        }
        
        public static void RegistComponent(GameFrameComponentBase comp)
        {
            if (comp == null)
            {
                Log.Debug("comp is null");
                return;
            }

            var type = comp.GetType();
            if (components.ContainsKey(type))
            {
                Log.Error("compType {0} is already exist",type);
                return;
            }
            components.Add(type,comp);
        }

        public static T GetComponent<T>() where T : GameFrameComponentBase
        {
            var type = typeof(T);
            if (components.ContainsKey(type))
                return (T)components[type];
            return null;
        }

        public static void ShutDown(ShutDownType type)
        {
            
        }

    }
}