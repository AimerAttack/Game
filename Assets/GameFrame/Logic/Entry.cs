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
        private void Awake()
        {
            DontDestroyOnLoad(this);

            CoreEntry.Coroutine = this;
            
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
        public static UIManager UI { get; private set; }
        public static TipsComponent Tips { get; private set; }


        public static MonoBehaviour Coroutine
        {
            get
            {
                return CoreEntry.Coroutine;
            }
        }


        public static void InitBuildInComponents()
        {
            Basic = new BasicComponent();
            Fsm = new FsmComponent();
            Procedure = new ProcedureComponent();
            Event = new EventComponent();
            Res = new ResourceComponent();
            Download = new DownloadComponent();
            Network = new NetworkComponent();
            Sound = new SoundComponent();
            WebRequest = new WebRequestComponent();
        }

        void InitCustomComponents()
        {
            Http = new HttpComponent();
            UI = new UIManager();
            Tips = new TipsComponent();
        }

        private void Update()
        {
            CoreEntry.Update(Time.deltaTime, Time.unscaledDeltaTime);
        }

        void Start()
        {
            List<GameFrameProcedureBase> procedures = new List<GameFrameProcedureBase>();
            
            procedures.Add(new ProcedureInit());
            procedures.Add(new ProcedureLogin());
            procedures.Add(new ProcedureCheckUpdte());
            procedures.Add(new ProcedureEnterMainScene());
            procedures.Add(new ProcedureGetServerList());
            procedures.Add(new ProcedureUpdateClient());
            procedures.Add(new ProcedureUpdateRes());
            
            Procedure.StartLogic(procedures);
        }
    }
}