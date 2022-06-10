using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public class RP_Luminance : BaseRenderPass<Comp_Luminance>
    {
        protected override string _TempTexName
        {
            get { return "RP_Luminance Temp Texture"; }
        }

        protected override string _PostProcessingTag
        {
            get { return "RP_Luminance Tag"; }
        }

        public RP_Luminance(RenderPassEvent @event, Shader shader) : base(@event, shader)
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