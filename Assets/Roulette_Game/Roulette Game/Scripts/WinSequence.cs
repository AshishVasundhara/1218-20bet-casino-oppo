using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinSequence : MonoBehaviour {

    // Variable definition
    [SerializeField] private GameObject winPanel;
    [SerializeField] private Text       winText;
    [SerializeField] private GameObject buttons;
    [SerializeField] private TextMeshProUGUI  valueText;
    [SerializeField] private Text       redHistoryText;
    [SerializeField] private Text       blackHistoryText;
    
    private int[]                       redNumbers;
    private bool                        isRed;

    private void Start()
    {
        // Variable initialization
        redNumbers = new int[] { 1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36 };
    }

    // Display what number the ball landed on, and adds money if you bet on that number
    public void ShowValue(int value, int totalWin)
    {
        CameraManager.getInstance().SwitchToBet();

        if (totalWin > 0)
        {
            AudioManager.getInstance().Play("win", 1.0f);
            winPanel.SetActive(true);

            winText.text = totalWin.ToString();
        }

        valueText.text = value.ToString();
        isRed = false;

        for (int i = 0; i < redNumbers.Length; i++)
        {
            if (redNumbers[i] == value)
            {
                isRed = true;
                break;
            }
        }

        if (isRed)
        {
            valueText.color = Color.red;
            redHistoryText.text = value.ToString() + "\n" + redHistoryText.text;
            blackHistoryText.text = "\n" + blackHistoryText.text;
        }
        else
        {
            if (value == 0)
            {
                valueText.color = Color.green;
            } else
            {
                valueText.color = Color.white;
            }
            blackHistoryText.text = value.ToString() + "\n" + blackHistoryText.text;
            redHistoryText.text = "\n" + redHistoryText.text;
        }

        EnableBets();
    }

    public void EnableBets()
    {
        winPanel.SetActive(false);
        buttons.SetActive(true);
        ValueManager.betsEnabled = true;
    }
}
