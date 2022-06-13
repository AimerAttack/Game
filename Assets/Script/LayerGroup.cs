using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NamedTD.Common
{
    public class LayerGroup : MonoBehaviour
    {
        [ShowInInspector]
        private Dictionary<Transform,LayerMask> _StoreLayers = new Dictionary<Transform,LayerMask>();
        public void ChangeLayer(LayerMask layer)
        {
            if(_StoreLayers.Count == 0)
                _Store();
            var children = GetComponentsInChildren<Transform>(true);
            if (children != null)
            {
                foreach (var child in children)
                {
                    child.gameObject.layer = layer;
                }
            }
        }

        void _Store()
        {
            var children = GetComponentsInChildren<Transform>(true);
            if (children != null)
            {
                foreach (var child in children)
                {
                    _StoreLayers[child] = child.gameObject.layer;
                }
            } 
        }

        public void Restore()
        {
            var children = GetComponentsInChildren<Transform>(true);
            if (children != null)
            {
                foreach (var child in children)
                {
                    if(_StoreLayers.ContainsKey(child))
                        child.gameObject.layer = _StoreLayers[child];
                }
            }
            _StoreLayers.Clear();
        }
    }
}