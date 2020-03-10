using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneLoadManager : MonoBehaviour
{
    private static SceneLoadManager instance;
    public static SceneLoadManager Instance
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
    private void Start()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    AsyncOperation async = null;
    public void SceneLoad(string sceneName, LoadCallBack loadCallBack = null)
    {
        IEnumeratorManager.Instance.StartIEnum(SceneLoadIE(sceneName, loadCallBack));
    }
    IEnumerator SceneLoadIE(string sceneName, LoadCallBack loadCallBack = null)
    {
        async = SceneManager.LoadSceneAsync(sceneName);
        yield return async;
        if (loadCallBack != null)
        {
            loadCallBack();
        }
    }
}
