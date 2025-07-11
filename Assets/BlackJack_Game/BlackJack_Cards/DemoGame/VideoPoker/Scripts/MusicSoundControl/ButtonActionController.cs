using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ButtonActionController : MonoBehaviour
{

    public static ButtonActionController Click;     // instance of ButtonActionController

    public Sprite[] ButtonSprite;                   //sprite array of buttons
    void Awake()
    {
        if (Click == null)
        {
            DontDestroyOnLoad(gameObject);
            Click = this;
        }
        else if (Click != this)
        {
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Go to a scene with name
    /// </summary>
    /// <param name="screen">name of the scene to direction</param>
    IEnumerator GotoScreen(string screen)
    {
        yield return new WaitForSeconds(0);
        SceneManager.LoadScene(screen);
    }
		

    /// <summary>
    /// Set and change state of music
    /// </summary>
    /// <param name="button">Image button</param>
    public void BMusic(UnityEngine.UI.Button button)
    {

        if (PlayerPrefs.GetInt("MUSIC", 0) != 1)
        {
            PlayerPrefs.SetInt("MUSIC", 1); // music off
        }
        else
        {
            PlayerPrefs.SetInt("MUSIC", 0); // music on
        }

    }
    /// <summary>
    /// Set and change state of sound background
    /// </summary>
    /// <param name="button">Image button</param>
    public void BSound(UnityEngine.UI.Image button)
    {
        if (PlayerPrefs.GetInt("SOUND", 0) != 1)
        {
            PlayerPrefs.SetInt("SOUND", 1);
            button.overrideSprite = ButtonSprite[3];
        }
        else
        {
            PlayerPrefs.SetInt("SOUND", 0);
            button.overrideSprite = ButtonSprite[2];
        }
    }
}
