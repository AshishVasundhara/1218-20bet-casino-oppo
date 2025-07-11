using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonSound : MonoBehaviour
{
    void Start() {

        Button[] buttons = GameObject.FindObjectsOfType(typeof(Button)) as Button[];
        foreach (Button temp in buttons) {

            temp.onClick.AddListener(delegate() { PlaySoundButtonOnClick(); });
        }
    }

    public void PlaySoundButtonOnClick()
	{
        
		SoundController.Sound.ClickBtn ();
    }

}
