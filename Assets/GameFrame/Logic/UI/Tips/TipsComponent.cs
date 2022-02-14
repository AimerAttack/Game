using System;
using GameFrame.Core;

namespace GameFrame.Logic
{
    public class TipsComponent : GameFrameComponentBase
    {
        public void Tip(string msg,string title = "",Action callback = null)
        {
            
        }

        public void Confirm(string msg, string title, string btnSureTxt, Action sureCallback, string btnCancelTxt,
            Action cancelCallback = null)
        {
            
        }
    }
}