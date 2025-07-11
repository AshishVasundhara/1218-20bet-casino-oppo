using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;

public class MainMenuController : MonoBehaviour, IPopUp {


    public Button[] buttons;

    bool showed;

    public void CloseHandler()
    {
        showed = false;
        closeD(gameObject);
    }
    public void OpenHandler()
    {
        showed = true;
        openD(gameObject);
        SetControlActivity(true);
    }
    public void CloseButton_Click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GetComponent<GuiFader_v2>().FadeOut(0f, () => { CloseHandler();  });
    }
    public void NewGameButton_Click()
    {
        SetControlActivity(false);
        SlotPlayer.Instance.SetStartSettings();
        SlotController.Instance.ReStartSettings();
        GuiController.Instance.RefreshGui();
        GetComponent<GuiFader_v2>().FadeOut(0f, () => { CloseHandler(); });
    }
    public void ResumeButton_Click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GetComponent<GuiFader_v2>().FadeOut(0f, () => { CloseHandler(); });
    }

    public void SettingsButton_Click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        SettingsMenuController sM = FindObjectOfType<SettingsMenuController>();
       sM.OpenHandler();
       sM.GetComponent<GuiFader_v2>().FadeIn(0, () => { GetComponent<GuiFader_v2>().FadeOut(0f, () => { CloseHandler(); }); });
    }

    public void ExitButton_Click()
    {
        Application.Quit();
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

    public void SetControlActivity(bool activity)
    {
        foreach (Button b in buttons)
        {
            b.interactable = activity;
        }
    }

}
