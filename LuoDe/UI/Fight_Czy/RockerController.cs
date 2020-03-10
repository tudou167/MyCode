using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class RockerController : MonoBehaviour, IDragHandler , IEndDragHandler ,IBeginDragHandler
{
    GameObject rockerFrame;
    GameObject rocker;
    RectTransform rockerRect;
    Vector2 localPos;
    Hero hero;
    float radius;

    void Start()
    {
        rockerFrame = transform.Find("rocker").gameObject;
        rocker = rockerFrame.transform.Find("rockerX").gameObject;
        rockerRect = rockerFrame.transform as RectTransform;
        localPos = Vector2.zero;
        radius = 115;
        hero = HeroManager.Instance.GetCurHero().GetComponent<Hero>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        rockerFrame.SetActive(true);
        RectTransformUtility.ScreenPointToLocalPointInRectangle(transform as RectTransform, eventData.position, eventData.pressEventCamera, out localPos);
        rockerFrame.transform.localPosition = localPos;
    }

    public void OnDrag(PointerEventData eventData)
    {
        RectTransformUtility.ScreenPointToLocalPointInRectangle(rockerRect, eventData.position, eventData.pressEventCamera, out localPos);
        rocker.transform.localPosition = localPos;
        if (localPos.magnitude > radius)
        {
            rocker.transform.localPosition = localPos.normalized * radius;
        }
        hero.h = rocker.transform.localPosition.x / radius;
        hero.v = rocker.transform.localPosition.y / radius;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        hero.h = 0;
        hero.v = 0;
        rocker.transform.localPosition = Vector2.zero;
        rockerFrame.SetActive(false);
    }

}
