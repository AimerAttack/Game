using System.Collections.Generic;
using UnityEngine;

namespace GameFrame.Logic
{
    public class UIGroup
    {
        private SortedList<ulong, UIBase> forms = new SortedList<ulong, UIBase>();

        public RectTransform transform
        {
            private set;
            get;
        }
        
        public string GroupName
        {
            private set;
            get;
        }

        public UIGroup(string groupName)
        {
            GameObject go = new GameObject();
            transform = go.AddComponent<RectTransform>();
            GroupName = groupName;
        }
        
        public void AddForm(UIBase form)
        {
            form.group = this;
            forms.Add(form.SerialId,form);
        }

        public void RefreshDepth()
        {
            
        }

        public void RemoveForm(UIBase form)
        {
            if(form == null)
                return;
            forms.Remove(form.SerialId);
        }
    }
}