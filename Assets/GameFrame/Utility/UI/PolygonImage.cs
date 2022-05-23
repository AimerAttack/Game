using UnityEngine;
using UnityEngine.UI;

namespace GameFrame.Utility.UI
{
    //异形响应区域图片
    public class PolygonImage : Image
    {
        private PolygonCollider2D _polygon;

        private PolygonCollider2D Polygon
        {
            get
            {
                if (_polygon == null)
                    _polygon = GetComponent<PolygonCollider2D>();

                return _polygon;
            }
        }
        public override bool IsRaycastLocationValid(Vector2 screenPoint, Camera eventCamera)
        {
            Vector3 point;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, screenPoint, eventCamera, out point);
            return Polygon.OverlapPoint(point);
        }
    }
}