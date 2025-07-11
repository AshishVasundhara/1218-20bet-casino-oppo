using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using System.Linq;
public class ControlBet : MonoBehaviour
{
    public static ControlBet controlBet;
    public List<Text> listText;
    public int order;
    public GameObject overlay;
    void Start() 
	{
        mainInputField.onEndEdit.AddListener(delegate { ValueChangeCheck(); });
        controlBet = this;
        if (order != 0) {
            for (int i = 0; i < listText.Count; i++) {
                listText[i].text = (int.Parse(listText[i].text.Trim()) * (order + 1)).ToString();
            }
        }
    }
    public InputField mainInputField;

    public void ValueChangeCheck() 
	{
        try {
            if (int.Parse(mainInputField.text.Trim()) - 1 < 0) return;
            Common.countNumberBet = int.Parse(mainInputField.text.Trim()) - 1;

            if (Common.countNumberBet < 5) {
                if (order != 0) {
                    for (int i = 0; i < listText.Count; i++) {
                        listText[i].text = (Common.common.valueOri[i] * (order + 1)).ToString();
                    }
                }
                if (order == 0) {
                    for (int i = 0; i < listText.Count; i++) {
                        listText[i].text = (Common.common.valueOri[i]).ToString();
                    }
                }
            }

            UpBet(this);
        }
        catch {

        }
    }
    public void UpBet(ControlBet controlBet) {
      
        listText = controlBet.listText;
        overlay.SetActive(false);
        if (order == Common.countNumberBet) {
            overlay.SetActive(true);
            StartCoroutine(Delay());
            return;
        }

        if (Common.countNumberBet > 4) {
            if (order == 4) {
                overlay.SetActive(true);
                StartCoroutine(Delay());
            }
            for (int i = 0; i < listText.Count; i++) {
                listText[i].text = ((((order + Common.countNumberBet) - 4) * Common.common.valueOri[i]) + Common.common.valueOri[i]).ToString();
            }
        }
    }

    IEnumerator Delay() {
        yield return new WaitForSeconds(.2f);
        mainInputField.text = (Common.countNumberBet + 1) + "$";
    }
}
