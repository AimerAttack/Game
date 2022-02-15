using UnityEngine;

namespace GameFrame.Utility
{
    public static class YieldUtility
    {
        public static readonly YieldInstruction Wait1S = new WaitForSeconds(1);
        public static readonly YieldInstruction WaitForEndOfFrame = new WaitForEndOfFrame();
    }
}