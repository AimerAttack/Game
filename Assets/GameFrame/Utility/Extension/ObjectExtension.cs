using UnityEngine;

namespace GameFrame.Utility.Extension
{
    public static class ObjectExtension
    {
        public static T AddOrGet<T>(this GameObject obj) where T : Component
        {
            var result = obj.GetComponent<T>();
            if (result == null)
                result = obj.AddComponent<T>();
            return result;
        }
        
        public static T AddOrGet<T>(this Component comp) where T : Component
        {
            return comp.gameObject.AddOrGet<T>();
        }
    }
}