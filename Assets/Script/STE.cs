using System;
using System.Collections.Generic;
using UnityEngine;

public class STE : MonoBehaviour
{
    public static STE Instance { get; private set; }
    
    Dictionary<string,STEHolder> _Holders = new Dictionary<string,STEHolder>();
    private void Awake()
    {
        Instance = this;
        
        var holders = GetComponentsInChildren<STEHolder>();
        if (holders != null)
        {
            foreach (var holder in holders)
            {
                _Holders[holder.Layer] = holder;
            }
        }
    }

    public void Open(string layer)
    {
        if(_Holders.ContainsKey(layer))
            _Holders[layer].Open();
    }

    public void Close(string layer)
    {
        if(_Holders.ContainsKey(layer))
            _Holders[layer].Close();
    }

    public bool IsOpen(string layer)
    {
        if (_Holders.ContainsKey(layer))
            return _Holders[layer].IsOpen;
        return false;
    }
}