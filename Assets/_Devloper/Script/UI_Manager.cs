using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UI_Manager : MonoBehaviour
{
    public GameObject exitPanel;
    public GameObject loadingScreen;
    public Slider loadingBar;

    public int quick_count;

    private void Awake()
    {
        Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            quick_count++;

            if (quick_count == 1)
            {
                StartCoroutine(reset());
            }

            if (quick_count >= 2)
            {
                Application.Quit();
                Debug.Log("Exit");
            }
        }
    }

    public IEnumerator reset()
    {
        yield return new WaitForSeconds(0.6f);

        quick_count = 0;

        if (SceneManager.GetActiveScene().buildIndex != 1)
        {
            SceneManager.LoadScene("Main");
        }
        else
        {
            if (exitPanel)
            {
                exitPanel.SetActive(true);
            }

        }
    }
    
    public void UserChoiceExit(int choice)
    {
        if (choice == 1)
        {
            Application.Quit();
        }
        exitPanel.SetActive(false);
    }

    public void Roulette()
    {
        SceneManager.LoadScene("roulettegame");
        //AsyncOperation opration = SceneManager.LoadSceneAsync("roulettegame");
        //loadingScreen.SetActive(true);
        //while(!opration.isDone)

        //{
        //    loadingBar.value = opration.progress;
            
        //}
    }
    public void SlotNew()
    {
        SceneManager.LoadScene("SlotNew");
    }
    public void SloatGame()
    {
        SceneManager.LoadScene("SlotDemo");
    }
    public void RouletteGameBackBtn()
    {
        SceneManager.LoadScene("Main");
        ValueManager.isSpinning = false;
        ValueManager.betsEnabled = true;
        ValueManager.ClearValue();
    }

    public void ExitPanelOpen()
    {
        exitPanel.SetActive(true);
    }
}
