using System.Reflection;
using GameFrame.Logic;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    [InitializeOnLoad]
    public class CustomHierarchyEditor
    {
        static Texture2D s_redPointIcon;

        static CustomHierarchyEditor()
        {
            if (!Application.isPlaying)
            {
                EditorApplication.hierarchyWindowItemOnGUI += OnHierarchyWindowItemOnGUI;
                s_redPointIcon = (Texture2D) AssetDatabase.LoadMainAssetAtPath("Assets/Gizmos/RedPoint_Icon.png");
            }
        }
        
        private static void OnHierarchyWindowItemOnGUI(int instanceId, Rect selectionRect)
        {
            var gameObject = EditorUtility.InstanceIDToObject(instanceId) as GameObject;
            if (gameObject == null)
                return;

            HandleRedPoint(gameObject, selectionRect);
        }
        
        private static void HandleRedPoint(GameObject go, Rect selectionRect)
        {
            var component = go.GetComponent<RedPoint>();
            if (component != null)
            {
                Rect r = new Rect(selectionRect);
                r.x = r.width - 15;
                r.width = 15;
                GUI.Label(r, s_redPointIcon);
                r.x += 15;
                r.width = 200;
                var field = typeof(RedPoint).GetField("FullName", BindingFlags.Instance | BindingFlags.NonPublic);
                if (field != null)
                {
                    var name = field.GetValue(component) as string;
                    GUI.Label(r, string.IsNullOrEmpty(name) ? "<Unknown>" : name);
                }
            }
        }

    }
}

