using UnityEngine;
using UnityEngine.EventSystems;

namespace GameFrame.Utility.Extension
{
    public static class PointerEventDataEX 
    {
        public static Vector2 GETPointerBy(this PointerEventData who, RectTransform ParentUI)
        {
            Vector2 _pos= who.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentUI, who.position, who.pressEventCamera, out _pos);

            return _pos;
        }
        public static Vector2 GETDeltPointerBy(this PointerEventData who, RectTransform ParentUI)
        {
            Vector2 _pos = who.position;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(ParentUI, who.delta, who.pressEventCamera, out _pos);

            return _pos;
        }
    }
}