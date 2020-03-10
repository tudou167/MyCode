using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class Register : MonoBehaviour
{
    DeleteHis go;
    void Start()
    {

        transform.Find("window/GoBack").GetComponent<Button>().onClick.AddListener(GoBack);
        transform.Find("window/Register").GetComponent<Button>().onClick.AddListener(RegisterSuccess);
    }
    private void GoBack()
    {
        transform.Find("window/Account/InputField").GetComponent<InputField>().text = "";
        transform.Find("window/Password/InputField").GetComponent<InputField>().text = "";
        transform.Find("window/RePassword/InputField").GetComponent<InputField>().text = "";
        gameObject.SetActive(false);
        transform.parent.Find("Login").gameObject.SetActive(true);
    }
    private void RegisterSuccess()
    {
        string account = transform.Find("window/Account/InputField").GetComponent<InputField>().text;
        string password = transform.Find("window/Password/InputField").GetComponent<InputField>().text;
        string rePassword = transform.Find("window/RePassword/InputField").GetComponent<InputField>().text;

        if (account == "" || password == "")
        {
           // Debug.Log("用户名不能为空");
            //littleHint();
            if (go == null)
            {
            littleHint("<color=red>注意！账号或密码不能为空!</color>，请检查重试", DeleteHis.Pattern.pattern3);
            }
            else
            {
                go.debugTime = 0;
                go.transform.Find("Content").GetComponent<Text>().text 
                    = "<color=red>注意！账号或密码不能为空!</color>，请检查重试";
                StartCoroutine("ss");
                return;
            }
            return;
        }else if (rePassword == "")
        {
           // Debug.Log("密码不能为空");
            if (go == null)
            {
                littleHint("<color=red>注意！确认密码不能为空</color>，请检查重试", DeleteHis.Pattern.pattern3);
            }
            else
            {
                go.debugTime = 0;
                go.transform.Find("Content").GetComponent<Text>().text 
                    = "<color=red>注意！确认密码不能为空</color>，请检查重试";
                StartCoroutine("ss");
                return;
            }
            return;
        }else if (password != rePassword)
        {
            //Debug.Log("两次输入的密码不一致");
            if (go == null)
            {
                littleHint("<color=red>注意！确认密码不正确</color>，确认密码必须跟密码一致，请检查重试", DeleteHis.Pattern.pattern3);
            }
            else
            {
                go.debugTime = 0;
                go.transform.Find("Content").GetComponent<Text>().text 
                    = "<color=red>注意！确认密码不正确</color>，确认密码必须跟密码一致，请检查重试";
                StartCoroutine("ss");
                return;
            }
            return;
        }

        UserData data = new UserData(account, password);
        int userId = UserManager.Instance.AddUser(data);
        if (userId == 0)
        {
            // Debug.Log("帐号已存在");
            ///
            if (go == null)
            {
                littleHint("<color=red>账号已经存在!</color>，请检查重试", DeleteHis.Pattern.pattern3);
            }
            else
            {
                go.debugTime = 0;
                go.transform.Find("Content").GetComponent<Text>().text
                    = "<color=red>账号已经存在!</color>，请检查重试";
                StartCoroutine("ss");
                return;
            }
            return;
            //littleHint("<color=red>账号已经存在!</color>，请检查重试", DeleteHis.Pattern.pattern3);
            ///
          
        }
        //Debug.Log("注册成功");
        if (go == null)
        {
            littleHint("<color=green>注册成功</color>", DeleteHis.Pattern.pattern3);
        }
        else
        {
            go.debugTime = 0;
            go.transform.Find("Content").GetComponent<Text>().text
                = "<color=green>注册成功!</color>";
            StartCoroutine("ss");
         }
       gameObject.SetActive(false);
        transform.parent.Find("Login").gameObject.SetActive(true);
        Destroy(gameObject);
    }
    IEnumerator ss()
    {

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
    void littleHint(string text, DeleteHis.Pattern his)
    {
        if (go == null)
        {
            go = "Prefabs/UI/ZQS/ASmallObject/LittleHint".toLad("UI/Canvas").GetComponent<DeleteHis>();
            go.pattern = his;
            go.transform.localPosition = new Vector3(0, 250, 0);
            go.debugTime = 0;
            go.hi = 3.5f;
        }
        else
        {
            go.debugTime = 0;
            StartCoroutine("ss");
            return;
        }

        go.transform.Find("Content").GetComponent<Text>().text = text ;
        RectTransform t = go.transform as RectTransform;
        t.sizeDelta = new Vector2(666, 333);
        return;
    }
}
