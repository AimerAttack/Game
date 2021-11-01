using System;
using System.Collections.Generic;

namespace GameFrame.Logic
{
    public static class UIUtility
    {
        private static Dictionary<Type, EUIOpenType> openTypes = new Dictionary<Type, EUIOpenType>();

        public static EUIOpenType GetOpenType<T>() where T : UIBase
        {
            if (openTypes.TryGetValue(typeof(T), out EUIOpenType val))
                return val;
            return EUIOpenType.Single;
        }
    }
}