using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeroCreate : MonoBehaviour
{
    void Start()
    {
        transform.Find("Create").GetComponent<Button>().onClick.AddListener(CreateSuccess);
        transform.Find("Back").GetComponent<Button>().onClick.AddListener(GoBack);
    }
    Transform s = null;
    DeleteHis  go;
    void CreateSuccess()
    {
        InputField name = transform.Find("InputName").GetComponent<InputField>();
        List<RoleData> heroList = HeroManager.Instance.GetAllHero();
        if (name.text == "")
        {
            return;
        }
        for (int i = 0; i < heroList.Count; i++)
        {
            if (name.text == heroList[i].Name)
            {
                Debug.Log("名字重复");
                littleHints("昵称出现重复", DeleteHis.Pattern.pattern3);

              return;
            }
        }
        int userID = UserManager.Instance.GetCurUser().ID;
        HeroManager.Instance.AddHero(userID, name.text);
        Debug.Log("创建成功");
        littleHints("创建成功", DeleteHis.Pattern.pattern3);
        //Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroSelect",transform.parent);
        transform.parent.Find("HeroSelect").gameObject.SetActive(true);
        name.text = "";
        gameObject.SetActive(false);
    }
    void GoBack()
    {
        transform.Find("InputName").GetComponent<InputField>().text = "";
        gameObject.SetActive(false);
        Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroSelect",transform.parent);
    }

   IEnumerator ss()
    {
        Debug.Log("进入了协同程序");
        Debug.Log(go);
        go.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(false);
        yield return new WaitForSeconds(0.1f);
        go.gameObject.SetActive(true);
  
    }
    /// <summary>
    /// 创建一个小提示
    /// </summary>
    /// <param name="text">小提示有显示的文本</param>
    /// <param name="his">选择的模式</param>
    void littleHints(string text, DeleteHis.Pattern his)
    {
        if (go == null)
        {
            go = "Prefabs/UI/ZQS/ASmallObject/LittleHint".toLad("UI/Canvas").GetComponent<DeleteHis>();
            go.pattern = his;
            go.transform.localPosition = new Vector3(0,100, -350);
            go.debugTime = 0;
            go.hi = 3.5f;
        }
        else
        {
            go.debugTime = 0;
            StartCoroutine("ss");
            return;
        }

        go.transform.Find("Content").GetComponent<Text>().text = "<color=red>"+text+"</color>";
        RectTransform t = go.transform as RectTransform;
        t.sizeDelta = new Vector2(600, 305);
        go.transform.SetAsLastSibling();
        return;
    }
}
