using UnityEngine;
using System.Collections.Generic;

public static class ValueManager {

    // Variable definition
    public static bool isSpinning   = false;
    public static bool betsEnabled  = true;
    public static int value;
    static List<BetSpace> betSpaces = new List<BetSpace>();

    // This function sets the value the ball landed on, then informs you if you won any money
    public static void SetValue(int newValue)
    {
        if (isSpinning)
        {
            isSpinning = !isSpinning;
            value = newValue;
            GameObject.Find("high" + value.ToString()).GetComponent<MeshRenderer>().enabled = true;
            int totalWin = 0;
            foreach (BetSpace betSpace in betSpaces)
            {
                totalWin += betSpace.ResolveBet(value);
            }
            BalanceManager.getInstance().balance += totalWin;
            PlayerPrefs.SetInt("balanceVal", BalanceManager.getInstance().balance);
            BetHistoryManager.getInstance().ClearHistory();
            GameObject.Find("WinSequence").GetComponent<WinSequence>().ShowValue(value, totalWin);
        }
    }

    // Clear the value choice made before the spin
    public static void ClearValue()
    {
        GameObject previousResultHighlight = GameObject.Find("high" + value.ToString());
        if (previousResultHighlight != null) { 
        previousResultHighlight.GetComponent<MeshRenderer>().enabled = false;
        }
    }

    // Add the value to the bet total before the spin
    public static void RegisterBetSpace(BetSpace betSpace)
    {
        betSpaces.Add(betSpace);
    }
}