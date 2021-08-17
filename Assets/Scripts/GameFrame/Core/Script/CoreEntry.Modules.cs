using System;
using System.Collections.Generic;

namespace GameFrame.Core
{
    public partial class CoreEntry
    {
        private static readonly LinkedList<GameFrameModule> modules = new LinkedList<GameFrameModule>();
        private static readonly HashSet<Type> moduleTypes = new HashSet<Type>();

        public static T AddModule<T>() where T : GameFrameModule
        {
            var type = typeof(T);
            if (moduleTypes.Contains(type))
            {
                throw new GameFrameException(Utility.Text.Format("module {0} already exist",type));
                return null;
            }
            
            GameFrameModule module = (GameFrameModule)Activator.CreateInstance(type);

            var current = modules.First;
            while (current != null)
            {
                if (module.Priority > current.Value.Priority)
                {
                    break;
                }

                current = current.Next;
            }

            if (current != null)
            {
                modules.AddBefore(current, module);
            }
            else
            {
                modules.AddLast(module);
            }

            moduleTypes.Add(type);
            return module as T;
        }

        public static T GetModule<T>() where T : GameFrameModule
        {
            var type = typeof(T);
            if (moduleTypes.Contains(type))
            {
                foreach (var module in modules)
                {
                    if (module.GetType() == type)
                    {
                        return module as T;
                    }
                }
            }

            return null;
        }
        
        public static void Update(float elapseSeconds, float realElapseSeconds)
        {
            foreach (var frameModule in modules)
            {
                frameModule.Update(elapseSeconds,realElapseSeconds);
            }
        }
    }
}