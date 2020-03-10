using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GM : MonoBehaviour
{

    public TMP_InputField text;
    private void Awake() {
        DontDestroyOnLoad(gameObject);
    }
    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.BackQuote))
        {
            if (text.gameObject.activeSelf)
            {
                text.gameObject.SetActive(false);
            }
            else
            {
                text.gameObject.SetActive(true);
            
            }
            text.gameObject.transform.SetAsLastSibling();
        }
        if (text.gameObject.activeSelf==true && (Input.GetKeyDown(KeyCode.Return)|| Input.GetKeyDown(KeyCode.KeypadEnter)))
        {
            string[] temp = text.text.Split(' ');
            GetSwitch(temp);
            text.gameObject.SetActive(false);
        }
    }
    public void GetSwitch(string[] temp)
    {
        switch (temp[0])
        {
            case "Add":
                {
                    switch (temp[1])
                    {
                        case "Gold":
                            {
                                PlayerModel.Instance.AddGold(int.Parse(temp[2]));
                                break;
                            }
                        case "Atk":
                            {
                                PlayerModel.Instance.AddAtk(int.Parse(temp[2]));
                                break;
                            }
                        case "Crit":
                            {
                                PlayerModel.Instance.AddCrit(int.Parse(temp[2]));
                                break;
                            }
                        case "Def":
                            {
                                PlayerModel.Instance.AddDef(int.Parse(temp[2]));
                                break;
                            }
                        case "Diamond":
                            {
                                PlayerModel.Instance.AddDiamond(int.Parse(temp[2]));
                                break;
                            }
                        case "Exp":
                            {
                                PlayerModel.Instance.AddExp(int.Parse(temp[2]));
                                break;
                            }
                        case "Hp":
                            {
                                PlayerModel.Instance.AddHp(int.Parse(temp[2]));
                                break;
                            }
                        case "Lv":
                            {
                                PlayerModel.Instance.AddLv(int.Parse(temp[2]));
                                break;
                            }
                        case "MaxHp":
                            {
                                PlayerModel.Instance.AddMaxHp(int.Parse(temp[2]));
                                break;
                            }
                        case "RegisterDay":
                            {
                                PlayerModel.Instance.AddRegisterDay(int.Parse(temp[2]));
                                break;
                            }
                        default:
                            GameDebug.Log("无效指令:Add " + temp[1]);
                            break;
                    }
                    break;
                }
            default:
                GameDebug.Log("无效指令:" + temp[0]);
                break;
        }
   
    }
}
