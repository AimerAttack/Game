using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script;
using UnityEngine;
using UnityEngine.Rendering;

public class UISwitch : MonoBehaviour
{
    public VolumeProfile vol;
    public Comp_Luminance _comp;
    public Comp_Luminance _Realcomp;
    public float Duration = 0.3f;

    void Start()
    {
        vol.TryGet(out _Realcomp);
        _comp = UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<Comp_Luminance>();
    }

    void OnGUI()
    {
        var buttonName = _comp.mIsEnable ? "关" : "开";
        if (GUI.Button(new Rect(0, 0, 150, 50), buttonName))
        {
            if (_comp.mIsEnable)
            {
                DOTween.To(() => _Realcomp.Luminance.value, x => _Realcomp.Luminance.value = x,1,Duration)
                    .OnComplete(() => _comp.mIsEnable = false).SetEase(Ease.OutQuart);
            }
            else
            {
                _comp.mIsEnable = true;
                DOTween.To(() => _Realcomp.Luminance.value, x => _Realcomp.Luminance.value = x,0.1f,Duration).SetEase(Ease.OutQuart);
            }
        }
    }
}