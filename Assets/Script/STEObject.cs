using System;
using NamedTD.Common;
using UnityEngine;

public class STEObject : MonoBehaviour
{
    public string Layer;
    private void Awake()
    {
        STEManager.WaitOpen(Layer,OnOpenEvent);
        STEManager.WaitClose(Layer,OnCloseEvent);
        
        //Awake的时候对应特效已经开启了，执行一次OpenEvent
        if(STE.Instance.IsOpen(Layer))
            OnOpenEvent();
    }

    private void OnDestroy()
    {
        STEManager.RemoveWaitOpen(Layer,OnOpenEvent);
        STEManager.RemoveWaitClose(Layer,OnCloseEvent);
    }

    void OnOpenEvent()
    {
        gameObject.ChangeLayer(LayerMask.NameToLayer(Layer));
    }

    void OnCloseEvent()
    {
        gameObject.RestoreLayer();
    }
}