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
}