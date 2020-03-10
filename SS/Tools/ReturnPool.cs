using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPool : MonoBehaviour {
    
    public float time=5;
	private void Start() {
        Invoke("Return",time);
    }
    public void Return(){
        Destroy(gameObject);
        //MyGameObjectPool.Instance.Return(gameObject);
    }
}
