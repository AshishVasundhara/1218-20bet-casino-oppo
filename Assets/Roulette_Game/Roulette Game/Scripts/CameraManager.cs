using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    // Variable definition
    [SerializeField] GameObject cameras;
    static CameraManager instance;

    // Singleton initialization
    void Start () { instance = this; }

    // Switch camera to watch the roulette
    public void SwitchToSpin()
    {
        cameras.transform.GetChild(0).gameObject.SetActive(false);
        cameras.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Switch camera to watch the table
    public void SwitchToBet()
    {
        cameras.transform.GetChild(1).gameObject.SetActive(false);
        cameras.transform.GetChild(0).gameObject.SetActive(true);
    }

    // Return the instance of the singleton
    public static CameraManager getInstance() { return instance; }
}
