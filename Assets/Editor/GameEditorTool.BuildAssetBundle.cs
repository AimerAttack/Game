using System.IO;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEditor.Build.Content;
using UnityEditor.Build.Pipeline;
using UnityEditor.Build.Pipeline.Interfaces;
using UnityEngine;
using UnityEngine.Build.Pipeline;
using BuildCompression = UnityEngine.BuildCompression;

namespace GameEditor
{
    public partial class GameEditorTool
    {
        public class BuildAssetBundle
        {
            [Button]
            void Build()
            {
                var outputPath = "/Users/attack/Documents/Game/Game/Assets/output";
                var buildTarget = BuildTarget.StandaloneOSX;
                var buildGroup = BuildPipeline.GetBuildTargetGroup(buildTarget);

                var buildContent = new BundleBuildContent(ContentBuildInterface.GenerateAssetBundleBuilds());
                var buildParams = new BundleBuildParameters(buildTarget, buildGroup, outputPath);
                buildParams.BundleCompression = BuildCompression.LZ4;

                IBundleBuildResults results;
                ReturnCode exitCode = ContentPipeline.BuildAssetBundles(buildParams, buildContent, out results);
                Debug.Log(exitCode.ToString());
                var manifest = ScriptableObject.CreateInstance<CompatibilityAssetBundleManifest>();
                manifest.SetResults(results.BundleInfos);
                File.WriteAllText(
                    buildParams.GetOutputFilePathForIdentifier(Path.GetFileName(outputPath)) + ".manifest",
                    manifest.ToString());
                AssetDatabase.Refresh();
            }
        }
    }
}