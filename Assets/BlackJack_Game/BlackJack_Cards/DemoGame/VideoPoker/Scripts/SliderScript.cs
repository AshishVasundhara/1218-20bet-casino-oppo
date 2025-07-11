using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScript : MonoBehaviour
{

    Slider slider;
    float maxBet;
    public InputField inputField;
    public Transform parentSlider;
    public static SliderScript sliderScipt;
    void Awake() {
        sliderScipt = this;
        slider = GetComponent<Slider>();
        slider.onValueChanged.AddListener(delegate { OnValueChange(); });
        inputField.onEndEdit.AddListener(delegate { OnValueChangeInputField(); });
    }

    void OnValueChange() {
		maxBet = DataManager.Instance.Coins;
        float temp = Mathf.RoundToInt((maxBet * slider.value));
        inputField.text = temp.ToString();
        RewadControl.reward.txtShowBet.text = temp.ToString();
    }

    public void OnValueChangeInputField() {
        float inputvalue = float.Parse(inputField.text);
		float maxBet = DataManager.Instance.Coins;
        inputvalue = Mathf.Clamp(inputvalue, 1, maxBet);
        slider.value = inputvalue / maxBet;
        RewadControl.reward.txtShowBet.text = inputvalue.ToString();
    }
    public void ChangeSliderValue() {
        float inputvalue = float.Parse(RewadControl.reward.txtShowBet.text);
		float maxBet = DataManager.Instance.Coins;
        inputvalue = Mathf.Clamp(inputvalue, 1, maxBet);
        slider.value = inputvalue / maxBet;
        RewadControl.reward.txtShowBet.text = inputvalue.ToString();
    }
    public void ClickBet() {
		if (RotateCard.rotateCard.st != StageDealAndDraw.deal || DataManager.Instance.Coins <= 0) return;
        if (parentSlider.localScale.y == 0) {
            print(inputField.text);
            if (!string.IsNullOrEmpty(inputField.text)) {
                ChangeSliderValue();
            }
            inputField.text = RewadControl.reward.txtShowBet.text;
            parentSlider.GetComponent<Animator>().Play("down up");
        }
        else {
            parentSlider.GetComponent<Animator>().Play("up down");
        }
    }
}
