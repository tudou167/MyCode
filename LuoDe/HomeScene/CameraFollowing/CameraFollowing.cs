using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class CameraFollowing : MonoBehaviour {
    public GameObject play;
    public float x, y, z;
  public   AudioSource sound;
   public  AudioClip music;
    void Start () {

        voice();
        Music = voice;
    }
    public UnityAction Music;
    private void LateUpdate()
    {
        Vector3 ck = play.transform.forward * x;
        Vector3 up = Vector3.up * y;
        Vector3 c = ck + up;
        Vector3 pos = play.transform.position + c;
        transform.position = Vector3.Lerp(transform.position, pos, z);
        //transform.LookAt
        Quaternion.LookRotation(play.transform.position);
    }
    public void voice()
    {
        sound = transform.GetComponent<AudioSource>();
        music= Resources.Load<AudioClip>("Voice/BackgroundMusic/Bandari");
        sound.clip = music;
        sound.Play();
        sound.volume = 1f;
      }
}
