using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMageAtk : MonoBehaviour
{
    float curTime;
    public UnityEngine.Events.UnityAction<GameObject> callBack;
    public LayerMask curUserLayer;
    void OnEnable()
    {
        curTime = 0;
    }

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * 30);
        curTime += Time.deltaTime;
        if (curTime > 3)
        {
            Pool.Instance.RecyleObj(gameObject, "Effect");
            curTime = 0;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        //TODO 用数字不行
        if (LayerMask.LayerToName(other.gameObject.layer) == LayerType.hero.ToString())
        {
            if (callBack != null)
            {
                callBack(other.gameObject);
            }
            Pool.Instance.RecyleObj(gameObject, "Effect");
        }
    }
}
