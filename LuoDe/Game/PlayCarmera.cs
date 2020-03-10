using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayCarmera : MonoBehaviour
{
    Transform tf;
    Vector3 cameraPos;
    public AudioSource mv;
    public AudioClip music;
    void Awake()
    {
        cameraPos = new Vector3(0, 5, -3);
    }
    void Start()
    {
        tf = HeroManager.Instance.GetCurHero();
        transform.eulerAngles = new Vector3(55, 0, 0);
        music = Resources.Load<AudioClip>("Voice/BackgroundMusic/Zoe _ JODODO - Rage");
        mv = transform.gameObject.AddComponent<AudioSource>();
        mv.clip = music;
        mv.loop = true;
        mv.Play();
        mv.volume = 0.3f;
    }
    void LateUpdate()
    {
        transform.position = tf.position + cameraPos;
    }
}
