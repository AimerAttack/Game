using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public class RenderFeatureLuminance : BaseRenderFeature
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
        private RenderPassLuminance _Pass;
        private VolumeCompLuminance _VolumeComp;


        protected override void OnCreate()
        {
            _Pass = new RenderPassLuminance(Event, Shader.Find("Luminance"));
        }

        protected override void OnAddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var stack = VolumeManager.instance.stack;
            _VolumeComp = stack.GetComponent<VolumeCompLuminance>();
            if (!_VolumeComp.IsActive())
                return;
            var cameraColorTarget = renderer.cameraColorTarget;
            _Pass.Setup(cameraColorTarget, _VolumeComp);
            renderer.EnqueuePass(_Pass);
        }
    }
    
    [Serializable, VolumeComponentMenu("STE/Luminance")]
    public class VolumeCompLuminance : VolumeComponent,IPostProcessComponent
    {
        public bool mIsEnable = false;
        public ClampedFloatParameter Luminance = new ClampedFloatParameter(1f, 0.01f, 1f,true);
        public bool IsActive()
        {
            return mIsEnable && active;
        }

        public bool IsTileCompatible()
        {
            return false;
        }
    }
    
    public class RenderPassLuminance : BaseRenderPass<VolumeCompLuminance>
    {
        protected override string _TempTexName
        {
            get { return "RP_Luminance Temp Texture"; }
        }

        protected override string _PostProcessingTag
        {
            get { return "RP_Luminance Tag"; }
        }

        public RenderPassLuminance(RenderPassEvent @event, Shader shader) : base(@event, shader)
        {
        }

        protected override void OnExecute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            var cmd = CommandBufferPool.Get(_PostProcessingTag);
            RenderImage(cmd, ref renderingData);
            context.ExecuteCommandBuffer(cmd);
            CommandBufferPool.Release(cmd);
        }

        protected override void RenderImage(CommandBuffer cmd, ref RenderingData renderingData)
        {
            if (_Mat == null)
            {
                Debug.LogError("mat is null");
                return;
            }

            _Mat.SetFloat("_Luminance",_Comp.Luminance.value);
            
            RenderTextureDescriptor desc = renderingData.cameraData.cameraTargetDescriptor;
            cmd.GetTemporaryRT(_TempTexHandle.id,desc,mFilterMode);
            Blit(cmd, _SourceRTIdentifier, _TempTexHandle.Identifier(), _Mat, 0);
            Blit(cmd, _TempTexHandle.Identifier(), _SourceRTIdentifier);
        }
    }
}