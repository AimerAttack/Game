using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Script;
using UnityEngine;
using UnityEngine.Rendering;

public class UISwitch : MonoBehaviour
{
    private bool Effected = false;

    void OnGUI()
    {
        var buttonName = Effected ? "关" : "开";
        if (GUI.Button(new Rect(0, 0, 150, 50), buttonName))
        {
            if (Effected)
            {
                GetComponent<STEHolder>().Close();
            }
            else
            {
                GetComponent<STEHolder>().Open();
            }
            Effected = !Effected;
        }

    }
}