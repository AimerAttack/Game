
using System.Collections.Generic;

public static class STEManager
{
    public delegate void Action();
    private static Dictionary<string,Action> _ActionOpen = new Dictionary<string,Action>();
    private static Dictionary<string,Action> _ActionClose = new Dictionary<string,Action>();
    

    public static void Open(string key)
    {
        if(!_ActionOpen.ContainsKey(key))
            return;
        _ActionOpen[key]();
    }

    public static void Close(string key)
    {
        if(!_ActionClose.ContainsKey(key))
            return;
        _ActionClose[key]();
    }

    static void OnOpenEvent()
    {
    }

    static void OnCloseEvent()
    {
        
    }
    
    public static void WaitOpen(string key,Action action)
    {
        if(!_ActionOpen.ContainsKey(key))
            _ActionOpen.Add(key,OnOpenEvent);
        _ActionOpen[key] += action;
    }

    public static void WaitClose(string key,Action action)
    {
        if(!_ActionClose.ContainsKey(key))
            _ActionClose.Add(key,OnCloseEvent);
        _ActionClose[key] += action;
    }

    public static void RemoveWaitOpen(string key,Action action)
    {
        if (!_ActionOpen.ContainsKey(key))
            return;
        _ActionOpen[key] -= action;
    }

    public static void RemoveWaitClose(string key,Action action)
    {
        if(!_ActionClose.ContainsKey(key))
            return;
        _ActionClose[key] -= action;
    }
}