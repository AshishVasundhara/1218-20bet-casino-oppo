using UnityEngine;

public class ValueDetector : MonoBehaviour {

    // Constant definition
    const int WAIT_UNTIL_SET_VALUE =30;

    // Variable definition
    private bool isBallInside;
    private int  insideTimer;
    public int   slotValue;

    void Update()
        {
            // Check if the ball has been inside the trigger for a number of 
            // frames, then do logic according to the slot value
            if (isBallInside && ValueManager.isSpinning)
            {
                insideTimer++;
            } else
            {
                insideTimer = 0;
            }
    
            if (insideTimer > WAIT_UNTIL_SET_VALUE)
            {
                ValueManager.SetValue(slotValue);
            }
        }

    void OnTriggerEnter(Collider col)
    {
        // Check if ball has entered
        if (col.gameObject.name == "ball")
        {
            isBallInside = true;
        }
    }

    void OnTriggerExit(Collider col)
    {
        // Check if ball has left
        if (col.gameObject.name == "ball")
        {
            isBallInside = false;
        }
    }

    
}
