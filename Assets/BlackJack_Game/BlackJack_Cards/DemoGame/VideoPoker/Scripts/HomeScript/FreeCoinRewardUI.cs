using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using System;
using BooStudio;
//ads
using UnityEngine.Advertisements;

public class FreeCoinRewardUI : MonoBehaviour
{

    public GameObject dailyRewardBtn;
    public Text dailyRewardBtnText;
	public Text dailyRewardBtnText1;
	public Text dailyRewardBtnText2;
    public GameObject rewardUI;
	public GameObject rewardUIBG;
    Animator dailyRewardAnimator;
	public static int isFreeAd=0;
	public Text CountNumAds;

    // Use this for initialization
    void Start()
    {
		
		dailyRewardAnimator = dailyRewardBtn.GetComponent<Animator>();
    }
    // Update is called once per frame
    void Update()
    {
		CountNumAds.text = ""+DataManager.Instance.FreeAdNumber;
   //     if (!DailyRewardController1.Instance.disable && dailyRewardBtn.gameObject.activeSelf)
   //     {
   //         //ads
   ////         if (DailyRewardController1.Instance.CanRewardNow()&&Advertisement.IsReady("rewardedVideo"))
   ////         {
   ////             dailyRewardBtnText.text = "ACTIVE";
			////	dailyRewardBtnText1.text = "ACTIVE";
			////	dailyRewardBtnText2.text = "ACTIVE";
   ////             dailyRewardAnimator.SetTrigger("activate");
   ////         }
   ////         else
   ////         {
   ////             TimeSpan timeToReward = DailyRewardController1.Instance.TimeUntilReward;
   ////             dailyRewardBtnText.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
			////	dailyRewardBtnText1.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
			////	dailyRewardBtnText2.text = string.Format("{0:00}:{1:00}:{2:00}", timeToReward.Hours, timeToReward.Minutes, timeToReward.Seconds);
   ////             dailyRewardAnimator.SetTrigger("deactivate");
   ////         }
   ////         //ads
   ////         if (DailyRewardController1.Instance.CanRewardNow()&&!Advertisement.IsReady("rewardedVideo"))
			////{
			////	dailyRewardBtnText.text = "WAIT";
			////	dailyRewardBtnText1.text = "WAIT";
			////	dailyRewardBtnText2.text = "WAIT";
			////	dailyRewardAnimator.SetTrigger("deactivate");
			////}
   //     }
    }

    public void ShowStartUI()
    {

        ShowDailyRewardBtn();
    }

    void ShowDailyRewardBtn()
    {
        // Not showing the daily reward button if the feature is disabled
        if (!DailyRewardController1.Instance.disable)
        {
            dailyRewardBtn.SetActive(true);
        }
    }
	public void FreeCoinWatchAds()
	{
        //ads
  //      if (DailyRewardController1.Instance.CanRewardNow ()&&(Advertisement.IsReady ("rewardedVideo"))) 
		//{

		//	if (DataManager.Instance.FreeAdNumber <= 10 && DataManager.Instance.FreeAdNumber > 0) 
		//	{
		//		DataManager.Instance.RemoveFreeAdNumber (1);
		//		SoundController.Sound.ClickBtn ();
  //              //show ads
  //              //AdManagerUnity.Instance.ShowAd ("rewardedVideo");
		//		isFreeAd = 1;
		//	} else
		//		SoundController.Sound.DisactiveButtonSound ();
		//	if(DataManager.Instance.FreeAdNumber<=0)
		//	{
		//		DailyRewardController1.Instance.ResetNextRewardTime ();	
		//		DataManager.Instance.AddFreeAdNumber (10);
		//	}
		//	else SoundController.Sound.DisactiveButtonSound ();


		//} else 
			SoundController.Sound.DisactiveButtonSound ();
	}
	public void FreeAdsCallBack()
	{
		isFreeAd = 0;
		dailyRewardBtn.SetActive (false);

		float value = UnityEngine.Random.value;
		int reward = DailyRewardController1.Instance.GetRandomRewardCoins ();

		// Round the number and make it mutiplies of 5 only.
		int roundedReward = (reward / 5) * 5;
		// Show the reward UI
		ShowRewardUI (roundedReward);
		
	}


    public void ShowRewardUI(int reward)
    {
        rewardUI.SetActive(true);
		rewardUIBG.SetActive (true);
        rewardUI.GetComponent<RewardUIController>().Reward(reward);

        RewardUIController.RewardUIClosed += OnRewardUIClosed;
    }

    void OnRewardUIClosed()
    {
        RewardUIController.RewardUIClosed -= OnRewardUIClosed;
        dailyRewardBtn.SetActive(true);
    }

    public void HideRewardUI()
    {
		rewardUIBG.SetActive (false);
        rewardUI.GetComponent<RewardUIController>().Close();
    }

}
