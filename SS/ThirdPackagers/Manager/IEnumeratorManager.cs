using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public delegate void LoadCallBack();
public class IEnumeratorManager : MonoBehaviour
{
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    [HideInInspector]
    private static IEnumeratorManager instance;
    public static IEnumeratorManager Instance
    {
        get
        {
            return instance;
        }
    }
    private void Awake()
    {
        instance = this;
    }
    public void StartIEnum(IEnumerator ie)
    {
        StartCoroutine(ie);


    }
    public IEnumerator DelayToInvokeDo(float delaySeconds, LoadCallBack callBack = null)

    {
        yield return new WaitForSeconds(delaySeconds);
        if (callBack != null)
        {
            callBack();
        }

    }
    public void MyInvoke(float delaySeconds, LoadCallBack callBack = null){
        StartIEnum(DelayToInvokeDo(delaySeconds,callBack));
    }


}
