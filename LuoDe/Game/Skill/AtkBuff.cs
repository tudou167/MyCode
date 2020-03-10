using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtkBuff : MonoBehaviour
{
    public Hero hero;
    public float recycleTime;
    public float ratio;
    public float tmpAtk;
    private GameObject buff;
    private Transform parent;

    void OnEnable()
    {
        buff = Pool.Instance.GetObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/Skill/AtkBuff", new Vector3(282, 464, 0), Quaternion.identity, parent == null ? GameObject.Find("/UI/Canvas").transform : parent);
        buff.transform.localPosition = new Vector3(282, 464, 0);
        Debug.Log(111);
        StartCoroutine(Recycle());
    }
    void Start()
    {
        hero = transform.parent.GetComponent<Hero>();
        tmpAtk = hero.curATK;
    }

    IEnumerator Recycle()
    {
        yield return null;
        hero.curATK = (int)(tmpAtk * ratio);
        yield return new WaitForSeconds(recycleTime);
        hero.curATK = (int)tmpAtk;
        Pool.Instance.RecyleObj(buff, "UI");
        Pool.Instance.RecyleObj(gameObject, "Effect");
    }

}
