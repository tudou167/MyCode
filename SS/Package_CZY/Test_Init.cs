using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Test_Init : MonoBehaviour {
    public GameObject boss;
    public GameObject map;
    public GameObject player_information;
    public AsyncOperation async;
    void Start () {
        //Debug.Log(Application.persistentDataPath);

        //    //UIPackageModel.Instance.InItem(UIPackageModel.Instance.GainItem());

        //    //return;
        //    //AllTools.Instance.Load(AllPaths.Instance.packagePrefabs + "Package");
    }
    public void TempClick () {
        //UIPackageModel.Instance.NewPackage();
        UIPackageModel.Instance.InItem (UIPackageModel.Instance.GainItem ());

    }
    public void EnterSence () {
        StartCoroutine ("Load");
    }
    IEnumerator Load () {
        async = SceneManager.LoadSceneAsync (1);
        yield return async;
        PlayerDataInfoView.Instance.Display_BossDataInfo ();
        map.transform.DOMoveX (2100, 1);
        player_information.transform.DOMoveY (1200, 1);
        //boss.transform.DOMoveY (-1, 1);
        transform.DOMoveY(-600,1);
    }
}