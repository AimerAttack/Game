using UnityEngine;

public abstract class STEEffectBase : MonoBehaviour
{
    public bool Opened { get; set; }

    public void Open()
    {
        if (Opened)
            return;
        Opened = true;
        OnOpen();
    }

    public void Close()
    {
        if (!Opened)
            return;
        Opened = false;
        OnClose();
    }

    protected abstract void OnOpen();
    protected abstract void OnClose();
}