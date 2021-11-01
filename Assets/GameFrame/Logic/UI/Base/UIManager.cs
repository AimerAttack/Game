using System;
using System.Collections.Generic;
using GameFrame.Core;
using UnityEngine;
using UnityEngine.UIElements;

namespace GameFrame.Logic
{
    public class UIManager : GameFrameComponentBase
    {
        private ulong SortId = 0;


        private Dictionary<Type, Dictionary<ulong, UIBase>> loadingUI =
            new Dictionary<Type, Dictionary<ulong, UIBase>>();

        private HashSet<ulong> waitReleased = new HashSet<ulong>();

        private Dictionary<Type, UIBase> poolUI = new Dictionary<Type, UIBase>();

        private Dictionary<EUILayer, UIGroup> groups = new Dictionary<EUILayer, UIGroup>();

        protected override void OnAwake()
        {
            base.OnAwake();
        }


        public void AddUIGroup(EUILayer type)
        {
            if(groups.ContainsKey(type))
                return;
            var group = new UIGroup();
            groups[type] = group;
        }

        UIGroup GetUIGroup(EUILayer type)
        {
            if (groups.TryGetValue(type, out UIGroup group))
                return group;
            return null;
        }

        public void Open<T>(object param = null) where T : UIBase,new()
        {
            var type = typeof(T);
            var openType = UIUtility.GetOpenType<T>();
            switch (openType)
            {
                case EUIOpenType.Single:
                {
                    var existUI = GetUI<T>();
                    //有生效中的
                    if (existUI != null && existUI.IsAvaliable)
                    {
                        return;
                    }
                    //有加载中的
                    if(loadingUI.ContainsKey(type))
                        return;
                    Load<T>(param);
                }
                    break;
                case EUIOpenType.Multi:
                {
                    Load<T>(param);
                }
                    break;
                default:
                {
                    throw new GameFrameException($"error openType:{StackTraceUtility.ExtractStackTrace()}");
                }
                    break;
            }
        }

        void Load<T>(object param) where T : UIBase,new()
        {
            var type = typeof(T);
            var logic = new T();
            logic.SortId = SortId++;
            if (!loadingUI.ContainsKey(type))
            {
                loadingUI.Add(type, new Dictionary<ulong, UIBase>());
            }

            loadingUI[type][logic.SortId] = logic;
            var loadParam = new UILoadParam(logic, param);
            Entry.Res.LoadAsset(logic.AssetPath,loadParam,OnLoadSuccess,OnLoadFailed);
        }

        void OnLoadSuccess(object asset,object param)
        {
            var p = param as UILoadParam;
            RemoveLoadingInfo(p);
        }

        void OnLoadFailed(object param)
        {
            var p = param as UILoadParam;
            RemoveLoadingInfo(p);
        }

        void RemoveLoadingInfo(UILoadParam p)
        {
            if (loadingUI.TryGetValue(p.type, out Dictionary<ulong, UIBase> dic))
            {
                dic.Remove(p.logic.SortId);
            }
        }

        public bool IsLoading(UIBase ui)
        {
            if (loadingUI.TryGetValue(ui.GetType(), out Dictionary<ulong, UIBase> dic))
            {
                if (dic.ContainsKey(ui.SortId))
                    return true;
            }
            return false;
        }

        public void Close(UIBase ui)
        {
            if (IsLoading(ui))
            {
                waitReleased.Add(ui.SortId);
            }
            else
            {
                var form = GetUI(ui.GetType(), ui.SortId);
                if (form != null)
                {
                    form.InternalClose();
                }
            }
        }

        UIBase GetUI(Type type, ulong sortId)
        {
            return null;
        }
        
        T GetUI<T>() where T : UIBase
        {
            return null;
        }

        class UILoadParam
        {
            public object param;
            public UIBase logic;

            public Type type
            {
                get
                {
                    return logic.GetType();
                }
            }

            public UILoadParam(UIBase _logic,object _param)
            {
                logic = _logic;
                param = _param;
            }
        }
    }
}