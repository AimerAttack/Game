using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LUTRenderFeature : ScriptableRendererFeature
{
    public RenderPassEvent mEvent = RenderPassEvent.AfterRenderingTransparents;
    
    private LUTRenderPass mPass;

    private LUT mLUT;

    public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
    {
        var stack = VolumeManager.instance.stack;
        mLUT = stack.GetComponent<LUT>();
        if (!mLUT.IsActive()) //控制开关
            return;

        var cameraColorTarget = renderer.cameraColorTarget;
        //设置当前需要后期的画面
        mPass.Setup(cameraColorTarget, mLUT);
        //添加到渲染列表
        renderer.EnqueuePass(mPass);
    }

    public override void Create()
    {
        mPass = new LUTRenderPass(mEvent, Shader.Find("Custom/LUT"));
    }
}
