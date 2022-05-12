using System;
using System.Collections;
using GameFrame.Utility.Extension;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;
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
        public bool UseDrag = false;
        [LabelText("是否可释放")]
        public bool UseDrop = false;
        [ShowIf("UseDrop")]
        [LabelText("如果拖拽失败，重置坐标")]
        public bool ResetPositionIfDropFailed = true;

        [LabelText("是否可长按")]
        public bool UseHold = false;
        [LabelText("长按阈值")]
        public float HoldThreshold = 0.5f;
        [ShowIf("UseDrag")]
        [LabelText("子节点响应拖拽")] public bool ChildResponseDrag = false;

        public UnityEvent OnClick;
        public BoolAction<GameObject> OnDrop;
        public UnityEvent OnHold;

        private ButtonPro _Parent;
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
                Target = GetComponent<Graphic>();
            if (Target == null)
            {
                enabled = false;
                return;
            }
            _Trans = Target.GetComponent<RectTransform>();
            _RectParent = _Trans.parent.GetComponent<RectTransform>();
            if (UseDrag && ChildResponseDrag)
            {
                var btns = GetComponentsInChildren<ButtonPro>(true);
                if (btns != null)
                {
                    foreach (var btn in btns)
                    {
                        if (btn != this)
                        {
                            btn._Parent = this;
                        }
                    }
                }
            }
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _OldPosition = _Trans.localPosition;
            _PointDownPosition = eventData.GETPointerBy(_RectParent);
            _LastDragPosition = _PointDownPosition;
            _PointDownTime = Time.unscaledTime;
            _PointDown = true;
            if (UseHold)
                StartCoroutine("CheckHold");
            if(UseDrop || _Parent != null && _Parent.UseDrop)
                _DisableRaycast();
            if (_Parent != null)
            {
                _Parent._OldPosition = _Parent._Trans.localPosition;
                _Parent._PointDownPosition = eventData.GETPointerBy(_Parent._RectParent);
                _Parent._LastDragPosition = eventData.GETPointerBy(_Parent._RectParent);
            }
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if(UseDrop || _Parent != null && _Parent.UseDrop)
                _RestoreRaycast();
            if (_Dragging)
            {
                if (_Parent != null)
                {
                    if (_Parent.UseDrop)
                    {
                        var success = _Parent._DoDrop(eventData);
                        if (!success && _Parent.ResetPositionIfDropFailed)
                            _Parent._Trans.localPosition = _Parent._OldPosition;
                    }
                }
                else
                {
                    if (UseDrop)
                    {
                        var success = _DoDrop(eventData);
                        if (!success && ResetPositionIfDropFailed)
                            _Trans.localPosition = _OldPosition;
                    }
                }
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
            if(UseHold)
                StopCoroutine("CheckHold");

            _PointDown = false;
            _Dragging = false;
            _Holding = false;
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_Parent != null)
            {
                var pos = eventData.GETPointerBy(_Parent._RectParent);
                if (!_Dragging && (pos - _Parent._PointDownPosition).magnitude < _Parent.DragThreshold)
                    return;
                var newPos = (Vector2) _Parent._Trans.localPosition + (pos - _Parent._LastDragPosition);
                _Parent._Trans.localPosition = newPos;
                _Parent._LastDragPosition = pos;
                _Dragging = true;
            }
            else
            {
                if (!_PointDown || !UseDrag || _Holding)
                    return;
                var pos = eventData.GETPointerBy(_RectParent);
                if (!_Dragging && (pos - _PointDownPosition).magnitude < DragThreshold)
                    return;
                var newPos = (Vector2) _Trans.localPosition + (pos - _LastDragPosition);
                _Trans.localPosition = newPos;
                _LastDragPosition = pos;
                _Dragging = true;
            }
        }

        void _DisableRaycast()
        {
            if (_Parent != null)
            {
                var canvas = _Parent.Target.AddOrGet<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerName = "top"; 
            }
            else
            {
                var canvas = Target.AddOrGet<Canvas>();
                canvas.overrideSorting = true;
                canvas.sortingLayerName = "top";
            }
        }

        void _RestoreRaycast()
        {
            if (_Parent != null)
            {
                Destroy(_Parent.Target.GetComponent<Canvas>());
            }
            else
            {
                Destroy(Target.GetComponent<Canvas>());
            }
        }

        void _DoClick()
        {
            OnClick?.Invoke();
        }

        bool _DoDrop(PointerEventData eventData)
        {
            if(OnDrop == null)
                return false;
            var result =  OnDrop.Invoke(eventData.pointerCurrentRaycast.gameObject);
            return result;
        }

        void _DoHold()
        {
            OnHold?.Invoke();
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