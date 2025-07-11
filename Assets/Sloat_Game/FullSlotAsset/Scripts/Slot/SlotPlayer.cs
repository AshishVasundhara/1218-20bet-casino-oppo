using UnityEngine;

public class SlotPlayer : MonoBehaviour
{
    [Space(10, order = 0)]
    [Header("Default data", order = 1)]
    public int defCoinsCount = 500; // default data
    public int defFreeSpin = 0;    

    private int coins;
    private int freeSpins;
    private int bet;
    private int selLinesCount = 0;
    public static SlotPlayer Instance;

    public int Coins
    {
        get { return coins; }
    }
    public int FreeSpins
    {
        get {  return freeSpins;}
    }
    public bool HasFreeSpin
    {
        get { return freeSpins > 0; }
    }
    public int Bet
    {
        get { return bet; }
        set { bet = value; }
    }
    internal bool AnyLineSelected
    {
        get { return selLinesCount > 0; }
    }
    internal bool HasMoneyForBet
    {
        get { return bet <= coins; }
    }

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    void Start()
    {
        SetStartSettings();

        LineButtonBehavior[] lbbs = FindObjectsOfType<LineButtonBehavior>();
        for (int i = 0; i < lbbs.Length; i++)
        {
            lbbs[i].PressButtonDelegate += (num) => {
                selLinesCount++;
                bet = selLinesCount;
                SlotMenuController.Instance.Refresh();
            };

            lbbs[i].UnPressButtonDelegate += (num) =>
            {
                selLinesCount--;
                bet = selLinesCount;
                SlotMenuController.Instance.Refresh();
            };
        }
    }

    internal void SetStartSettings()
    {
        coins = defCoinsCount;
        freeSpins = defFreeSpin;
    }

    internal void AddCoins(int coins)
    {
        this.coins += coins;
        if (this.coins < 0) this.coins = 0;
    }

    internal void AddSpins(int spins)
    {
        freeSpins += spins;
        if (freeSpins < 0) freeSpins = 0;
    }

    internal bool ApplyBet()
    {
        if (HasMoneyForBet)
        {
            AddCoins(-bet);
            SlotMenuController.Instance.Refresh();
            return true;
        }
        else
        {
            return false;
        }
    }

    internal bool ApllyFreeSpin()
    {
        if (HasFreeSpin)
        {
            AddSpins(-1);
            SlotMenuController.Instance.Refresh();
            return true;
        }
        else
        {
            return false;
        }
    }
}
