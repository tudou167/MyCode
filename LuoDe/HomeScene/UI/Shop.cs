using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    public List<Toggle> toggles;
    /// <summary>
    /// 所有选项
    /// </summary>
    public List<GameObject> goPanle;
    /// <summary>
    /// 当前选择的状态
    /// </summary>
    public GameObject currentOpenPanle;
    /// <summary>
    /// ToggleGroup组件
    /// </summary>
    ToggleGroup to;
    void Start()
    {
        to = transform.GetComponent<ToggleGroup>();
          toggles = new List<Toggle>();//创建了一个新的toggle存储在toggles变量中
        for (int i = 0; i < transform.childCount; i++)//便利自己的孩子有多少个
        {
            toggles.Add(transform.GetChild(i).GetComponent<Toggle>());
            //便利的孩子存储在定义tossles变量中
        }
        for (int i = 0; i < goPanle.Count; i++)//便利有多少个页面
        {
            if (goPanle[i] == null) continue;//如果这个页面是可的那就跳过他
            goPanle[i].SetActive(false);//防止页面提前打开了所有把他们全部关闭
        }
        currentOpenPanle = goPanle[0];//但是我们默认显示第一个，所以打开第一个
        currentOpenPanle.SetActive(true);
    }

    /// <summary>
    /// 选择项
    /// </summary>
    /// <param name="v"></param>
    public void shop(bool v)
    {
        for (int i = 0; i < toggles.Count; i++)//便利存储在toggles变量中的孩子
        {
            if (toggles[i].isOn) {//判断这个孩子的开关是否打开
                currentOpenPanle.SetActive(false);//如果是我们就关闭他的页面
                currentOpenPanle = goPanle[i];//并且把下一个开关开启
                currentOpenPanle.SetActive(true);
                return;
            }
        }
    }
}
