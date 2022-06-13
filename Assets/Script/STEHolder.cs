using UnityEngine;

[RequireComponent(typeof(Camera))]
public class STEHolder : MonoBehaviour
{
    public bool IsOpen { get; private set; }
    
    public string Layer;
    
    public void Open()
    {
        IsOpen = true;
        STEManager.Open(Layer);
        var effects = GetComponents<STEEffectBase>();
        if (effects != null)
        {
            foreach (var effect in effects)
            {
                effect.Open();
            }
        }
    }

    public void Close()
    {
        IsOpen = false;
        STEManager.Close(Layer);
        var effects = GetComponents<STEEffectBase>();
        if (effects != null)
        {
            foreach (var effect in effects)
            {
                effect.Close();
            }
        } 
    }
}