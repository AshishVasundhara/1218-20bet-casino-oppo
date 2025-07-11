using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitGameButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnExitGameButtonClick()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
