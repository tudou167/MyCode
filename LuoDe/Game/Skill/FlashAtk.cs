using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashAtk : MonoBehaviour {

    protected virtual void OnEnable()
    {
        StartCoroutine(Recyle());
    }
    public IEnumerator Recyle()
    {
        yield return new WaitForSeconds(1);
        Pool.Instance.RecyleObj(gameObject, "Effect");
    }

}
