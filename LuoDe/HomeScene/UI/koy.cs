using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class koy : MonoBehaviour {

	
	void Start () {
        StartCoroutine("animal");
        InvokeRepeating("animal1",3f,6f);
       // InvokeRepeating("animal2", 3f, 10f);
    }
    
    public Transform fish, bird;
    float v ;
    void Update () {
        v += Time.deltaTime;
        if (v >= 16)
        {
            StartCoroutine("animal");
            v = 0;
        }
    }
    IEnumerator animal()
    {
       bird.DOLocalMove(new Vector3(-960, 245), 3f);
        yield return new WaitForSeconds(4);
        bird.DOLocalMove(new Vector3(-1372, 245), 1f);
        yield return new WaitForSeconds(2);
        bird.DOLocalMove(new Vector3(-1365, 33), 0.5f);
        yield return new WaitForSeconds(1.5f);
        bird.DOLocalMove(new Vector3(-2444, 667), 3f);
        yield return new WaitForSeconds(4f);
        int a = Random.Range(0, 2);
        if (a == 0)
        {
            bird.DOLocalMove(new Vector3(100, -450), 0f);
        }
        else
        {
            bird.DOLocalMove(new Vector3(200, 700), 0f);
        }
    }

    void animal1()
    {
        Sequence s = DOTween.Sequence();
        s.Append(fish.DOLocalMove(new Vector3(-1116, 480),3f));
        s.Append(fish.DOLocalMove(new Vector3(-2280, -322), 3f));
        s.Append(fish.DOLocalMove(new Vector3(100, -450), 0f));
     }
    //void animal2()
    //{
    //    Sequence s = DOTween.Sequence();
    //    s.Append(bird.DOLocalMove(new Vector3(-960, 250), 3f));
    //    s.Append(bird.DOLocalMove(new Vector3(-1372,257), 1f));
    //    s.Append(bird.DOLocalMove(new Vector3(-1365, 33), 0.5f));
    //    s.Append(bird.DOLocalMove(new Vector3(-2444, 667), 3f));
    //    int a = Random.Range(0,2);
    //    if (a == 0)
    //    {
    //        s.Append(bird.DOLocalMove(new Vector3(100, -450), 0f));
    //    }
    //    else
    //    {
    //        s.Append(bird.DOLocalMove(new Vector3(200, 700), 0f));
    //    }
       
    //}
}
