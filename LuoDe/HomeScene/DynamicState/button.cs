using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.EventSystems;

public class button : MonoBehaviour,IPointerDownHandler,IPointerUpHandler {
    public enum MyEnum
    {
        off,
        yes,
        no
    }
    public  AudioClip yes, no;
    AudioSource go;
    GameObject voice;
    private void Awake()
    {
        voice = GameObject.Find("voice");
        yes = Resources.Load<AudioClip>("Voice/button/yes/mcx20070511");
        no = Resources.Load<AudioClip>("Voice/button/no/mcx20070511");
        if (voice == null)
        {
            return;
        }
        go = voice.transform.GetComponent<AudioSource>();
        if (go == null)
        {
            go = voice.transform.gameObject.AddComponent<AudioSource>();
        }
        go.playOnAwake = false;
        go.pitch = 3;
        if (sound == MyEnum.yes)
        {
            go.clip = yes;
        }
        else if (sound == MyEnum.no)
        {
            go.clip = no;
        }
    }
    void Start () {
     
    }
    public MyEnum sound = MyEnum.yes;
    void knapsack()
    {
      
    }

    public void OnPointerDown(PointerEventData eventData)
    {
         transform.DOScale(new Vector3(0.8f,0.8f,1), 0.2f);
         go.Play();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        transform.DOScale(new Vector3(1f,1f, 1), 0.5f);
    }
}
