using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BtnAnimate : MonoBehaviour,
IPointerDownHandler, IPointerUpHandler,
IPointerEnterHandler, IPointerExitHandler
{
    public bool IsCloseBtn = false;
    private bool _InArea = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.1f);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _InArea = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _InArea = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        //是关闭按钮，并且在点击区域中抬起
        if (IsCloseBtn && _InArea)
        {
            return;
        }

        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScale(new Vector3(1.05f, 1.05f, 1f), 0.04f));
        seq.Append(transform.DOScale(new Vector3(1f, 1f, 1f), 0.06f));
    }
}
