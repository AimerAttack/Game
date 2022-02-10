using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace GameEditor
{
    public partial class GameEditorTool : OdinMenuEditorWindow
    {
        [MenuItem("Tools/工具箱")]
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

            tree.Add("打包资源",new BuildAssetBundle());
            return tree;
        }
    }
    
}