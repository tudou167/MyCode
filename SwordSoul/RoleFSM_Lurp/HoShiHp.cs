using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Werewolf.StatusIndicators.Components;

public class HoShiHp : CSharpSingletion<HoShiHp> {
    public CharacterInfo hoshi { get { return PlayerModel.Instance._characterInfo; } }
    public CharacterInfo boss { get { return PlayerModel.Instance._bossInfo; } }
    public Transform bossText { get { return GameObject.Find ("/BOSS/Canvas").transform; } }
    public Transform hoshiText { get { return GameObject.Find ("/Hoshi/Canvas").transform; } }
    private StatusIndicator status { get { return GameObject.Find ("StatusBasic2").GetComponent<StatusIndicator> (); } }
    private bool isCrit;
    private bool isGetHitByHp = false;
    public void HoshiGetHit (int Magnification) {
        int hitHp = -DoubleHit (boss.atk, boss.crit, out isCrit) * Magnification;
        GameDebug.Log (PlayerModel.Instance.AddHp (hitHp).ToString (), "Player的Hp");
        GameObject text = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/Common/HitText"), hoshiText.position, Quaternion.identity, hoshiText);
        TextMeshProUGUI HoshiText = text.GetComponent<TextMeshProUGUI> ();
        HoshiText.text = (-hitHp).ToString ();
        status.Progress = (float)hoshi.hp / (float)hoshi.maxHp;
        if (isCrit) {
            HoshiText.fontSize = 0.8f;
            HoshiText.color = new Color (1, 0.4f, 0, 1);
        }
        if (hoshi.hp > 0) { HoshiController.Instance.GetHit (); } else { HoshiController.Instance.SetHoshiDead (); }
    }
    public void BossGetHit (int Magnification) {
        int hitHp = -DoubleHit (hoshi.atk, hoshi.crit, out isCrit) * Magnification;
        GameDebug.Log (PlayerModel.Instance.AddBossHp (hitHp).ToString (), "Boss的Hp");
        GameObject text = GameObject.Instantiate<GameObject> (Resources.Load<GameObject> ("Prefabs/Common/HitText"), bossText.position, Quaternion.identity, bossText);
        TextMeshProUGUI BossText = text.GetComponent<TextMeshProUGUI> ();
        BossText.text = (-hitHp).ToString ();
        if (isCrit) { text.GetComponent<TextMeshProUGUI> ().fontSize = 0.8f; text.GetComponent<TextMeshProUGUI> ().color = new Color (1, 0.4f, 0, 1); }
        if (boss.hp <= boss.maxHp / 2 && isGetHitByHp == false) {
            isGetHitByHp = true;
            BossController.Instance.GetHitByHp ();
        }
        if (boss.hp <= 0) {
            BossController.Instance.SetBossDead ();
        }
    }
    public int DoubleHit (int atk, int crit, out bool temp) {
        temp = Random.value < crit / 100;
        if (temp) return atk *= 2;
        return atk;
    }
}