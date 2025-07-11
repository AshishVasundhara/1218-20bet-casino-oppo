using UnityEngine;

public class BetSpace : MonoBehaviour {

    // Variable definition
    private GameObject chipStackGameObject;
    [HideInInspector]
    public ChipStack chipStack;

    public int[] winningNumbers = new int[] { 1 };
    BalanceManager bm;

    // Initiate the bet buttons on the table
    void Start()
    {
        bm = FindObjectOfType<BalanceManager>();

        if (GetComponent<MeshRenderer>())
        {
            GetComponent<MeshRenderer>().enabled = false;
        }

        chipStackGameObject = Instantiate(Resources.Load<GameObject>("chipstack"));
        chipStackGameObject.transform.position = transform.position;
        chipStackGameObject.transform.parent = transform.parent;
        chipStack = chipStackGameObject.GetComponent<ChipStack>();
        ValueManager.RegisterBetSpace(this);
    }

  
    // Show the chip value in the betspace
    void OnMouseOver()
    {
        ToolTipManager.getInstance().target = chipStack;
    }

    // Dismiss the chip value in the betspace
    void OnMouseOut()
    {
        if (ToolTipManager.getInstance().target == chipStack)
        {
            ToolTipManager.getInstance().target = null;
        }
    }

    // Adds the chosen chip to the certain spot you chose
	void OnMouseDown()
    {
        if (bm.balance == 0)
        {
            // show not enough balance popup
            bm.NotEnoughBalPopup.SetActive(true);
        }
        Debug.Log("=== ENTER ====");
        int selectedValue = ChipManager.GetSelectedValue();

        if (ValueManager.betsEnabled && BalanceManager.getInstance().balance >= selectedValue)
        {
            AudioManager.getInstance().Play("chip", 1.0f);
            Debug.Log("===SELECTED VAL====" + selectedValue);

            BetHistoryManager.getInstance().Add(chipStack, selectedValue);
            chipStack.Add(selectedValue);
        }
    }

   // Calculates the winning chips and sums up the money won|lost
    public int ResolveBet(int result)
    {
         int multiplier = 36 / winningNumbers.Length;
         bool won = false;

         foreach (int num in winningNumbers)
         {
             if (num == result)
             {
                 won = true;
                 break;
             }
         }

         int winAmount = 0;
         if (won) { winAmount = chipStack.Win(multiplier); }
         chipStack.Clear();
         return winAmount;
    }
}
