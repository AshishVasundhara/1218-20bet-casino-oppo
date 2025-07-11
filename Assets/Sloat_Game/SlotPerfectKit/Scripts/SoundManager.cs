using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    public AudioSource music, sound ;

    public AudioClip click_btn_sound, win_sound, Reel_end_sound , spin_sound , spin_start_sound ;
    public static SoundManager instance;
    // Use this for initialization
    void Start()
    {

        if (instance == null)
        {
            instance = this;

        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Play_click_btn_sound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            sound.PlayOneShot(click_btn_sound);
        }
    }

    public void Play_Reel_end_sound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            sound.PlayOneShot(Reel_end_sound);
        }
    }

    public void Play_spin_sound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            sound.PlayOneShot(spin_sound);
        }
    }

    public void Play_win_sound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            sound.PlayOneShot(win_sound);
        }
    }


    public void Play_spin_start_sound()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            sound.PlayOneShot(spin_start_sound);
        }
    }

    public void Stop_spin_sound()
    {
        sound.Stop();

    }






  


}
