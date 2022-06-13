using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script;
using UnityEngine;
using UnityEngine.Rendering;

public class UISwitch : MonoBehaviour
{
    public string Layer;

    void OnGUI()
    {
        var buttonName = STE.Instance.IsOpen(Layer) ? "关" : "开";
        if (GUI.Button(new Rect(0, 0, 150, 50), buttonName))
        {
            if (STE.Instance.IsOpen(Layer))
            {
                STE.Instance.Close(Layer);
            }
            else
            {
                STE.Instance.Open(Layer);
            }
        }

    }
}