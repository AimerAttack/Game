using System;
using System.Collections;
using System.Collections.Generic;
using GameFrame.Core;
using UnityEngine;

namespace GameFrame.Logic
{
    [DisallowMultipleComponent]
    public partial class Entry : MonoBehaviour
    {
        private void Start()
        {
            InitBuildInComponents();
            InitCustomComponents();
        }

        public static BasicComponent Basic { get; private set; }

        public static FsmComponent Fsm { get; private set; }

        public static ProcedureComponent Procedure { get; private set; }

        public static ResourceComponent Res { get; private set; }

        public static DownloadComponent Download { get; private set; }

        public static EventComponent Event { get; private set; }

        public static NetworkComponent Network { get; private set; }

        public static SoundComponent Sound { get; private set; }

        public static WebRequestComponent WebRequest { get; private set; }


        public static HttpComponent Http { get; private set; }

        public static void InitBuildInComponents()
        {
            Basic = GameFrame.Core.CoreEntry.GetComponent<BasicComponent>();
            Fsm = GameFrame.Core.CoreEntry.GetComponent<FsmComponent>();
            Procedure = GameFrame.Core.CoreEntry.GetComponent<ProcedureComponent>();
            Event = GameFrame.Core.CoreEntry.GetComponent<EventComponent>();
            Res = GameFrame.Core.CoreEntry.GetComponent<ResourceComponent>();
            Download = GameFrame.Core.CoreEntry.GetComponent<DownloadComponent>();
            Network = GameFrame.Core.CoreEntry.GetComponent<NetworkComponent>();
            Sound = GameFrame.Core.CoreEntry.GetComponent<SoundComponent>();
            WebRequest = GameFrame.Core.CoreEntry.GetComponent<WebRequestComponent>();
        }

        void InitCustomComponents()
        {
            Http = GameFrame.Core.CoreEntry.GetComponent<HttpComponent>();
        }
    }
}