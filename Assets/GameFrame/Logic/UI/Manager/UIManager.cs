using System;
using System.Collections.Generic;
using GameFrame.Core;

namespace GameFrame.Logic
{
    public class UIManager : GameFrameComponentBase
    {
        private ulong SortId = 0;
        
        private SortedDictionary<Type, UIBase> opendUI = new SortedDictionary<Type, UIBase>();
        private SortedDictionary<Type, UIBase> loadingUI = new SortedDictionary<Type, UIBase>();


        public void Open<T>(object userData = null) where T : UIBase,new()
        {
            var type = typeof(T);
            if(loadingUI.ContainsKey(type))
                return;
            if(opendUI.ContainsKey(type))
                return;
            var logic = new T();
        }
    }
}