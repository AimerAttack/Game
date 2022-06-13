using System;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Script
{
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
}