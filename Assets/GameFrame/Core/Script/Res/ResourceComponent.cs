using System;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace GameFrame.Core
{
    public partial class ResourceComponent : GameFrameComponentBase
    {
        private IResourceManager _manager;

        private IResourceManager manager
        {
            get
            {
                if (_manager == null)
                {
#if UNITY_EDITOR
                    _manager = new EditorResourceManager();
#else
                    _manager = new RuntimeResourceManager();
#endif
                }

                return _manager;
            }
        }

        public void LoadAsset(string path, object param, Action<string,object, object> successCallback,
            Action<string,object> failedCallback)
        {
            Load(new Loader(path,param,successCallback,failedCallback));
        }

        void Load(Loader loader)
        {
            manager.Load(loader,OnLoadSuccess,OnLoadFailed);
        }

        void OnLoadSuccess(Loader loader,object asset)
        {
            if (asset == null)
            {
                Log.Fatal($"load asset is null,path={loader.path}");
                return;
            }

            if (loader.successCallback != null)
            {
                loader.successCallback(loader.path, asset, loader.param);
            }
        }

        void OnLoadFailed(Loader loader)
        {
            
        }
    }
}