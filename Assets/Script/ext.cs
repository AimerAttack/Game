using UnityEngine;

namespace NamedTD.Common
{
    public static class GameObjectExtension
    {
        public static T GetOrAddComponent<T>(this GameObject go) where T : Component
        {
            if (go == null) return null;
            T comp = go.GetComponent<T>();
            if (!comp)
                comp = go.AddComponent<T>();
            return comp;
        }
        
        public static T GetOrAddComponent<T>(this Component go) where T : Component
        {
            if (go == null) return null;
            T comp = go.GetComponent<T>();
            if (!comp)
                comp = go.gameObject.AddComponent<T>();
            return comp;
        }

        public static void ClearChildren(this GameObject go)
        {
            var trans = go.transform;
            trans.ClearChildren();
        }
        
        public static void ClearChildren(this Transform trans)
        {
            while (trans.childCount > 0)
            {
                var child = trans.GetChild(0);
                child.parent = null;
                GameObject.Destroy(child.gameObject);
            }
        }

        public static void ChangeLayer(this GameObject go,LayerMask layer)
        {
            var group = go.GetOrAddComponent<LayerGroup>();
            group.ChangeLayer(layer);
        }

        public static void RestoreLayer(this GameObject go)
        {
            var group = go.GetComponent<LayerGroup>();   
            if(group != null)
                group.Restore();
        }
    }
}