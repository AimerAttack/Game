using System.Collections.Generic;

namespace GameFrame.Logic
{
    public class UIGroup
    {
        private SortedList<ulong, UIBase> forms = new SortedList<ulong, UIBase>();

        public void AddForm(UIBase form)
        {
            form.group = this;
            forms.Add(form.SortId,form);
        }

        public void RefreshDepth()
        {
            
        }

        public void RemoveForm(UIBase form)
        {
            if(form == null)
                return;
            forms.Remove(form.SortId);
        }
    }
}