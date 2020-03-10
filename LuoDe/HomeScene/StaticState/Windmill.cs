using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Windmill : MonoBehaviour {

	
	void Start () {
		
	}

    public float windmills , wind = 5;

    void Update () {
        windmills += Time.deltaTime;
         if (windmills >= 5)
        {
            windmill();
            windmills = 0;
        }
        transform.Rotate(wind,0, 0);
	}
    /// <summary>
    /// 随机生成风力
    /// </summary>
    private void windmill()
    {
     wind = Random.Range(0.1f, 2);
    }

}
