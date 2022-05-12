using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ddd : MonoBehaviour
{
    private void Awake()
    {
        var btn = GetComponent<ClickEvent>();
        btn.OnClick.AddListener(OnClick);
        btn.OnDoubleClick.AddListener(OnDoubleClick);
        btn.OnClickContinue.AddListener(OnHold);
    }

    public void OnClick()
    {
        Debug.Log("OnClick");
    }

    public void OnDoubleClick()
    {
        Debug.Log("OnDoubleClick");
    }

    public void OnHold()
    {
        Debug.Log("OnHold");
    }
    
}
