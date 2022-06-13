using UnityEngine;

[RequireComponent(typeof(STEHolder))]
public abstract class STEEffectBase : MonoBehaviour
{
    public bool IsOpen { get; set; }

    public void Open()
    {
        if (IsOpen)
            return;
        IsOpen = true;
        OnOpen();
    }

    public void Close()
    {
        if (!IsOpen)
            return;
        IsOpen = false;
        OnClose();
    }

    protected abstract void OnOpen();
    protected abstract void OnClose();
}