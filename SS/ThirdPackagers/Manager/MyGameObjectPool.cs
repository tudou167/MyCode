using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Internal;
public class MyGameObjectPool : MonoBehaviour {
    private static MyGameObjectPool instance;
    public static MyGameObjectPool Instance {
        get {
            return instance;
        }
    }
    private void Awake () {
        instance = this;
        dic = new Dictionary<string, List<GameObject>> ();
    }
    public Dictionary<string, List<GameObject>> dic;
    //预制体名,Resource路径,初始位置,初始角度
    public GameObject Get (string key, string path, Vector3 position, Quaternion rotation) {
        string name = key + "(Clone)";
        GameObject push;

        if (dic.ContainsKey (name) && dic[name].Count > 0) {
            push = dic[name][dic[name].Count - 1];
            dic[name].Remove (push);
            push.transform.position = position;
            push.transform.rotation = rotation;
            push.SetActive (true);
        } else {
            push = Instantiate (Resources.Load<GameObject> (path), position, rotation, transform);
        }

        return push;
    }
    public void Return (GameObject put) {
        put.SetActive (false);
        string key = put.name;
        if (dic.ContainsKey (key)) {
            dic[key].Add (put);
        } else {
            dic[key] = new List<GameObject> { put };
        }
    }
}