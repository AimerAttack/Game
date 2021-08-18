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
            CoreEntry.InitBuildInComponents();
            InitCustomComponents();
        }


        public static HttpComponent Http
        {
            get;
            private set;
        }
        
        void InitCustomComponents()
        {
            Http = CoreEntry.GetComponent<HttpComponent>();
        }
    }
}