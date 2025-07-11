using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
public class InputBet : MonoBehaviour {

    public InputField mainInputField;
  
    public void Start() {
        //Adds a listener to the main input field and invokes a method when the value changes.
        mainInputField.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
    }

    // Invoked when the value of the text field changes.
    public void ValueChangeCheck() {
        try {
            StartCoroutine(Delay());
        }
        catch {

        }
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(0f);
        mainInputField.text = (Common.countNumberBet + 1) + "$";
    }
}
