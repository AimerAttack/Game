using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Core
{
    public partial class CoreEntry
    {
        private static readonly Dictionary<Type, GameFrameComponentBase> components =
            new Dictionary<Type, GameFrameComponentBase>();

        public static MonoBehaviour Coroutine { get; set; }
       
        
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