using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
    public Hero hero;
    public float recycleTime;
    public float ratio;
    public float tmpSpeed;
    private GameObject buff;
    private Transform parent;
    void OnEnable()
    {
        buff = Pool.Instance.GetObj(AllPath.Instance.uiPrefabsPath + "Fight_Czy/Skill/SpeedBuff", new Vector3(827, 1040, 0), Quaternion.identity, parent == null ? GameObject.Find("/UI/Canvas").transform : parent);
        buff.transform.localPosition = new Vector3(827, 1040, 0);
        StartCoroutine(Recycle());
    }
    void Start()
    {
        hero = transform.parent.GetComponent<Hero>();
        tmpSpeed = hero.curATK;
    }

    IEnumerator Recycle()
    {
        yield return null;
        hero.moveSpeed *= ratio;
        yield return new WaitForSeconds(recycleTime);
        hero.moveSpeed = tmpSpeed;
        Pool.Instance.RecyleObj(buff, "UI");
        Pool.Instance.RecyleObj(gameObject, "Effect");
    }
}
