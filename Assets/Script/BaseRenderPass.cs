using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public abstract class BaseRenderPass<T> : ScriptableRenderPass where T : VolumeComponent
    {
        protected abstract string _TempTexName{get;}
        protected abstract string _PostProcessingTag{get;}

        protected FilterMode mFilterMode = FilterMode.Bilinear; 
        protected RenderTargetHandle _TempTexHandle;
        protected Material _Mat;
        protected RenderTargetIdentifier _SourceRTIdentifier;
        protected T _Comp;
        
        public BaseRenderPass(RenderPassEvent @event, Shader shader)
        {
            this.renderPassEvent = @event;
            _TempTexHandle.Init(_TempTexName);
            _Mat = CoreUtils.CreateEngineMaterial(shader);
        }
        
        sealed public override void Execute(ScriptableRenderContext context, ref RenderingData renderingData)
        {
            OnExecute(context, ref renderingData);   
        }

        protected abstract void OnExecute(ScriptableRenderContext context, ref RenderingData renderingData);

        public void Setup(RenderTargetIdentifier sourceRT, T comp)
        {
            _SourceRTIdentifier = sourceRT;
            _Comp = comp;
        }

        protected abstract void RenderImage(CommandBuffer cmd, ref RenderingData renderingData);
       

        public override void FrameCleanup(CommandBuffer cmd)
        {
            cmd.ReleaseTemporaryRT(_TempTexHandle.id);
        }
    }
}