using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class LUTRenderPass : ScriptableRenderPass
{
    private const string mPostProcessingTag = "Render LUT Effects";
    private const string mTempTexName = "Render LUT Temp Texture";

    private RenderTargetHandle mTempTex_Handle;
    private Material mMat;
    private LUT mLUT;
    private FilterMode mFilterMode = FilterMode.Bilinear;


    private RenderTargetIdentifier mSourceRT_Identifier;

    public LUTRenderPass(RenderPassEvent @event, Shader shader)
    {
        this.renderPassEvent = @event;
        mTempTex_Handle.Init(mTempTexName);
        mMat = CoreUtils.CreateEngineMaterial(shader);
    }


    public void Setup(in RenderTargetIdentifier sourceRT,LUT lut)
    {
        mSourceRT_Identifier = sourceRT;
        mLUT = lut;
    }


    public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
    {
        var cmd = CommandBufferPool.Get(mPostProcessingTag);
        RenderImage(cmd, ref renderingData);
        context.ExecuteCommandBuffer(cmd);
        CommandBufferPool.Release(cmd);
    }

    private void RenderImage(CommandBuffer cmd,ref RenderingData renderingData)
    {
        if (mMat == null)
        {
            Debug.LogError("LUTRenterPass mMat can not be null!");
            return;
        }
            
        mMat.SetColor("_Color", mLUT.mColor.value);
        mMat.SetFloat("_Brightness", mLUT.mBrightness.value);

        RenderTextureDescriptor opaqueDesc = renderingData.cameraData.cameraTargetDescriptor;
        //获取临时RT
        cmd.GetTemporaryRT(mTempTex_Handle.id, opaqueDesc, mFilterMode);
        //将当前相机的RT经过处理后,存入临时RT
        Blit(cmd, mSourceRT_Identifier, mTempTex_Handle.Identifier(), mMat, 0);
        //将处理后的RT赋值给相机RT
        Blit(cmd, mTempTex_Handle.Identifier(), mSourceRT_Identifier);
    }

    public override void FrameCleanup(CommandBuffer cmd)
    {
        //释放临时RT
        cmd.ReleaseTemporaryRT(mTempTex_Handle.id);
    }

}
