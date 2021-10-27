using System;
using UnityEngine;

namespace GameFrame.Logic
{
    public abstract class UIBase : IComparable<UIBase>
    {
        public ulong SortId { get; set; }
        public abstract string AssetPath { get;}
        public abstract EUILayer UILayer { get; }

        private bool inited = false;
        public GameObject gameObject { get; private set; }

        private UIObjHolder m_holder;
        
        public T GetHolder<T>() where T : UIObjHolder
        {
            return m_holder as T;
        }
        
        public int CompareTo(UIBase other)
        {
            return SortId.CompareTo(other.SortId);
        }

        public void BindObj(GameObject obj)
        {
            gameObject = obj;
            m_holder = obj.GetComponent<UIObjHolder>();
        }
        
        public void InternalOpen()
        {
            if (!inited)
            {
                OnRegistEvent();
                inited = true;
            }

            OnOpen();
        }

        protected virtual void OnRegistEvent()
        {
        }

        protected virtual void OnOpen()
        {
            
        }

        public void InternalClose()
        {
            OnClose();
        }

        protected virtual void OnClose()
        {
            
        }
    }

}