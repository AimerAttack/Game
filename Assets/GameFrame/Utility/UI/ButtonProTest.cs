using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameFrame.Utility.UI
{
    public class ButtonProTest : MonoBehaviour
    {
        private void Awake()
        {
            var script = GetComponent<ButtonPro>();
            script.OnDrop += ((obj) =>
            {
                if (obj != null && obj.name.StartsWith("Grid_"))
                {
                    script.transform.SetParent(obj.transform,false);
                    script.transform.localPosition = Vector3.zero;
                    return true;
                }
                return false;
            });
            script.OnClick.AddListener(delegate
            {
            });
        }
    }
}