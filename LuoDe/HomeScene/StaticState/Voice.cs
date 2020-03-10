using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Voice : MonoBehaviour {
    public enum MyEnum
    {
        music1,
        music2,
        music3,
        music4,
        music5,
        music6,
        music7,
    }
    public float MusicSlider = 1;
    public float FunctionSlider = 1;
    public float ImageQuality = 1;
    // AudioSource[] voice;
    public UnityAction Play;
    public int yy = 1;
   public AudioClip[] music;
  public  AudioSource voice, voices;
    public MyEnum Music = MyEnum.music7;
    void Start () {
        //music = new AudioClip[6];
        //AddVoice();
        //voice = transform.gameObject.AddComponent<AudioSource>();
        //voices = voice.GetComponent<AudioSource>();
        //voices.loop = true;
        //voices.playOnAwake = false;
        //Play = PlayVoice;
        soundEffect = transform.GetComponent<AudioSource>();
           
  }
    GameObject mainCamera;
    AudioSource sound, soundEffect;
    private void Update()
    {
        if (!mainCamera)
        {
        mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        }
        if (!sound)
        {
            sound =  mainCamera.GetComponent<AudioSource>();
        }
        if (sound != null)
        {
        sound.volume = MusicSlider;
        }
        if (soundEffect != null)
        {
            soundEffect.volume = FunctionSlider;
          
        }

    }
    public void  AddVoice()
    {
        //music[0] = Resources.Load<AudioClip>("Voice/BackgroundMusic/Bandari");
        //music[1] = Resources.Load<AudioClip>("Voice/BackgroundMusic/HOYO-MiX - ACE");
        //music[2] = Resources.Load<AudioClip>("Voice/BackgroundMusic/The xx - Intro");
        //music[3] = Resources.Load<AudioClip>("Voice/BackgroundMusic/Zoe _ JODODO - Pre-Pro");
        //music[4] = Resources.Load<AudioClip>("Voice/BackgroundMusic/Zoe _ JODODO - Rage");
        //music[5] = Resources.Load<AudioClip>("Voice/BackgroundMusic/DrumLoop1mcx20070418");
        //music[6] = Resources.Load<AudioClip>("Voice/BackgroundMusic/Color-X 3D");
   }
    public void PlayVoice()
    {
        //if (Music == MyEnum.music1)
        //{
        //    voice.clip = music[0];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music2)
        //{
        //    voice.clip = music[1];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music3)
        //{
        //    voice.clip = music[2];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music4)
        //{
        //    voice.clip = music[3];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music5)
        //{
        //    voice.clip = music[4];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music6)
        //{
        //    voice.clip = music[5];
        //    voice.Play();
        //}else
        //if (Music == MyEnum.music7)
        //{
        //    voice.clip = music[6];
        //    voice.Play();
        //}
    }
	
}
