using UnityEngine;
using System.Collections;
using Holoville.HOTween;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;

public enum StageDealAndDraw
{
    none,
    deal,
    draw,
    inprocess,
    end,

}
public class RotateCard : MonoBehaviour
{
	public GameObject DealBtn;
	public GameObject DrawBtn;
    public GameObject finger, txtTabDraw;
    public GameObject overlayBet;
    public Animator animatorDealBtn;
    public float time = 1.3f;
    public float timeDelay = 0;
    public EaseType easeTypeGoTo = EaseType.EaseInBack;
    public GameObject[] holds;
    public Sprite cardZero, draw;
    Sprite deal;
    public Image btnDeal;
    public StageDealAndDraw st = StageDealAndDraw.deal;
    public static RotateCard rotateCard;
	public Text TutText;
	public Button BetButton;
    void Start() 
	{
        rotateCard = this;
        deal = btnDeal.sprite;
		TutText.text = "WELCOME -TAP DEAL TO START!";
    }
	void Update()
	{
		if (st != StageDealAndDraw.deal) 
		{
			BetButton.interactable = false;
		} else
		{
			BetButton.interactable = true;
		}
	}


    int countClickDraw = 0;
	public void Show_Bar()
	{
		SliderScript.sliderScipt.ClickBet();
	}
    public void Draw() 
	{
			print (st);
			if (SliderScript.sliderScipt.parentSlider.localScale.y > .9f) 
			{
				SliderScript.sliderScipt.ClickBet ();
			}
			if (st == StageDealAndDraw.inprocess)
				return;
		if (st == StageDealAndDraw.deal) 
		{
			if (DataManager.Instance.Coins <= 0) {
				finger.SetActive (true);
				Common.common.overlayMoney.SetActive (true);
                RewadControl.reward.txtShowBet.text = "1";
			} else {
				DataManager.Instance.RemoveCoins (int.Parse (RewadControl.reward.txtShowBet.text));
				TutText.text = "TAP CARD TO HOLD AND DRAW TO SHOW RESULT!";
				DealBtn.SetActive (false);
				DrawBtn.SetActive (true);
				txtTabDraw.SetActive (false);
				if ((int.Parse (RewadControl.reward.txtMoney.text) - (int.Parse (RewadControl.reward.txtShowBet.text)) < 0)) {
                    RewadControl.reward.txtShowBet.text = RewadControl.reward.txtMoney.text;
				}

				foreach (Transform tr in RewadControl.reward.parentCards) {
					if (!Common.holdList.Contains (int.Parse (tr.name)))
						tr.GetComponent<Image> ().sprite = cardZero;
				}

				if (countClickDraw == 10) 
				{
                    //show ads
                    //AdmobBannerController.Instance.ShowInterstitial();
                    //AdManagerUnity.Instance.ShowAd("video");
                    countClickDraw = 0;
				}
				countClickDraw++;
				overlayBet.SetActive (true);
				Common.holdList.RemoveRange (0, Common.holdList.Count);
				Common.cardsOut.RemoveRange (0, Common.cardsOut.Count);
				Common.cardsOut = Common.common.CARDS.ToList ();
                RewadControl.reward.txtMoney.text = (int.Parse (RewadControl.reward.txtMoney.text) - (int.Parse (RewadControl.reward.txtShowBet.text))).ToString ();

				st = StageDealAndDraw.inprocess;
				StartCoroutine (ChangeStage (StageDealAndDraw.draw, animatorDealBtn));
				foreach (Transform tr in RewadControl.reward.parentCards) {
				}
				for (int i = 0; i < 5; i++) {
					StartCoroutine (DelayRotation (RewadControl.reward.parentCards.GetChild (i), i));
				}
			} 
		}else if (st == StageDealAndDraw.draw) {
				TutText.text = "SHOW RESULT- GOOD LUCK!";
				DealBtn.SetActive (true);
				DrawBtn.SetActive (false);
				int i = 0;
				foreach (Transform tr in RewadControl.reward.parentCards) {
					if (!Common.holdList.Contains (int.Parse (tr.name))) {
						tr.GetComponent<Image> ().sprite = cardZero;
						StartCoroutine (DelayRotation2 (tr, i));
						i++;
					}
				}
				st = StageDealAndDraw.inprocess;
				StartCoroutine (EndRotation ());
			}
    }

    public IEnumerator FingerActive() 
	{
        yield return new WaitForSeconds(1.2f);
        finger.SetActive(true);
        Common.common.overlayMoney.SetActive(true);
    }
    public IEnumerator ChangeStage(StageDealAndDraw sttemp, Animator animatorDealBtn)
	{
        yield return new WaitForSeconds(time * 2f);
        st = sttemp;
        if (st == StageDealAndDraw.end) {
            st = StageDealAndDraw.deal;
            overlayBet.SetActive(false);
            StartCoroutine(SHOWTABDRAWPLAY());
        }
    }

    IEnumerator SHOWTABDRAWPLAY() {
        yield return new WaitForSeconds(4);
        if (st == StageDealAndDraw.deal && (int.Parse(RewadControl.reward.txtMoney.text)) > 0)
            txtTabDraw.SetActive(true);
    }
    public void RotateChangeCard(Transform tr) {

        if (Common.holdList.Count != 0 && Common.holdList.Contains(int.Parse(tr.name)) && st != StageDealAndDraw.deal) return;
        TweenParms parms = new TweenParms().Prop("rotation", Quaternion.Euler(new Vector3(0, 90, 0))).Ease(easeTypeGoTo).OnComplete(gameObject, "OnComplete", tr);
        HOTween.To(tr, time, parms);
    }

    void Rotate2(Transform tr) {
        TweenParms parms = new TweenParms().Prop("rotation", Quaternion.Euler(new Vector3(0, 90, 0))).Ease(easeTypeGoTo).OnComplete(gameObject, "OnComplete2", tr);
        HOTween.To(tr, time, parms);
    }

    void OnComplete2(Transform tr) {
        OnComplete(tr);
    }


    IEnumerator DelayRotation(Transform tr, int i) {
        yield return new WaitForSeconds(timeDelay * i);
        RotateChangeCard(tr);
    }
    IEnumerator DelayRotation2(Transform tr, int i) {
        yield return new WaitForSeconds(timeDelay * i);
        Rotate2(tr);
    }
    void OnComplete(Transform tr) {
		SoundController.Sound.VideoPoker_DealOrDrawCard ();
        int index = 0;
        if (Common.holdList.Count == 0) {
            index = Random.Range(0, Common.cardsOut.Count - 1);
            tr.GetComponent<Image>().sprite = Common.cardsOut[index];
            Common.cardsOut.RemoveAt(index);
        }
        else if (!Common.holdList.Contains(int.Parse(tr.name))) {
            index = Random.Range(0, Common.cardsOut.Count - 1);
            tr.GetComponent<Image>().sprite = Common.cardsOut[index];
            Common.cardsOut.RemoveAt(index);
        }
        if (st == StageDealAndDraw.end) {
            tr.GetComponent<Image>().sprite = cardZero;
        }

        TweenParms parms = new TweenParms().Prop("rotation", Quaternion.Euler(new Vector3(0, 0, 0))).Ease(easeTypeGoTo);
        HOTween.To(tr, time, parms);

    }

    IEnumerator EndRotation() {
        yield return new WaitForSeconds(1f + (5 - Common.holdList.Count) * timeDelay);
        st = StageDealAndDraw.end;
        RewadControl.reward.Result();
		TutText.text = "YOU WILL LUCKY IN NEXT TIME!!!";
    }

    public void Reset() {
        Common.holdList.RemoveRange(0, Common.holdList.Count);
        foreach (GameObject tr in holds) {
            if (tr.transform.localScale.x == 1)
                tr.GetComponent<Animator>().Play("zoom in");
        }
        foreach (Transform tr in RewadControl.reward.parentCards) {
            RotateChangeCard(tr);
        }
        StartCoroutine(ChangeStage(StageDealAndDraw.end, animatorDealBtn));
    }
    public void Hold(GameObject btn) {
        GameObject hold = holds[int.Parse(btn.name) - 1];
        if (hold.transform.localScale.x > .5f) {
            hold.GetComponent<Animator>().Play("zoom in");
            Common.holdList.RemoveAt(Common.holdList.IndexOf(int.Parse(btn.name)));
            return;
        }
        if (st == StageDealAndDraw.draw) {
            hold.GetComponent<Animator>().Play("zoom out");
            int btnName = int.Parse(btn.name);
            if (Common.holdList.Contains(btnName)) {
                return;
            }
            else {
                Common.holdList.Add(btnName);
                print(" add = " + btnName);
            }
        }
    }
}
