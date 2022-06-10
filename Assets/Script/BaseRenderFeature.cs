using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
    public abstract class BaseRenderFeature : ScriptableRendererFeature
    {
        sealed public override void Create()
        {
            OnCreate();
        }

        sealed public override void AddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData)
        {
            OnAddRenderPasses(renderer, ref renderingData);
        }

        protected abstract void OnCreate();
        protected abstract void OnAddRenderPasses(ScriptableRenderer renderer, ref RenderingData renderingData);
    }
}