using UnityEngine;

public class STEHolder : MonoBehaviour
{
    public string Layer;
    
    public void Open()
    {
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