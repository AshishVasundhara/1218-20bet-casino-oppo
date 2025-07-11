using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using System.Collections;
using TMPro;

public class SlotMenuController : MonoBehaviour {

    public Button[] buttons;
    //public Button StartBtn;
    public GameObject[] menuElements;

    public TextMeshProUGUI BetCount;
    public TextMeshProUGUI MoneyCount;
    public TextMeshProUGUI MoneyName;
    public Text winLetterText;
    public Text freeSpin;
    public Text spinCountText;
    public Text infoText;


    public static SlotMenuController Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    internal void SetActive(bool activity)
    {
        foreach (GameObject gO in menuElements)
        {
            gO.SetActive(activity);
        }
    }

    public void SetControlActivity(bool activity)
    {
        foreach (Button b in buttons)
        {
            b.interactable = activity;
            //StartBtn.interactable = activity;
        }
    }

    internal void Refresh()
    {
        MoneyCount.text = SlotPlayer.Instance.Coins.ToString();
        //MoneyName.text = GetMoneyName(SlotPlayer.Instance.Coins);
        BetCount.text = SlotPlayer.Instance.Bet.ToString();
        freeSpin.gameObject.SetActive(SlotPlayer.Instance.FreeSpins > 0);
        spinCountText.text = (SlotPlayer.Instance.FreeSpins > 0) ? SlotPlayer.Instance.FreeSpins.ToString() : "";
        infoText.text = (SlotPlayer.Instance.Bet > 0) ? "Total bet "+ SlotPlayer.Instance.Bet.ToString() +". Click to SPIN to start!" : "Select any slot line to start!";
    }
    public void MainMenu_Click()
    {
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GuiController.Instance.mMC.OpenHandler();
        GuiController.Instance.mMC.GetComponent<GuiFader_v2>().FadeIn(0, null);
    }
    public void PayTable_Click()
    {
        SoundMasterController.Instance.SoundPlayPopUp(0.2f, null);
        GuiController.Instance.payTableC.OpenHandler();
        GuiController.Instance.payTableC.GetComponent<GuiFader_v2>().FadeIn(0, null);
    }

    public void StartButton_Click()
    {
        SlotController.Instance.RunSlots();
    }

    private string GetMoneyName(int count)
    {
        if (count > 1) return "coins";
        else return "coin";
    }
}
