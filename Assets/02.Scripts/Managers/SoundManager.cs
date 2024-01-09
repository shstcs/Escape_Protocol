using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private AudioSource _audio;
    public void SetBGM()
    {
        _audio = GameObject.Find("@Main").GetComponent<AudioSource>();
        if( _audio != null)
        {
            _audio.clip = Resources.Load<AudioClip>("Sounds\\BGM");
        }
    }

    public void StartBGM()
    {
        _audio.Play();
    }
}
