using System;
using System.Collections;
using GameFrame.Utility.Extension;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace GameFrame.Utility.UI
{
    public class ButtonPro : MonoBehaviour,IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        public Graphic Target;
        [LabelText("点击事件时间阈值")]
        public float ClickTimeThreshold = 0.3f;
        
        [ShowIf("UseDrag")]
        [LabelText("拖拽距离阈值")]
        public float DragThreshold = 30f;
        [LabelText("是否可拖拽")]
        public bool UseDrag = true;
        [ShowIf("UseDrag")]
        [LabelText("如果拖拽失败，重置坐标")]
        public bool ResetPositionIfDropFailed = true;

        [LabelText("是否可长按")]
        public bool UseHold = true;
        [LabelText("长按阈值")]
        public float HoldThreshold = 0.5f;

        public UnityEvent OnClick;

        private RectTransform _Trans;
        private RectTransform _RectParent;
        private Vector3 _OldPosition;

        #region Click
        private float _PointDownTime;
        private Vector2 _PointDownPosition;
        private bool _PointDown;
        #endregion
        
        #region Drag
        private bool _Dragging;
        private Vector2 _LastDragPosition;
        #endregion
        
        #region Hold

        private bool _Holding;
        #endregion

        private void Awake()
        {
            if (Target == null)
            {
                enabled = false;
                return;
            }
            _Trans = Target.GetComponent<RectTransform>();
            _RectParent = _Trans.parent.GetComponent<RectTransform>();       
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _OldPosition = _Trans.localPosition;
            _PointDownPosition = eventData.GETPointerBy(_RectParent);
            _LastDragPosition = _PointDownPosition;
            _PointDownTime = Time.unscaledTime;
            _PointDown = true;
            StartCoroutine("CheckHold");
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_Dragging)
            {
                var success = _DoDrop();
                if (!success && ResetPositionIfDropFailed)
                    _Trans.localPosition = _OldPosition;
            }
            else
            {
                var delta = Time.unscaledTime - _PointDownTime;
                if (delta > 0 && delta < ClickTimeThreshold)
                {
                    var pos = eventData.GETPointerBy(_RectParent);
                    if ((pos - _PointDownPosition).magnitude <= DragThreshold)
                    {
                        _DoClick();
                    }
                }
            }
            StopCoroutine("CheckHold");

            _PointDown = false;
            _Dragging = false;
            _Holding = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if(!_PointDown || !UseDrag || _Holding)
                return;
            var pos = eventData.GETPointerBy(_RectParent);
            if (!_Dragging && (pos - _PointDownPosition).magnitude < DragThreshold)
                return;
            var newPos = (Vector2)_Trans.localPosition + (pos - _LastDragPosition);
            _Trans.localPosition = newPos;
            _LastDragPosition = pos;
            _Dragging = true;
        }

        void _DoClick()
        {
            Debug.Log("Click");
            OnClick?.Invoke();
        }

        bool _DoDrop()
        {
            Debug.Log("Drop");
            return false;
        }

        void _DoHold()
        {
            Debug.Log("Hold");
        }

        IEnumerator CheckHold()
        {
            yield return new WaitForSeconds(HoldThreshold);
            if(!_PointDown)
                yield break;
            if (!_Dragging)
            {
                _Holding = true;
                _DoHold();
            }
        }
    }
}