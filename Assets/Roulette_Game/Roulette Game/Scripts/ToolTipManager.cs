using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class ToolTipManager : MonoBehaviour {

    // Variable definition
    static ToolTipManager               instance;
    [SerializeField] private GameObject toolTip;
    [SerializeField] private Text       toolTipText;

    [HideInInspector]
    public ChipStack target;

    // Use this for initialization
    void Start () {
        instance = this;
    }

    void Update()
    {
        toolTip.SetActive(false);

        if (target != null)
        {
            if (target.GetValue() > 0)
            {
                toolTip.transform.position = Camera.main.WorldToScreenPoint(target.transform.position);
                toolTip.SetActive(true);
                toolTipText.text = target.GetValue().ToString();
            }
        }
    }
	
    public static ToolTipManager getInstance()
    {
        return instance;
    }
}
