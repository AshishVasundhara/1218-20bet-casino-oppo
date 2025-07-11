using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Holoville.HOTween;
using UnityEngine.SceneManagement;

public class HomePopup : MonoBehaviour
{
    public Text CoinTex;
    public Text CoinTex1;
    public GameObject OptionsObj;
    public GameObject ExitGameObj;
    public GameObject ShopGameObj;
    public EaseType easeTypeGoTo = EaseType.EaseInOutBack;
    public Transform target;
    public GameObject overlay;
    public GameObject SpinObj;

    Vector3 oriPos = Vector3.zero;
    bool isOpen = false;
    void Start()
    {
        SpinObj.SetActive(false);
    }
    void Update()
    {
        CoinTex.text = "" + DataManager.Instance.Coins;
        CoinTex1.text = "" + DataManager.Instance.Coins;
    }

    public void Move(Transform tr)
    {
        if (!isOpen)
        {
            if (tr.name == "Quit Game")
            {
                ExitGameObj.SetActive(true);
                OptionsObj.SetActive(false);
                ShopGameObj.SetActive(false);
                //show ads
                //AdmobBannerController.Instance.ShowInterstitial();
                //AdManagerUnity.Instance.ShowAd("video");
            }

            if (tr.name == "Setting")
            {
                ExitGameObj.SetActive(false);
                OptionsObj.SetActive(true);
                ShopGameObj.SetActive(false);
            }

            if (tr.name == "IAP")
            {
                ExitGameObj.SetActive(false);
                OptionsObj.SetActive(false);
                ShopGameObj.SetActive(true);
            }

            overlay.SetActive(true);
            oriPos = tr.position;
            TweenParms parms = new TweenParms().Prop("position", target.position).Ease(easeTypeGoTo);
            HOTween.To(tr, .7f, parms);
            isOpen = true;
            SoundController.Sound.PopupShow();
        }
        else
        {
            SoundController.Sound.CloseBtn();
            overlay.SetActive(false);
            TweenParms parms = new TweenParms().Prop("position", oriPos).Ease(easeTypeGoTo).OnComplete(OnComplete);
            HOTween.To(tr, .7f, parms);
            ExitGameObj.SetActive(false);
            OptionsObj.SetActive(false);
            ShopGameObj.SetActive(false);
        }
    }

    void OnComplete()
    {
        isOpen = false;
    }

    public void VideoPokerScene()
    {
        SoundController.Sound.ClickBtn();
        SceneManager.LoadScene("JackOrBetter");
        MusicController.Music.BG_VideoPoker();
    }

    public void BlackJackScene()
    {
        MusicController.Music.BG_BlackJack();
        SoundController.Sound.ClickBtn();
        SceneManager.LoadScene("Blackjack");
    }

    public void SpinScene()
    {
        SpinObj.SetActive(true);
        SoundController.Sound.PopupShow();
    }

    public void SpinToHomeScene()
    {
        SoundController.Sound.CloseBtn();
        SceneManager.LoadScene("HomeScene");
    }

    public void CloseLuckyWheel()
    {
        SoundController.Sound.CloseBtn();
    }

    public void ShowUnityAds()
    {
        SoundController.Sound.ClickBtn();
        FreeCoinRewardUI.isFreeAd = 1;
        //show ads
        //AdManagerUnity.Instance.ShowAd ("rewardedVideo");
    }

    public void QuitGame()
    {
        Application.Quit();
        //UnityEditor.EditorApplication.isPlaying = false;
    }
}
