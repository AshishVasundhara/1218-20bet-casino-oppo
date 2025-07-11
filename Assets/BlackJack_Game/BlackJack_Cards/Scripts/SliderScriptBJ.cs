using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SliderScriptBJ : MonoBehaviour
{

	Slider slider;
	float maxBet;
	public InputField inputField;
	public Transform parentSlider;
	public static SliderScriptBJ _sliderScipt;
	public Text BetTex;
	public Text MoneyTex;


	void Awake() 
	{
		BetTex.text = "1";
		_sliderScipt = this;
		slider = GetComponent<Slider>();
		slider.onValueChanged.AddListener(delegate { OnValueChange(); });
		inputField.onEndEdit.AddListener(delegate { OnValueChangeInputField(); });

	}
	void Update()
	{
		MoneyTex.text = "" + DataManager.Instance.Coins;
	}

	void OnValueChange() 
	{
		maxBet = DataManager.Instance.Coins;
		float temp = Mathf.RoundToInt((maxBet * slider.value));
		inputField.text = temp.ToString();
		BetTex.text = temp.ToString();
	}

	public void OnValueChangeInputField() {
		float inputvalue = float.Parse(inputField.text);
		float maxBet =DataManager.Instance.Coins;
		inputvalue = Mathf.Clamp(inputvalue, 1, maxBet);
		slider.value = inputvalue / maxBet;
		BetTex.text = inputvalue.ToString();
	}
	public void ChangeSliderValue() {
		float inputvalue = float.Parse(BetTex.text);
		float maxBet = DataManager.Instance.Coins;
		inputvalue = Mathf.Clamp(inputvalue, 1, maxBet);
		slider.value = inputvalue / maxBet;
		BetTex.text = inputvalue.ToString();
	}
	public void ClickBet() {
		if (DataManager.Instance.Coins <= 0 || int.Parse(BetTex.text)<=0) return;
		if(int.Parse(BetTex.text)>DataManager.Instance.Coins)
		{
			BetTex.text = "" + DataManager.Instance.Coins;
		}
		if (parentSlider.localScale.y == 0)
		{
			if (!string.IsNullOrEmpty(inputField.text)) {
				ChangeSliderValue();
			}
			inputField.text = BetTex.text;
			parentSlider.GetComponent<Animator>().Play("down up");
		}
		else {
			parentSlider.GetComponent<Animator>().Play("up down");
		}
	}

}
