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