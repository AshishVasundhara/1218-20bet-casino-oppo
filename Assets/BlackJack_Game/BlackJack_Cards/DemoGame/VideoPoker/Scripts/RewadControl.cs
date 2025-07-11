using UnityEngine;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine.UI;

public enum RewardRulee
{
    NONE = 0,
    JOB = 1,
    TP = 2,
    TOK = 3,
    S = 4,
    F = 5,
    FH = 6,
    FOK = 7,
    SF = 8,
    RF = 9,
}
public class RewadControl : MonoBehaviour
{
    public ParticleSystem moneyEffect;
    public Transform parentCards;

    public static RewadControl reward;

    public Text txtShowBet;

    public Text txtMoney, txtWin;

    public Animator[] animatorKindReward;

    public Animator doubleUpAnimator, winAnimator, loseAnimator;
    [HideInInspector]
    public RewardRulee rewardRule = RewardRulee.NONE;

    void Awake()
    {
        reward = this;
        txtShowBet.text = "1";

    }
    void Update()
    {
        txtMoney.text = "" + DataManager.Instance.Coins;
    }

    public void Result()
    {
        List<int> listNumber = new List<int>();
        List<string> listKind = new List<string>();
        foreach (Transform tr in parentCards)
        {
            string str = tr.GetComponent<Image>().sprite.name;
            if (str.Length == 4)
            {
                listNumber.Add(int.Parse(str.Substring(0, 2)));
                listKind.Add(str.Substring(3, 1));
            }
            else
            {
                listNumber.Add(int.Parse(str.Substring(0, 1)));
                listKind.Add(str.Substring(2, 1));
            }
        }
        CheckReward(listNumber, listKind);
        CalculateMoney();
    }

    void CheckReward(List<int> listNumber, List<string> kind)
    {
        listNumber.Min();

        listNumber.Max();

        print("min =" + listNumber.Count(st => st == listNumber.Min()) + " " + listNumber.Min());
        print("max=" + listNumber.Count(st => st == listNumber.Max()) + " " + listNumber.Max());
        print("mid=" + listNumber.Count(st => st != listNumber.Min() & st != listNumber.Max()));

        int countMin = listNumber.Count(st => st == listNumber.Min());
        int countMax = listNumber.Count(st => st == listNumber.Max());
        int countMid = listNumber.Count(st => st != listNumber.Min() & st != listNumber.Max());

        IEnumerable<int> temp = listNumber.Where(st => st != listNumber.Min() & st != listNumber.Max());
        List<int> listTempInt = temp.ToList();

        if (kind.Distinct().Count() == 1 && listNumber.Max() == 14 && listNumber.Min() == 10)
        {
            print("ROYAL FLUSH");
            rewardRule = RewardRulee.RF;
            return;
        }

        if (kind.Distinct().Count() == 1 && (listNumber.Max() - listNumber.Min() == 4))
        {
            print("STRAIGHT FLUSH");
            rewardRule = RewardRulee.SF;
            return;
        }

        if (countMin == 4 || countMax == 4)
        {
            print("FOUR OF A KIND");
            rewardRule = RewardRulee.FOK;
            return;
        }
        if ((countMin == 3 && countMax == 2) || (countMax == 3 && countMin == 2))
        {
            print("FULL HOUSE");
            rewardRule = RewardRulee.FH;
            return;
        }

        if (kind.Distinct().Count() == 1)
        {
            print("FUSH");
            rewardRule = RewardRulee.F;
        }
        if (listNumber.Max() - listNumber.Min() == 4 && listNumber.Distinct().Count() == 5)
        {
            print("STRAIGHT");
            rewardRule = RewardRulee.S;
            return;
        }
        else if (countMid == 3)
        {
            if (listTempInt.Count != 0)
            {
                if (listTempInt.Distinct().Count() == 1)
                {
                    print("THREE OF KIND");
                    rewardRule = RewardRulee.TOK;
                    return;
                }
            }

        }

        if (listTempInt.Count != 0)
        {

            if (listNumber.Distinct().Count() == 3)
            {
                if (countMin == 2 || countMax == 2)
                {
                    print("TWO PAIR");
                    rewardRule = RewardRulee.TP;
                    return;
                }
                else
                {
                    print("KIND OF THREE");
                    rewardRule = RewardRulee.TOK;
                    return;
                }
            }

            if (listNumber.Distinct().Count() == 4)
            {
                if (listNumber.Count(n => n == 11) == 2 || listNumber.Count(n => n == 12) == 2 || listNumber.Count(n => n == 13) == 2 || listNumber.Count(n => n == 14) == 2)
                {
                    print("JACKS OR BETTER");
                    rewardRule = RewardRulee.JOB;
                }
                return;

            }
        }
    }
    int moneyShow = 0;
    void CalculateMoney()
    {
        try
        {

            int value = int.Parse(txtShowBet.text);
            switch (rewardRule)
            {
                case RewardRulee.JOB:

                    OpenNotifyDouble("JACKS OR BETTER", value, 8);
                    break;
                case RewardRulee.TP:

                    OpenNotifyDouble("TWO PAIR", value, 7);
                    break;
                case RewardRulee.TOK:

                    OpenNotifyDouble("THREE OF A KIND", value, 6);
                    break;
                case RewardRulee.S:

                    OpenNotifyDouble("STRAIGHT", value, 5);
                    break;
                case RewardRulee.F:

                    OpenNotifyDouble("FLUSH", value, 4);
                    break;
                case RewardRulee.FH:

                    OpenNotifyDouble("FULL HOUSE", value, 3);
                    break;
                case RewardRulee.FOK:

                    OpenNotifyDouble("FOUR OF A KIND", value, 2);
                    break;
                case RewardRulee.SF:

                    OpenNotifyDouble("STRAIGHT FLUSH", value, 1);
                    break;
                case RewardRulee.RF:


                    OpenNotifyDouble("ROYAL FLUSH", value, 0);
                    break;
                default:
                    ShowWin("", loseAnimator);
                    SoundController.Sound.VideoPoker_Lose();
                    StartCoroutine(DelayResetLose(1f));
                    if (DataManager.Instance.Coins <= 0)
                    {
                        StartCoroutine(RotateCard.rotateCard.FingerActive());
                        txtShowBet.text = "1";
                    }
                    if (int.Parse(txtShowBet.text) > DataManager.Instance.Coins && DataManager.Instance.Coins > 0)
                    {
                        txtShowBet.text = "" + DataManager.Instance.Coins;
                    }
                    break;
            }

        }
        catch
        {

        }
    }
    public int valueRewardOri = 0;
    void OpenNotifyDouble(string strNameReward, int value, int index)
    {
        moneyShow = moneyShow + value * Common.common.valueOri[index];
        animatorKindReward[index].Play("blink");
        DoubleUp.doubleUp.txtTake.text = (value * Common.common.valueOri[index]).ToString();
        valueRewardOri = value * Common.common.valueOri[index];
        Common.common.overlay.SetActive(true);
        ShowWin(strNameReward + "\n$" + (value * Common.common.valueOri[index]).ToString(), winAnimator);
        SoundController.Sound.VideoPoker_Win();
        DataManager.Instance.AddCoins(value * Common.common.valueOri[index]);
    }
    public IEnumerator DelayResetLose(float time)
    {
        yield return new WaitForSeconds(time);
        RotateCard.rotateCard.Reset();

    }

    public void ShowScore()
    {
        txtMoney.text = "" + DataManager.Instance.Coins;
        rewardRule = RewardRulee.NONE;
        if (moneyShow == 0)
        {
            StartCoroutine(RotateCard.rotateCard.FingerActive());
        }
        if (int.Parse(txtShowBet.text) > DataManager.Instance.Coins)
        {
            txtShowBet.text = "" + DataManager.Instance.Coins;
        }
    }

    public void ShowWin(string str, Animator anim)
    {
        txtWin.text = str;
        anim.Play("zoom out");
        StartCoroutine(OffWin(anim));
    }

    IEnumerator OffWin(Animator anim)
    {
        yield return new WaitForSeconds(1f);
        anim.Play("zoom in");
        yield return new WaitForSeconds(.3f);
        if (anim.name != "lose bg" && !DoubleUp.doubleUp.doubleUpPanel.activeInHierarchy)
        {
            doubleUpAnimator.Play("zoom out");

        }

    }
    public void UpBet()
    {
        if (RotateCard.rotateCard.st != StageDealAndDraw.deal) return;
        int value = int.Parse(txtShowBet.text);
        value++;
        value = Mathf.Clamp(value, 1, DataManager.Instance.Coins);
        txtShowBet.text = value.ToString();
    }
    public void DownBet()
    {
        if (RotateCard.rotateCard.st != StageDealAndDraw.deal) return;
        int value = int.Parse(txtShowBet.text);
        value--;
        value = Mathf.Clamp(value, 1, DataManager.Instance.Coins);
        txtShowBet.text = value.ToString();
    }

    public void MaxBet()
    {
        if (RotateCard.rotateCard.st != StageDealAndDraw.deal) return;
        if (DataManager.Instance.Coins == 0)
        {
            txtShowBet.text = "1";
        }
        else
            txtShowBet.text = "" + DataManager.Instance.Coins;
    }
    public void GetMoreCoin()
    {
        if (RotateCard.rotateCard.st != StageDealAndDraw.deal) return;
        if (DataManager.Instance.Coins == 0 || DataManager.Instance.Coins < (int.Parse(reward.txtShowBet.text)))
        {
            DataManager.Instance.AddCoins(Random.Range(50, 100));
            RotateCard.rotateCard.finger.SetActive(false);
            Common.common.overlayMoney.SetActive(false);
            SoundController.Sound.VideoPoker_MoreCoin();
            moneyEffect.Play();
        }
    }
    void ValueChangeCheck()
    {
        int value = int.Parse(txtShowBet.text);
        value = Mathf.Clamp(value, 1, DataManager.Instance.Coins);
        txtShowBet.text = value.ToString();
    }
}

