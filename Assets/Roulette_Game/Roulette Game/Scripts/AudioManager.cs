using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using System;

public class AudioManager : MonoBehaviour {

    static AudioManager instance;

    AudioSource audioSource;
    private bool rolling;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        audioSource = gameObject.GetComponent<AudioSource>();
    }

    public void PlayButtonSound(float volume)
    {
        Play("press_but", volume);
    }

    public void Play(string clipName, float volume)
    { 
        AudioClip clip = Resources.Load<AudioClip>(clipName);
        audioSource.clip = clip;
        audioSource.volume = volume;
        audioSource.Play();
    }
	
    public static AudioManager getInstance()
    {
        return instance;
    }

    internal void BallStopped()
    {
        if (rolling)
        {
            rolling = false;
            audioSource.Stop();
        }
    }

    internal void BallRolling()
    {
        if (!rolling)
        {
            Play("wheel_sound", 0.2f);
        }
        rolling = true;
    }
}
