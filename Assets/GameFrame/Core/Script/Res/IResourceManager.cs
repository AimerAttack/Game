using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrame.Core
{
    public class IResourceManager
    {
        public virtual void Load(ResourceComponent.Loader loader, Action<ResourceComponent.Loader,object> success,
            Action<ResourceComponent.Loader> failed)
        {
            
        }
    }
}