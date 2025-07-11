using System.Collections;
using System.Collections.Generic;
using BE;
using UnityEngine;
using UnityEngine.UI;

public class InfopanelShow : MonoBehaviour {


    public void OnEnable()
    {
        

        for (int i = 2; i < SlotGame.instance.Symbols.Count;i++)
        {
            for (int j = 1; j < 5;j++)
            {
                transform.GetChild(0).GetChild(i).GetChild(j).GetComponent<Text>().text = "x"+ (j+1).ToString(); 
                transform.GetChild(0).GetChild(i).GetChild(j).GetChild(0).GetComponent<Text>().text = SlotGame.instance.Symbols[i].reward[j].ToString(); 
            }
        }
    }

}
