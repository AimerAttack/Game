using UnityEngine;
using UnityEngine.Rendering;

public class CameraRender
{
    ScriptableRenderContext context;
    private Camera camera;

    private const string bufferName = "Custom Render Camera";
    private CommandBuffer buffer = new CommandBuffer {name = bufferName};

    private CullingResults cullingResults;

    private ShaderTagId unlitShaderTagId = new ShaderTagId("SRPDefaultUnlit");

    private static ShaderTagId[] legacyShaderTagIds =
    {
        new ShaderTagId("Always"),
        new ShaderTagId("ForwardBase"),
        new ShaderTagId("PrepassBase"),
        new ShaderTagId("Vertex"),
        new ShaderTagId("VertexLMRGBM"),
        new ShaderTagId("VertexLM")
    };
    
    public void Render(ScriptableRenderContext context, Camera camera)
    {
        this.context = context;
        this.camera = camera;
        
        if(!Cull())
            return;

        Setup();
        
        DrawVisibleGeometry();

        Submit();
    }

    void Setup()
    {
        context.SetupCameraProperties(camera);
        buffer.ClearRenderTarget(true,true,Color.clear);
        ExcuteBuffer();
    }

    
    void DrawVisibleGeometry()
    {
        var sortingSettings = new SortingSettings(camera){criteria = SortingCriteria.CommonOpaque};
        
        var drawingSettings = new DrawingSettings(unlitShaderTagId,sortingSettings);
        var filteringSettings = new FilteringSettings(RenderQueueRange.opaque);
        
        context.DrawRenderers(cullingResults,ref drawingSettings,ref filteringSettings);
        
        context.DrawSkybox(camera);

        sortingSettings.criteria = SortingCriteria.CommonTransparent;
        drawingSettings.sortingSettings = sortingSettings;
        filteringSettings.renderQueueRange = RenderQueueRange.transparent;
        
        context.DrawRenderers(cullingResults,ref drawingSettings,ref filteringSettings);
    }

    void ExcuteBuffer()
    {
        context.ExecuteCommandBuffer(buffer);
        buffer.Clear();
    }

    void Submit()
    {
        ExcuteBuffer();
        context.Submit();
    }

    bool Cull()
    {
        if (camera.TryGetCullingParameters(out ScriptableCullingParameters p))
        {
            cullingResults = context.Cull(ref p);
            return true;
        }

        return false;
    }
}