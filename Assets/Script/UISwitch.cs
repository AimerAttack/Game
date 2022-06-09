using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour
{
    private LUT mLUT;
    void Start()
    {
        mLUT = UnityEngine.Rendering.VolumeManager.instance.stack.GetComponent<LUT>();
    }

    void OnGUI()
    {
        var buttonName = mLUT.mIsEnable ? "关" : "开";
        if (GUI.Button(new Rect(0,0,150,50), buttonName))
        {
            mLUT.mIsEnable = !mLUT.mIsEnable;
        }
    }
}
