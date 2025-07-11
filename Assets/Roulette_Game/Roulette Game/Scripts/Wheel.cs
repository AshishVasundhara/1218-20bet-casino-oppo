using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using TMPro;

public class Wheel : MonoBehaviour {
    
    // Variable definition
    [SerializeField] private Ball ball;
    [SerializeField] private TextMeshProUGUI resultText;
    GameObject                    resultCheckerObject;
    float                         speed;
    int[]                         betNumbers;


  //float z = 0.0f;
    void Start()
    {
        // Variable initialization
        speed = 0.6f;
        //betNumbers = new int[] { 0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36, 11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9, 22, 18, 29, 7, 28, 12, 35, 3, 26 };

        // Placing the result checker triggers
        //for (int i = 0; i < betNumbers.Length; i ++)
        //{
        //    resultCheckerObject = Instantiate(Resources.Load<GameObject>("resultchecker"));
        //    resultCheckerObject.transform.RotateAround(transform.position, Vector3.up, i * 360 / betNumbers.Length);      
      
        //    resultCheckerObject.transform.parent = transform;
        //    resultCheckerObject.GetComponent<ValueDetector>().slotValue = betNumbers[i];
        //}
    }

    void FixedUpdate (){ transform.Rotate(Vector3.up * speed); }

    // This function is responsible for the roulette spin movement
    public void SpinWheel()
    {
       
        CameraManager.getInstance().SwitchToSpin();
        ValueManager.isSpinning = true;
        ValueManager.betsEnabled = false;
        ValueManager.ClearValue();
        resultText.text = "";
        ball.SpinBall(Random.Range(15f, 25f));
    }

    
}
