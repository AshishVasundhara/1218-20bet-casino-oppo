using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class MenuHandler : MonoBehaviour {


    public Sprite[] Sound_images;
    public GameObject sound;

	// Use this for initialization
	void Start () {
        PlayerPrefs.DeleteAll();
        PlayerPrefs.SetInt("Sound", 0);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void click_on_play()
    {
        SceneManager.LoadSceneAsync("SlotGame");
    }

    public void SoundToggle()
    {
        if (PlayerPrefs.GetInt("Sound", 0) == 0)
        {
            //set off
            PlayerPrefs.SetInt("Sound", 1);
            sound.GetComponent<Image>().sprite = Sound_images[0];
            //SoundManager.instance.music.Stop();
            print("off");

        }
        else
        {
            //set on
            print("on");
            PlayerPrefs.SetInt("Sound", 0);
            sound.GetComponent<Image>().sprite = Sound_images[1];
           // SoundManager.instance.Bg_music_Play();
            SoundManager.instance.Play_click_btn_sound();
        }
    }
}
