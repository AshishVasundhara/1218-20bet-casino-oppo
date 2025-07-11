using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class BetHistoryManager : MonoBehaviour {

    static BetHistoryManager instance;

    public Button spinButton;
    public Button undoButton;
    public Button clearButton;

    List<ChipStack> stackHistory;
    List<int> valueHistory;

    int totalBet;

    // Use this for initialization
    void Start ()
    {
        stackHistory = new List<ChipStack>();
        valueHistory = new List<int>();

        spinButton.interactable = false;
        undoButton.interactable = false;
        clearButton.interactable = false;

        instance = this;

        ClearHistory();
    }
	
    public void Add(ChipStack chipStack, int value)
    {
        totalBet += value;

        spinButton.interactable = true;
        undoButton.interactable = true;
        clearButton.interactable = true;

        stackHistory.Add(chipStack);
        valueHistory.Add(value);
    }

    public void Undo()
    {
        if (ValueManager.betsEnabled && stackHistory.Count > 0)
        {
            totalBet -= valueHistory[valueHistory.Count - 1];
            stackHistory[stackHistory.Count - 1].Remove(valueHistory[valueHistory.Count - 1]);

            stackHistory.RemoveAt(stackHistory.Count - 1);
            valueHistory.RemoveAt(valueHistory.Count - 1);

            if (totalBet <= 0)
            {
                spinButton.interactable = false;
            }
            if (stackHistory.Count == 0)
            {
                undoButton.interactable = false;
                clearButton.interactable = false;
            }
        }
    }

    public void ClearBets()
    {
        if (ValueManager.betsEnabled)
        {
            ClearHistory();
            totalBet = 0;
        }
    }

    public void ClearHistory()
    {
        spinButton.interactable = false;
        undoButton.interactable = false;
        clearButton.interactable = false;

        int count = stackHistory.Count;

        while (count > 0)
        {
            Undo();
            count--;
        }
    }



    public static BetHistoryManager getInstance()
    {
        return instance;
    }
}
