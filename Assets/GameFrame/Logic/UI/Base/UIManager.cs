using System;
using System.Collections.Generic;
using GameFrame.Core;
using UnityEngine;
using UnityEngine.UIElements;
using Object = UnityEngine.Object;

namespace GameFrame.Logic
{
    public class UIManager : GameFrameComponentBase
    {
        [Serializable]
        public class GroupConfig
        {
            public EUILayer layer;
            public int sortingOrder;
        }
        
        
        public RectTransform root;

        [SerializeField] private List<GroupConfig> groupConfigs = new List<GroupConfig>();

        
        private ulong SerialId = 0;

        private Dictionary<EUILayer, UIGroup> groups = new Dictionary<EUILayer, UIGroup>();
        
        private Dictionary<Type, UIBase> openedUI = new Dictionary<Type, UIBase>();
        private HashSet<Type> loadingUI = new HashSet<Type>();
        private HashSet<ulong> releasedUI = new HashSet<ulong>();

        private Dictionary<Type, UIBase> poolUI = new Dictionary<Type, UIBase>();

        
       
        
        protected override void OnAwake()
        {
            base.OnAwake();
            
            InitGroups();
        }

        void InitGroups()
        {
            for (int i = 0; i < groupConfigs.Count; i++)
            {
                AddUIGroup(groupConfigs[i].layer,groupConfigs[i].sortingOrder);
            }
        }

        void AddUIGroup(EUILayer type,int sortingOrder)
        {
            if (groups.ContainsKey(type))
                return;
            var group = new UIGroup(type.ToString());
            groups[type] = group;
            
            group.transform.SetParent(root,false);
            group.transform.anchorMin = Vector2.zero;
            group.transform.anchorMax = Vector2.one;
            group.transform.anchoredPosition = Vector2.zero;
        }

        UIGroup GetUIGroup(EUILayer type)
        {
            if (groups.TryGetValue(type, out UIGroup group))
                return group;
            return null;
        }

        public void Open<T>(object param = null) where T : UIBase, new()
        {
            var type = typeof(T);

            var existUI = GetUI<T>();
            //有生效中的
            if (existUI != null && existUI.IsAvaliable)
            {
                return;
            }

            //有加载中的
            if (loadingUI.Contains(type))
                return;
            Load<T>(param);
        }

        void Load<T>(object param) where T : UIBase, new()
        {
            var type = typeof(T);
            var logic = new T();
            logic.SerialId = SerialId++;
            logic.openParam = param;
            loadingUI.Add(type);

            Entry.Res.LoadAsset(logic.AssetPath, logic, OnLoadSuccess, OnLoadFailed);
        }

        void OnLoadSuccess(string path, object asset, object param)
        {
            var form = param as UIBase;
            RemoveLoadingInfo(form);
            if (releasedUI.Contains(form.SerialId))
            {
                releasedUI.Remove(form.SerialId);
                return;
            }

            CreateForm(asset, form);
        }

        void CreateForm(object asset, UIBase form)
        {
            var obj = GameObject.Instantiate((UnityEngine.Object) asset) as GameObject;
            form.BindObj(obj);
        }

        void OnLoadFailed(string path, object param)
        {
            var form = param as UIBase;
            RemoveLoadingInfo(form);
            if (releasedUI.Contains(form.SerialId))
                releasedUI.Remove(form.SerialId);
        }

        void RemoveLoadingInfo(UIBase form)
        {
            loadingUI.Remove(form.GetType());
        }

        public bool IsLoading(UIBase ui)
        {
            return loadingUI.Contains(ui.GetType());
        }

        public void Close(UIBase ui)
        {
            if (IsLoading(ui))
            {
                releasedUI.Add(ui.SerialId);
            }
            else
            {
                var form = GetUI(ui.GetType(), ui.SerialId);
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
            var type = typeof(T);
            if (openedUI.TryGetValue(type, out UIBase ui))
            {
                return ui as T;
            }
            return null;
        }
    }
}