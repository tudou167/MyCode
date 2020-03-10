using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.Events;

public class Smithy : MonoBehaviour {

	
	void Start () {
        effects_yp = transform.Find("window/effects-yp");
        effects_bb = transform.Find("window/effects-bb");
        sj = transform.Find("window/effects-sj");
        effects_yp.DOShakePosition(10f,20f,1,20).SetLoops(-1);
        //.Find("/Smithy/window/effects-sj");
      //pop =  Resources.Load<GameObject>("Prefabs/UI/ZQS/pop-upWindows/effects-sj");
      // ui = GameObject.FindGameObjectWithTag("UI");
      //  ui.transform.Find("UI/Smithy/window/effects-sj");

   // Func = effects;
    }
  
    Transform effects_yp, effects_bb,sj,pop;
  // public  GameObject pop,ui;
   public UnityAction Func;
    public void effects()
    {
      Sequence s =  DOTween.Sequence();
       s.Append( effects_bb.DOLocalMoveY(-1274, 2));
        pop = "Prefabs/UI/ZQS/pop-upWindows/effects-sj".toLad("Canvas/Screen");
        //   GameObject go = Instantiate(pop.gameObject);
  RectTransform goo   = pop.transform as RectTransform;
        goo.sizeDelta = new Vector2(657, 864);
        // go.transform.localScale=new Vector3(5, 5, 5);
        s.Append(effects_bb.DOLocalMoveY(1274, 0));
    }

  //void Update () {}
}
