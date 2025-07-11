using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BalanceManager : MonoBehaviour {

    public int balance = 1000;

    //public Text balanceText;
    public TextMeshProUGUI balanceText;
    public GameObject NotEnoughBalPopup;

    static BalanceManager instance;

    public static BalanceManager getInstance()
    {
        return instance;
    }

    void Start()
    {
        instance = this;
        balance = PlayerPrefs.GetInt("balanceVal", 1000);

    }

	void Update () {
        //balanceText.text = PlayerPrefs.GetInt("balanceVal", 1000).ToString();
        balanceText.text = balance.ToString();
        
    }

    public void NotEnoughBalOKClick()
    {
        NotEnoughBalPopup.SetActive(false);
    }
}
