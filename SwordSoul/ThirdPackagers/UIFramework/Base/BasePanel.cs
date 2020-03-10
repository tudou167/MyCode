using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class BasePanel : MonoBehaviour
{
    protected CanvasGroup canvasGroup;
    private void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    //显示界面
    public virtual void OnPause()
    {
        canvasGroup.blocksRaycasts = false;
    }
    public virtual void OnEnter()
    {
        //if(canvasGroup==null)canvasGroup = GetComponent<CanvasGroup>();
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.3f);
    }
    public virtual void OnExit()
    {
        canvasGroup.blocksRaycasts = false;
        canvasGroup.alpha = 0;
        transform.localScale = Vector3.one;
        transform.DOScale(0, 0.3f);
    }
    public virtual void OnResume()
    {
        canvasGroup.blocksRaycasts = true;
        canvasGroup.alpha = 1;
    }
    public virtual void OnPushPanel(string panelTypeString)
    {
        UIPanelType panelType = (UIPanelType)System.Enum.Parse(typeof(UIPanelType), panelTypeString);
        UIManager.Instance.PushPanel(panelType);

    }

    public virtual void OnClosePanel()
    {
        UIManager.Instance.PopPanel();
    }


}
