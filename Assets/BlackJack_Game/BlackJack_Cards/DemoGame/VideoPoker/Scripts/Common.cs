using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using Holoville.HOTween;
using UnityEngine.SceneManagement;
//ads
using UnityEngine.Advertisements;
public class Common : MonoBehaviour
{
	public Button VideoAdsBtn;
    public static int countNumberBet = 0;
    public GameObject overlay, overlayMoney;
    public List<int> valueOri;
    public Sprite[] CARDS;
    public static List<Sprite> cardsOut;
    public EaseType easeTypeGoTo = EaseType.EaseInOutBack;
    public Transform target;
    public static Common common;
    public static List<int> holdList;
    public static int CountDraw = 0;
	public GameObject OptionsObj;
	public GameObject ExitGameObj;
	public GameObject TapPlayObj;

    void Start() {
        cardsOut = new List<Sprite>();
        holdList = new List<int>();
        common = this;
        countNumberBet = 0;
    }
	void Update()
	{
        //ads
        //if (Advertisement.IsReady("rewardedVideo"))
        //{
        //    VideoAdsBtn.interactable = true;
        //}
        //else
        //    VideoAdsBtn.interactable = false;
    }
    public void IncreaseCount() {

        countNumberBet++;
        print(countNumberBet);
    }
	public void LoadHomeScene()
	{
        //show ads
        //AdmobBannerController.Instance.ShowInterstitial();
        //AdManagerUnity.Instance.ShowAd("video");
        SoundController.Sound.ClickBtn ();
		SceneManager.LoadScene ("HomeScene");
	}

    public void QuitGame() 
	{
        Application.Quit();
    }
    bool isOpen = false;
    public void OpenPopup(Animator animator) {
        RotateCard.rotateCard.txtTabDraw.SetActive(false);
        if (!isOpen) {
            overlay.SetActive(true);
            animator.Play("zoom out");
            isOpen = true;
        }
        else {
            animator.Play("zoom in");
            overlay.SetActive(false);
            isOpen = false;
        }
    }
    public void OnOff(GameObject go) {
        if (go.activeInHierarchy) {
            go.SetActive(false);
			SoundController.Sound.ClickBtn ();
        }
    }

    public void OpenPopupUpDown(Animator animator) {
        if (!isOpen) {
            overlay.SetActive(true);
            animator.Play("godownup");
            isOpen = true;
        }
        else {
            animator.Play("goupdown");
            overlay.SetActive(false);
            isOpen = false;
        }
    }
    Vector3 oriPos = Vector3.zero;
    public void Move(Transform tr) 
	{

        if (!isOpen)
		{
            if (tr.name == "Quit Game") 
			{
				
            }
            overlay.SetActive(true);
            oriPos = tr.position;
            TweenParms parms = new TweenParms().Prop("position", target.position).Ease(easeTypeGoTo);
            HOTween.To(tr, .7f, parms);
            isOpen = true;
			ExitGameObj.SetActive (true);
			OptionsObj.SetActive (true);
        }
        else 
		{
            overlay.SetActive(false);
            TweenParms parms = new TweenParms().Prop("position", oriPos).Ease(easeTypeGoTo).OnComplete(OnComplete);
            HOTween.To(tr, .7f, parms);

			ExitGameObj.SetActive (false);
			OptionsObj.SetActive (false);
        }
		TapPlayObj.SetActive (false);

    }

    void OnComplete() {
        isOpen = false;
    }
	public void ShowUnityAds()
	{
		if (DataManager.Instance.Coins <= 0) {
			SoundController.Sound.ClickBtn ();
            //show ads
            //if (DataManager.Instance._rewardAdsType == DataManager.RewardedVideoAdsType.UnityAds)
            //    //AdManagerUnity.Instance.ShowAd("rewardedVideo");
            //else
            //    AdmobBannerController.Instance.ShowRewardedAd();
        } else
			SoundController.Sound.DisactiveButtonSound ();
	}

}
