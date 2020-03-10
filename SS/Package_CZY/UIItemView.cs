using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIItemView : MonoBehaviour
{
    private Image icon;
    private Text isWear;
    private Text count;
    public EquipmentTemp.Row.Row2 _Data;
    public RectTransform rectTransform;
    public Vector3 curPos;
    private Image quality;


    void Awake()
    {
        icon = GetComponent<Image>();
        count = transform.Find("Text").GetComponent<Text>();
        isWear = transform.Find("IsWear").GetComponent<Text>();
        rectTransform = GetComponent<RectTransform>();
        quality = transform.Find("Image").GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    public void DisPlay(EquipmentTemp.Row.Row2 data)
    {

        //将道具的数据存储在Cell的View脚本中
        _Data = data;

        //加载Icon的图片
        icon.sprite = Resources.Load<Sprite>(AllPaths.Instance.allIconPath + data.eid);

        //修改数量文字
        count.text = data.count.ToString();

        switch (data.quality)
        {
            case Enumclass.Quality.普通:
                quality.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 50f / 255f);
                break;
            case Enumclass.Quality.业余:
                quality.color = new Color(138f / 255f, 255f / 255f, 124f / 255f, 50f / 255f);
                break;
            case Enumclass.Quality.稀有:
                quality.color = new Color(80f / 255f, 131f / 255f, 250f / 255f, 50f / 255f);
                break;
            case Enumclass.Quality.专家:
                quality.color = new Color(255f / 255f, 104f / 255f, 0f / 255f, 50f / 255f);
                break;
            case Enumclass.Quality.传奇:
                quality.color = new Color(255f / 255f, 0f / 255f, 0f / 255f, 50f / 255f);
                break;
            default:
                quality.color = new Color(255f / 255f, 255f / 255f, 255f / 255f, 100f / 255f);
                break;
        }


        if (data.isWear == 1)
        {
            isWear.text = "E";
        }
    }


    public void DragItem(PointerEventData eventData)
    {
        Vector3 pos;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(rectTransform, eventData.position, eventData.enterEventCamera, out pos);
        rectTransform.position = pos;

    }

    public void setItemPos(Vector3 pos)
    {
        rectTransform.position = pos;
    }
    
}
