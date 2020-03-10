using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class FightChange : Singleton<FightChange>
{
    Transform mainCamera;
    Transform hudCanvas;
    Transform uiRoot;
    Transform gamePool;
    Transform uiCanvas;

    AsyncOperation async;
    public UnityEngine.Events.UnityAction callback;
    public void FightEnd()
    {
        FightTmpData data = FightTmpManager.Instance.GetData();
        if (data.curPlieNum % 5 == 0) data.isBack = true;
        Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/NextChange", uiCanvas);
    }

    public void FightStart(Transform parent)
    {
        if (mainCamera == null)
        {
            Tool.Instance.InstantiateObj(AllPath.Instance.rolePrefabsPath + "Hero", parent);
            mainCamera = Tool.Instance.InstantiateObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightSceneComponet/Main Camera", parent).transform;
            hudCanvas = Tool.Instance.InstantiateObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightSceneComponet/HUDCanvas", parent).transform;
            hudCanvas.GetComponent<Canvas>().worldCamera = Camera.main;
            gamePool = Tool.Instance.InstantiateObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightSceneComponet/GamePool", parent).transform;
            gamePool.position = new Vector3(1000,1000,1000);
            uiRoot = Tool.Instance.InstantiateObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightSceneComponet/UI", parent).transform;
            uiCanvas = uiRoot.Find("Canvas").transform;
            uiCanvas.GetComponent<Canvas>().worldCamera = uiRoot.transform.Find("UICamera").transform.GetComponent<Camera>();
        }
    }
    public void FightOver()
    {
        Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/FightStop", uiCanvas);
    }

    public IEnumerator LoadScene(Transform parent = null)
    {
        if (parent == null)
        {
            parent = uiCanvas;
        }
        //倒计时
        int countDown = 5;
        GameObject go = Tool.Instance.InstantiateObjOffset(AllPath.Instance.uiPrefabsPath + "Fight_Czy/CountTime", parent);
        UnityEngine.UI.Text text = go.transform.Find("Text").GetComponent<UnityEngine.UI.Text>();
        if (FightTmpManager.Instance.GetData().isBack)
        {
            async = SceneManager.LoadSceneAsync(SceneName.HomeScene.ToString());
            callback = DestroyTf;
        }
        else
        {
            DontDestroyTf();
            if (FightTmpManager.Instance.GetData().curPlieNum == 14)
            {
                async = SceneManager.LoadSceneAsync(SceneName.Fight.ToString() + 4);
            }
            else
            {
                async = SceneManager.LoadSceneAsync(SceneName.Fight.ToString() + Random.Range(1, 7));
            }
        }
        async.allowSceneActivation = false;
        while (countDown != 0)
        {
            text.text = countDown.ToString();
            countDown--;
            yield return new WaitForSeconds(1);
        }
        Object.Destroy(go);
        if (callback != null)
        {
            callback();
            callback = null;
        }
        async.allowSceneActivation = true;
    }
    private void DestroyTf()
    {
        Object.Destroy(HeroManager.Instance.GetCurHero().gameObject);
        Object.Destroy(mainCamera.gameObject);
        Object.Destroy(hudCanvas.gameObject);
        Object.Destroy(gamePool.gameObject);
        Object.Destroy(uiRoot.gameObject);
    }
    private void DontDestroyTf()
    {
        Object.DontDestroyOnLoad(HeroManager.Instance.GetCurHero());
        Object.DontDestroyOnLoad(mainCamera);
        Object.DontDestroyOnLoad(hudCanvas);
        Object.DontDestroyOnLoad(gamePool);
        Object.DontDestroyOnLoad(uiRoot);
    }
}
