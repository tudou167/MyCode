using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using DG.Tweening;
public class Login : MonoBehaviour
{


    public GameObject MainCamera;
    public AudioSource sound;
    public AudioClip music;
    void Start()
    {
        transform.Find("Register").GetComponent<Button>().onClick.AddListener(GoRegister);
        transform.Find("Login").GetComponent<Button>().onClick.AddListener(LoginSuccess);

        MainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        sound = MainCamera.GetComponent<AudioSource>();
        if (sound == null)
        {
            sound = MainCamera.AddComponent<AudioSource>();
        }
        music = Resources.Load<AudioClip>("Voice/BackgroundMusic/Color-X 3D");
        sound.clip = music;
        sound.playOnAwake = false;
        sound.loop = true;
        sound.spatialBlend = 1;
        sound.Play();
        sound.volume = 0.5f;
    }

    private void GoRegister()
    {
        transform.Find("Account/InputField").GetComponent<InputField>().text = "";
        transform.Find("Password/InputField").GetComponent<InputField>().text = "";
        gameObject.SetActive(false);
        Transform registerTr = transform.parent.Find("Register");
        GameObject register;
        if (registerTr == null)
        {
            register = Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/Register", transform.parent);
        }
        else
        {
            register = registerTr.gameObject;
        }
        register.SetActive(true);
        ///窗口跳转进入dotween动画
        Transform win = register.transform.Find("window");
        Sequence s = DOTween.Sequence();
        s.Append(win.transform.DOScale(new Vector3(0.8f, 0.8f, 1f), 0.0f));
        s.Append(win.transform.DOScale(new Vector3(1.1f, 1.1f, 1f), 0.6f));
        s.Append(win.transform.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
    }
    private void LoginSuccess()
    {

        string account = transform.Find("Account/InputField").GetComponent<InputField>().text;
        string password = transform.Find("Password/InputField").GetComponent<InputField>().text;
        UserData data = UserManager.Instance.GetUser(account);
        if (data != null)
        {
            if (data.Password == password)
            {
                Tool.Instance.InstantiateObj("Prefabs/Loader/HeroLoader", transform.root);
                Tool.Instance.InstantiateObjOffset("Prefabs/UI/LBN/HeroSelect", transform.parent);
                Transform registerTr = transform.parent.Find("Register");
                if (registerTr != null)
                {
                    GameObject register = registerTr.gameObject;
                    Destroy(register);
                }
                Destroy(gameObject);
                return;
            }
        }

        // Debug.Log("帐号或密码错误");
        ///账号提示登录失败
        if (go == null)
        {
            go = "Prefabs/UI/ZQS/BeInCommonUse/Hint".toLad("UI/Canvas");
        }
        else
        {
            go.gameObject.SetActive(true);
        }
        Text f = go.transform.Find("window/text/TextContent").GetComponent<Text>();
        f.text = "<color=red>登录失败！</color>账号或密码错误，请检查重新输入               " +
            "小提示：如果多次登录失败请尝试重新注册";
        Sequence s = DOTween.Sequence();
        s.Append(go.DOScale(new Vector3(0.5f, 0.5f, 1f), 0.0f));
        s.Append(go.DOScale(new Vector3(1.2f, 1.2f, 1f), 0.6f));
        s.Append(go.DOScale(new Vector3(1f, 1f, 1f), 0.4f));
        f.transform.parent.Find("login").GetComponent<Button>().onClick.AddListener(GoRegister);
        f.transform.parent.Find("login").GetComponent<Button>().onClick.AddListener(back);
        f.transform.parent.Find("back").GetComponent<Button>().onClick.AddListener(back);

    }
    Transform go;
    void back()
    {
        go.gameObject.SetActive(false);
    }
    ///
}
