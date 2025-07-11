using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public enum kindChose
{
    none,
    black,
    red,
    spade,
    heart,
    club,
    diamond,
}
public class DoubleUp : MonoBehaviour
{
    public GameObject doubleUpPanel, effectCard;
    public GameObject[] helpBlinkArr;
    public ParticleSystem particle;
    public Sprite spade, club, heart, diamond, douldZeroCard;
    public Sprite spadeCross, clubCross, heartCross, diamonCross;
    public Image[] cardDoubleUp;
    public static kindChose kindChose = global::kindChose.none;
    public static DoubleUp doubleUp;
    public Image imgOpenDoubleUp;
	public GameObject DoubleUpImgObj;
    Sprite sprOpenDoubleUp;

    public Text txtTake, txtX2, txtX4;
    public static kindChose result = global::kindChose.none;
    void Start() 
	{
		DoubleUpImgObj.SetActive (false);
        sprOpenDoubleUp = imgOpenDoubleUp.sprite;
        doubleUp = this;
    }

    public void Black() 
	{
        kindChose = global::kindChose.black;
        selected = true;
        effectCard.SetActive(true);
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }

    public void Red() {
        kindChose = global::kindChose.red;
        selected = true;
        effectCard.SetActive(true);
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }

    public void Spade() {
        effectCard.SetActive(true);
        kindChose = global::kindChose.spade;
        selected = true;
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }
    public void Club() {
        effectCard.SetActive(true);
        kindChose = global::kindChose.club;
        selected = true;
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }

    public void Heart() {
        effectCard.SetActive(true);
        kindChose = global::kindChose.heart;
        selected = true;
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }

    public void Diamond() {
        effectCard.SetActive(true);
        kindChose = global::kindChose.diamond;
        selected = true;
        foreach (GameObject go in helpBlinkArr) {
            go.SetActive(false);
        }
    }

    int count = 0;
	int sumDoubleWin =0;
    bool losed = false;
    public void OpenCard() {
        if (count == 4) {
            CloseDoubleUpPanel(1.5f);
        }
        if (imgOpenDoubleUp.sprite.name != "dbzerocard" || losed) return;
        StartCoroutine(DownCard());
        if (count > 4) return;
        if (kindChose == global::kindChose.none) return;
        int i = Random.Range(0, 51);
        string str = Common.common.CARDS[i].name;
        effectCard.SetActive(false);
        cardDoubleUp[count].gameObject.SetActive(true);
        if (str.Contains("s")) {
            cardDoubleUp[count].sprite = spadeCross;
            imgOpenDoubleUp.sprite = spade;
            if (kindChose == global::kindChose.black) {
                result = global::kindChose.black;  
            }
            else if (kindChose == global::kindChose.spade) {
                result = global::kindChose.spade; 
            }
        }
        else if (str.Contains("c")) {
            imgOpenDoubleUp.sprite = club;
            cardDoubleUp[count].sprite = clubCross;
            if (kindChose == global::kindChose.black) {
                result = global::kindChose.black; 
            }
            else if (kindChose == global::kindChose.club) {
                result = global::kindChose.club; 
            }
        }

        else if (str.Contains("d")) {
            imgOpenDoubleUp.sprite = diamond;
            cardDoubleUp[count].sprite = diamonCross;
            if (kindChose == global::kindChose.red) {
                result = global::kindChose.red;  
            }
            else if (kindChose == global::kindChose.diamond) {
                result = global::kindChose.diamond;
            }
        }
        else if (str.Contains("h")) {
            imgOpenDoubleUp.sprite = heart;
            cardDoubleUp[count].sprite = heartCross;
            if (kindChose == global::kindChose.red) {
                result = global::kindChose.red;  
            }
            else if (kindChose == global::kindChose.heart) {
                result = global::kindChose.heart;
            }
        }
		Debug.Log (""+count);
		count++;
        if (kindChose == result) 
		{
			
            int bet = int.Parse(RewadControl.reward.txtShowBet.text);
            if (kindChose == global::kindChose.red || kindChose == global::kindChose.black) 
			{
				//2^count
				if (count == 0) 
				{
					sumDoubleWin = (RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 1) 
				{
					sumDoubleWin = (2 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}

				if (count == 2) 
				{
					sumDoubleWin = (4 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
			     }
				

				if (count == 3) 
				{
					sumDoubleWin = (8 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
		
				}
				if (count == 4) 
				{
					sumDoubleWin = (16 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 5) 
				{
					sumDoubleWin = (32 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
            }
            else
			{
				//4^count
				if (count == 0) 
				{
					sumDoubleWin = (RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 1) 
				{
					sumDoubleWin = (4 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 2) 
				{
					sumDoubleWin = (46 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}

				if (count == 3) 
				{
					sumDoubleWin = (64 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 4) 
				{
					sumDoubleWin = (256 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
				if (count == 5) 
				{
					sumDoubleWin = (1024 * RewadControl.reward.valueRewardOri);
					txtTake.text = (sumDoubleWin).ToString ();	
				}
            }
            RewadControl.reward.ShowWin ("+" +sumDoubleWin+"$", RewadControl.reward.winAnimator);
			txtX2.text = (2 * sumDoubleWin).ToString ();
			txtX4.text = (4* sumDoubleWin).ToString ();
			SoundController.Sound.VideoPoker_Win ();
            selected = false;
            StartCoroutine(BlinkHelp());
            particle.Play();
			SoundController.Sound.VideoPoker_MoreCoin ();
        }
        else {
            RewadControl.reward.ShowWin("You lose", RewadControl.reward.loseAnimator);
            losed = true;
            sumDoubleWin = 0;
			txtTake.text = "0"+"$";
            StartCoroutine(CloseDoubleUpPanel(1.6f));
			SoundController.Sound.VideoPoker_Lose ();
        }
        kindChose = global::kindChose.none;
        result = global::kindChose.none;
    }

    IEnumerator DelayOpenCard() {
        yield return new WaitForSeconds(1);
    }
    IEnumerator CloseDoubleUpPanel(float time) 
	{
		
        yield return new WaitForSeconds(time);
        count = 0;
      
        foreach (Image img in cardDoubleUp) {
            img.gameObject.SetActive(false);
        }
        doubleUpPanel.gameObject.SetActive(false);
		int moneyshow = DataManager.Instance.Coins;
        if (losed) 
		{
            RewadControl.reward.txtMoney.text = "" + DataManager.Instance.Coins;
        }
        else 
		{
            RewadControl.reward.txtMoney.text = "" + DataManager.Instance.Coins;
        }


        RewadControl.reward.rewardRule = RewardRulee.NONE;
        selected = false;
        losed = false;
        StartCoroutine(RewadControl.reward.DelayResetLose(.5f));
		DoubleUpImgObj.SetActive (false);
    }


    IEnumerator DownCard() {
        yield return new WaitForSeconds(2.5f);
        imgOpenDoubleUp.sprite = douldZeroCard;
    }
    public static int adid = 0;
    bool selected = false;
    public void OpenDoubleUpPanel() 
	{
        RewadControl.reward.doubleUpAnimator.Play("zoom in");
		txtX2.text = (RewadControl.reward.valueRewardOri * 2).ToString();
		txtX4.text = (RewadControl.reward.valueRewardOri* 4).ToString();
        Common.common.overlay.SetActive(false);
        doubleUpPanel.gameObject.SetActive(true);
        StartCoroutine(BlinkHelp());
		DataManager.Instance.RemoveCoins (RewadControl.reward.valueRewardOri);
    }

    IEnumerator BlinkHelp() {
        yield return new WaitForSeconds(4);
        if (!selected && doubleUpPanel.activeInHierarchy) {
            foreach (GameObject go in helpBlinkArr) {
                go.SetActive(true);
            }
        }
    }
    public void CloseDoubleUp() {
        RewadControl.reward.doubleUpAnimator.Play("zoom in");
        RewadControl.reward.ShowScore();
        Common.common.overlay.SetActive(false);
        StartCoroutine(RewadControl.reward.DelayResetLose(1.1f));
		DoubleUpImgObj.SetActive (false);
    }

    public void Take() 
	{
        StartCoroutine(CloseDoubleUpPanel(.2f));
		DoubleUpImgObj.SetActive (false);
		DataManager.Instance.AddCoins (int.Parse(txtTake.text));
    }
}
