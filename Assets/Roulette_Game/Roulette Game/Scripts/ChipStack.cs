using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class ChipStack : MonoBehaviour {

    // Variable definition
    private Vector3 intiialPosition;
    private int value;
    private List<GameObject> chips;

    static int NUM_CHIPS = 6;
    static int[] CHIP_VALUES = new int[] { 1, 5, 10, 25, 50, 100 };
    static string[] CHIP_PREFAB_NAMES = new string[] { "bluechip", "greenchip", "orangechip", "redchip", "blueskychip", "pinkchip" };

    private Vector3 targetPosition;
    private float moveVelocity = 0f;
    private float maxVelocity = 1.5f;

    void Start()
    {
        // Variable initialization
        intiialPosition = transform.position;
        value = 0;
    }

    // Add chip value to stack
    public void Add(int value)
    {
        SetValue(this.value + value);
    }

    // Remove chip value from stack
    public void Remove(int value)
    {
        SetValue(this.value - value);
    }

    // Remove all chips value from stack
    public void Clear()
    {
        value = 0;

        if (chips != null)
        {
            foreach (GameObject chip in chips)
            {
                Destroy(chip);
            }
        }

        chips = null;
    }

    // Get chips value from stack
    public int GetValue()
    {
        return value;
    }

    // Set stack value
    public void SetValue(int value)
    {
        BalanceManager.getInstance().balance += this.value;
        PlayerPrefs.SetInt("balanceVal", BalanceManager.getInstance().balance);

        Clear();

        if (value <= 0)
        {
            return;
        }

        this.value = value;
        BalanceManager.getInstance().balance -= this.value;
        PlayerPrefs.SetInt("balanceVal", BalanceManager.getInstance().balance);
        chips = new List<GameObject>();

        int currentChipIndex = NUM_CHIPS - 1;

        while (value > 0)
        {
            int nextValue = value - CHIP_VALUES[currentChipIndex];

            if (nextValue < 0)
            {
                currentChipIndex--;
                if (currentChipIndex < 0)
                {
                    throw new Exception("Impossible value");
                }
                continue;
            }

            value = nextValue;

            GameObject newChip = Instantiate(Resources.Load<GameObject>(CHIP_PREFAB_NAMES[currentChipIndex]));
            newChip.transform.parent = gameObject.transform;
            newChip.transform.localPosition = new Vector3(0, newChip.GetComponent<Renderer>().bounds.size.y * (chips.Count + 1), 0);
            newChip.layer = 10;

            chips.Add(newChip);
        }
    }

    // Calculates and return win value
    public int Win(int multiplier)
    {
        int winAmount = value * multiplier;
    
        BalanceManager.getInstance().balance += winAmount - value;
        PlayerPrefs.SetInt("balanceVal", BalanceManager.getInstance().balance);

        SetValue(winAmount);
        return winAmount;
    }
}
