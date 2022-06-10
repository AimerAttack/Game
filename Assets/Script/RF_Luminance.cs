using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public class RF_Luminance : BaseRenderFeature
    {
        public RenderPassEvent Event = RenderPassEvent.AfterRenderingTransparents;
        private RP_Luminance _Pass;
        private Comp_Luminance _Comp;


        protected override void OnCreate()
        {
            _Pass = new RP_Luminance(Event, Shader.Find("Luminance"));
        }

        protected override void OnAddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            var stack = VolumeManager.instance.stack;
            _Comp = stack.GetComponent<Comp_Luminance>();
            if (!_Comp.IsActive())
                return;
            var cameraColorTarget = renderer.cameraColorTarget;
            _Pass.Setup(cameraColorTarget, _Comp);
            renderer.EnqueuePass(_Pass);
        }
    }
}