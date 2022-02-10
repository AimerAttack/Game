using System;
using System.Collections.Generic;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace GameFrame.Core
{
    public class EditorResourceManager : IResourceManager
    {
        private Dictionary<string, object> cache = new Dictionary<string, object>();

        public override void Load(ResourceComponent.Loader loader, Action<ResourceComponent.Loader, object> success, Action<ResourceComponent.Loader> failed)
        {
            if (cache.ContainsKey(loader.path))
            {
                var val = cache[loader.path];
                if (success != null)
                    success(loader, val);
            }
            else
            {
                #if UNITY_EDITOR
                var obj = AssetDatabase.LoadAssetAtPath<UnityEngine.Object>(loader.path);
                if (obj == null)
                {
                    if (failed != null)
                        failed(loader);
                }
                else
                {
                    if (success != null)
                        success(loader, obj);
                }
                #endif
            }
        }
    }
}