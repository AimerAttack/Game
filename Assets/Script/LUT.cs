using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

[Serializable, VolumeComponentMenu("PF/LUT")]
public class LUT : VolumeComponent, IPostProcessComponent
{
    public bool mIsEnable = false;
    public ColorParameter mColor = new ColorParameter(Color.white,true);
    public ClampedFloatParameter mBrightness = new ClampedFloatParameter(1f, 1f, 5f,true);
    public bool IsActive()
    {
        return mIsEnable && active;
    }

    public bool IsTileCompatible()
    {
        return false;
    }
}
