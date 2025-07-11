using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using TMPro;

public enum WarnMessType { NoOkClose, Ok,  OkClose}
public class WarningMessController : MonoBehaviour, IPopUp {

    public GuiController guC;
    public TextMeshProUGUI Messagetxt;
    private string textEn;
    public Button buttOk;
    public Button buttClose;


	private string locString;
    private Action okButDel;
    private Action closeButDel;
    bool showed;
    private WarnMessType wmType;

    public void OpenHandler()
    {
        SetControlActivity(true);
        showed = true;
        if(openD!=null) openD(gameObject);
        locString = textEn;
        Messagetxt.text = locString;
        Refresh();
    }

    public void CloseHandler()
    {
        showed = false;
        if (closeD != null) closeD(gameObject);
    }

    public void ShowMessage(string st)
    {
        wmType = WarnMessType.NoOkClose;
        SetControlActivity(false);
        textEn = st;
        OpenHandler();
        GetComponent<GuiFader_v2>().FadeIn(0,()=> { SetControlActivity(true); });
    }

    public void ShowMessage(string st, Action okClickCallBack)
    {
        wmType = WarnMessType.Ok;
        SetControlActivity(false);
        textEn = st;
        okButDel = okClickCallBack;
        OpenHandler();
        GetComponent<GuiFader_v2>().FadeIn(0, () => { SetControlActivity(true); });
    }

    public void ShowMessage(string st, Action okClickCallBack, Action closeClickCallback)
    {
        wmType = WarnMessType.OkClose;
        closeButDel = closeClickCallback;
        okButDel = okClickCallBack;
        SetControlActivity(false);
        textEn = st;
        OpenHandler();
        GetComponent<GuiFader_v2>().FadeIn(0, () => { SetControlActivity(true); });
    }

    public void HideMessage(Action callBack)
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GetComponent<GuiFader_v2>().FadeOut(0, () => { CloseHandler(); if (callBack != null) callBack(); });
    }

    public void ButtonOk_Click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayClick(0, null);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        if (okButDel != null) okButDel();
        GetComponent<GuiFader_v2>().FadeOut(0,()=> { CloseHandler(); });
    }

    public void ButtonClose_Click()
    {
        SetControlActivity(false);
        SoundMasterController.Instance.SoundPlayClick(0, null);
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        if (closeButDel != null) closeButDel();
        GetComponent<GuiFader_v2>().FadeOut(0, ()=> { CloseHandler(); });
    }

    public void SetControlActivity(bool activity)
    {
        buttOk.interactable = activity;
        buttClose.interactable = activity;
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

    void Refresh()
    {
        switch (wmType)
        {
            case WarnMessType.NoOkClose:
                buttOk.gameObject.SetActive(false);
                buttClose.gameObject.SetActive(false);
                break;
            case WarnMessType.Ok:
                buttOk.gameObject.SetActive(true);
                buttClose.gameObject.SetActive(false);
                break;
            case WarnMessType.OkClose:
                buttOk.gameObject.SetActive(true);
                buttClose.gameObject.SetActive(true);
                break;
        }
    }

}
