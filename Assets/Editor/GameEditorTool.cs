using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class GameEditorTool : OdinMenuEditorWindow
    {
        [MenuItem("Tools/GameFrame")]
        public static void OpenWindow()
        {
            var window = GetWindow<GameEditorTool>();
            window.titleContent = new GUIContent("工具列表");
            window.Show();
        }
        
        
        protected override OdinMenuTree BuildMenuTree()
        {
            var tree = new OdinMenuTree();
            tree.Selection.SupportsMultiSelect = false;

            tree.Add("设置", GeneralDrawerConfig.Instance);
            tree.Add("Utilities", new TextureUtilityEditor());
            tree.AddAllAssetsAtPath("Odin Settings", "Assets/Plugins/Sirenix", typeof(ScriptableObject), true, true);
            return tree;
        }
    }
    
    public class TextureUtilityEditor
    {
        [BoxGroup("Tool"), HideLabel, EnumToggleButtons]
        public Tool Tool;

        public List<Texture> Textures;

        [Button(ButtonSizes.Large), HideIf("Tool", Tool.Rotate)]
        public void SomeAction() { }

        [Button(ButtonSizes.Large), ShowIf("Tool", Tool.Rotate)]
        public void SomeOtherAction() { }
    }
}