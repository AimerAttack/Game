using System;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using Google.Protobuf.WellKnownTypes;
using Script;
using UnityEngine.Rendering;

public class STELuminance : STEEffectBase
{
    public VolumeProfile vol;
    public float Duration = 0.3f;

    private VolumeCompLuminance _SystemVolumeComp;
    private VolumeCompLuminance _ControlVolumeComp;
    private TweenerCore<float, float,FloatOptions> _Tween;


    private void Awake()
    {
        vol.TryGet(out _ControlVolumeComp);
        _SystemVolumeComp = UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<VolumeCompLuminance>();
    }

    protected override void OnOpen()
    {
        _SystemVolumeComp.mIsEnable = true;
        if(_Tween != null)
            _Tween.Kill(false);
        _Tween = DOTween.To(() => _ControlVolumeComp.Luminance.value, x => _ControlVolumeComp.Luminance.value = x,0.1f,Duration).SetEase(Ease.OutQuart);
    }

    protected override void OnClose()
    {
        if(_Tween != null)
            _Tween.Kill(false);
        _Tween = DOTween.To(() => _ControlVolumeComp.Luminance.value, x => _ControlVolumeComp.Luminance.value = x,1,Duration)
            .OnComplete(() => _SystemVolumeComp.mIsEnable = false).SetEase(Ease.OutQuart);
    }
}