using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System;
using System.Collections.Generic;

public class GuiController : MonoBehaviour
{

    [Space(8, order = 0)]
    [Header("Controllers", order = 1)]
    public WarningMessController wMC;
    public MainMenuController mMC;
    public SlotMenuController slotMenuC;
    public PayTableController payTableC;

    [Space(8, order = 0)]
    [Header("Refresh handlers", order = 1)]
    public UnityEvent GUIrefreshers;

    public List<GameObject> popUpsL;
    public static GuiController Instance;

    void Awake()
    {
        Application.targetFrameRate = 35;
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        popUpsL = new List<GameObject>();
        IPopUp[] popUps = gameObject.GetInterfaces<IPopUp>();
        for (int i = 0; i < popUps.Length; i++)
        {
            popUps[i].PopUpInit(new Action<GameObject>(PopUpOpenH), new Action<GameObject> (PopUpCloseH));
        }
    }

    private void PopUpOpenH(GameObject gO)
    {
        if (popUpsL.IndexOf(gO) == -1)
        {
            popUpsL.Add(gO);
        }
    }

    private void PopUpCloseH(GameObject gO)
    {
        if (popUpsL.IndexOf(gO) != -1)
        {
            popUpsL.Remove(gO);
        }
    }

    internal bool HasNoPopUp
    {
        get { return popUpsL.Count > 0; }
    }

    internal void RefreshGui()
    {
        GUIrefreshers.Invoke();
    }
}

public interface IPopUp
{
    bool IsShowed();
    GameObject getGameObject();
    void PopUpInit(Action<GameObject>openDel, Action <GameObject> closeDel);
    void OpenHandler();
    void CloseHandler();
    void SetControlActivity(bool activity);
}


 
