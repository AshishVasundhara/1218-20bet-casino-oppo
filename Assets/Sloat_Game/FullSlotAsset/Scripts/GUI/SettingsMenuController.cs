using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class SettingsMenuController : MonoBehaviour, IPopUp {

    public Text soundButtonText;
    public Text musikButtonText;

    public Button soundbtn;
    public Button musicbtn;

    public Sprite MusicOn, MusicOFF, SoundOn, SoundOff;

    public Button[] buttons;

    bool showed;

    public void SoundButton_Click()
    {
        SoundMasterController.Instance.soundOn = !SoundMasterController.Instance.soundOn;
        SetSoundButtText();
    }

    public void MusikButton_Click()
    {
        SoundMasterController.Instance.musicOn = !SoundMasterController.Instance.musicOn;
        SetMusicButtText();
        SoundMasterController.Instance.PlayBkgMusik(SoundMasterController.Instance.musicOn);
    }

    public void DevelopButton_Click()
    {

    }

    public void FacebookButton_Click()
    {

    }

    public void CloseButton_click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GuiController.Instance.RefreshGui();
        GetComponent<GuiFader_v2>().FadeOut(0, ()=> { CloseHandler(); });
    }

    public void CloseHandler()
    {
        showed = false;
        closeD(gameObject);
    }

    public void OpenHandler()
    {
        SetControlActivity(true);
        showed = true;
        openD(gameObject);
        SetSoundButtText();
        SetMusicButtText();
    }

    public void SetControlActivity(bool activity)
    {
        foreach (Button b in buttons)
        {
            b.interactable = activity;
        }
    }

    void SetSoundButtText()
    {
        if (SoundMasterController.Instance.soundOn)
        {
            //soundButtonText.text = "on";
            soundbtn.GetComponent<Image>().sprite = SoundOn;
        }
       else
        {
            //soundButtonText.text = "off";
            soundbtn.GetComponent<Image>().sprite = SoundOff;
        }
    }

    void SetMusicButtText()
    {
        if (SoundMasterController.Instance.musicOn)
        {
            //musikButtonText.text = "on";
            musicbtn.GetComponent<Image>().sprite = MusicOn;
        }
        else
        {
            //musikButtonText.text = "off";
            musicbtn.GetComponent<Image>().sprite = MusicOFF;

        }
    }

    public bool IsShowed()
    {
        return showed;
    }

    public GameObject getGameObject()
    {
        return gameObject;
    }

    Action<GameObject> openD;
    Action<GameObject> closeD;
    public void PopUpInit(Action<GameObject> openDel, Action<GameObject> closeDel)
    {
        if (openDel != null) openD = openDel;
        else
        {
            openD = new Action<GameObject>((gameObject) => { });
        }

        if (closeDel != null) closeD = closeDel;
        else
        {
            closeD = new Action<GameObject>((gameObject) => { });
        }
    }

}
